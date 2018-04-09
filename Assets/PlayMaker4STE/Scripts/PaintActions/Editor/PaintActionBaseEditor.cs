#if PLAYMAKER && SUPER_TILEMAP_EDITOR
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HutongGames.PlayMakerEditor;
using UnityEditor;
using HutongGames.PlayMaker;

namespace CreativeSpore.SuperTilemapEditor.PlayMakerActions
{
    [CustomActionEditor(typeof(PaintActionBase))]
    public class PaintActionBaseEditor<T> : CustomActionEditor where T : PaintActionBase
    {
        protected TileGridControl m_brushTileGridControl;
        protected T m_action;

        public override void OnEnable()
        {
            m_action = target as T;
            //Debug.Log("<color=green>FloodFill Enabled " + (m_action.gameObject != null? m_action.gameObject.GameObject : (GameObject)null) + "</color>");
        }

        public override void OnDisable()
        {
            //Debug.Log("<color=blue>FloodFill Disabled " + (m_action.gameObject != null ? m_action.gameObject.GameObject : (GameObject)null) + "</color>");
        }

        protected STETilemap GetTargetTilemap()
        {
            if (target.Owner)
            {
                var go = m_action.Fsm.GetOwnerDefaultTarget(m_action.gameObject);
                return go ? go.GetComponent<STETilemap>() : null;
            }
            return null;
        }

        // Find a tileset looking for a CreateTilemap action and using it's Tileset property. First in the same state, then globally in all the states
        protected Tileset GuessTileset()
        {
            if (target.Owner)
            {
                FsmStateAction[] actions = m_action.State.Actions.Where(x => x is CreateTilemap).ToArray();
                if (actions.Length == 0)
                    actions = m_action.Fsm.States.SelectMany(x => x.Actions).Where(x => x is CreateTilemap).ToArray();
                return actions.Length > 0 ? (actions[actions.Length - 1] as CreateTilemap).tileset.Value as Tileset : null;
            }
            return null;
        }

        protected bool UpdateTileGridControl(Tileset tileset)
        {
            bool isDirty = false;
            if (m_brushTileGridControl == null
                || m_action.tileSelection.rowLength != m_brushTileGridControl.Width
                || m_action.tileSelection.columnLength != m_brushTileGridControl.Height
                || target.Owner != m_brushTileGridControl.TargetObj
                )
            {
                isDirty = true;
                m_brushTileGridControl = new TileGridControl(target.Owner, m_action.tileSelection.rowLength, m_action.tileSelection.columnLength, null,
                    (int tileIdx) => { return tileIdx >= m_action.tileSelection.selectionData.Length? Tileset.k_TileData_Empty : (uint)m_action.tileSelection.selectionData[tileIdx]; },
                    (int tileIdx, uint tileData) => { if (tileIdx < m_action.tileSelection.selectionData.Length) m_action.tileSelection.selectionData[tileIdx] = (int)tileData; }
                );
            }
            m_brushTileGridControl.Tileset = tileset;
            return isDirty;
        }

        public override bool OnGUI()
        {
            bool isDirty = false;
            DoDrawBasicFields();
            isDirty |= DoDrawPatternFields();            

            return isDirty || GUI.changed;
        }

        protected void DoDrawBasicFields()
        {
            EditField("gameObject");
            EditField("tileset");
            EditorGUILayout.LabelField("Painting Position", EditorStyles.boldLabel);
            EditField("positionType");
            EditField("startPaintingPosition");
        }

        protected bool DoDrawPatternFields()
        {
            bool isDirty = false;
            EditField("randomizePattern");
            EditorGUILayout.LabelField("Pattern", EditorStyles.boldLabel);
            if (target.Owner)
            {
                STETilemap tilemap = GetTargetTilemap();
                Tileset tileset = m_action.tileset.Value as Tileset;
                if (!tileset)
                {
                    if (tilemap && tilemap.Tileset)
                    {
                        tileset = tilemap.Tileset;
                    }
                    else
                    {
                        tileset = GuessTileset();
                    }
                    m_action.tileset.Value = tileset;
                }

                if (tileset)
                {
                    isDirty |= UpdateTileGridControl(tileset);
                    float tileWidth = Mathf.Min(32f, 160f / m_brushTileGridControl.Width); //160f is half of the size of the width of the action view panel
                    Vector2 visualTileSize = new Vector2(tileWidth, tileWidth * tileset.VisualTileSize.y / tileset.VisualTileSize.x);
                    m_brushTileGridControl.Display(visualTileSize);
                    if (GUILayout.Button("Use tile palette selection as pattern"))
                    {
                        if (tileset.TileSelection != null && tileset.TileSelection.selectionData.Count > 0)
                        {
                            TileSelection copySelection = tileset.TileSelection.Clone();
                            copySelection.FlipVertical();
                            m_action.tileSelection.selectionData = copySelection.selectionData.Select(x => (int)x).ToArray();
                            m_action.tileSelection.rowLength = tileset.TileSelection.rowLength;
                        }
                        else
                        {
                            if (tileset.SelectedTileId != Tileset.k_TileId_Empty)
                                m_action.tileSelection.selectionData = new int[] { tileset.SelectedTileId };
                            else if (tileset.SelectedBrushId != Tileset.k_BrushId_Default)
                                m_action.tileSelection.selectionData = new int[] { tileset.SelectedBrushId << 16 };
                            else
                                m_action.tileSelection.selectionData = new int[] { -1 };
                            m_action.tileSelection.rowLength = 1;
                        }
                    }
                    if (GUILayout.Button("Use brush selection as pattern"))
                    {
                        uint[,] brushSelection = BrushBehaviour.Instance.GetBrushPattern();
                        int[] tileSelection = new int[brushSelection.Length];
                        for (int y = 0, idx = 0; y < brushSelection.GetLength(1); ++y)
                            for (int x = 0; x < brushSelection.GetLength(0); ++x, ++idx)
                                tileSelection[idx] = (int)brushSelection[x, brushSelection.GetLength(1) - y - 1];
                        m_action.tileSelection.selectionData = tileSelection;
                        m_action.tileSelection.rowLength = brushSelection.GetLength(0);
                    }
                }
                else
                {
                    if (tilemap)
                    {
                        EditorGUILayout.HelpBox("You need to set a tileset for the tilemap", MessageType.Info);
                    }
                }
            }
            return isDirty;
        }

        protected delegate void DrawingFunction(int x0, int y0, int x1, int y1, TilemapDrawingUtils.PlotFunction plot);
        protected List<Vector2> m_debugPoints = new List<Vector2>();
        protected virtual void RecalculateDebugPoints(Vector2 startPoint, Vector2 endPoint, DrawingFunction drawingFunc)
        {
            STETilemap tilemap = GetTargetTilemap();
            if (!tilemap)
                return;
            Vector2 cellSizeDiv2 = tilemap.CellSize / 2f;
            m_debugPoints.Clear();
            int x0, y0, x1, y1;
            if ((ePositionType)m_action.positionType.Value == ePositionType.LocalPosition)
            {
                x0 = TilemapUtils.GetGridX(tilemap, startPoint);
                y0 = TilemapUtils.GetGridY(tilemap, startPoint);
                x1 = TilemapUtils.GetGridX(tilemap, endPoint);
                y1 = TilemapUtils.GetGridY(tilemap, endPoint);
            }
            else //if ((ePositionType)m_action.positionType.Value == ePositionType.GridPosition)
            {
                x0 = (int)startPoint.x;
                y0 = (int)startPoint.y;
                x1 = (int)endPoint.x;
                y1 = (int)endPoint.y;
            }
            drawingFunc(x0, y0, x1, y1,
                (x, y) =>
                {
                    m_debugPoints.Add(new Vector2(x * tilemap.CellSize.x + cellSizeDiv2.x, y * tilemap.CellSize.y + cellSizeDiv2.y));
                    return true;
                });
        }

        protected virtual void DrawDebugPoints()
        {
            if (m_debugPoints.Count == 0) return;
            STETilemap tilemap = GetTargetTilemap();
            Vector2 cellSizeDiv2 = tilemap.CellSize / 2f;
            Vector2 realCellSizeDiv2 = tilemap.transform.TransformPoint(cellSizeDiv2);
            float size = Mathf.Min(realCellSizeDiv2.x, realCellSizeDiv2.y);
            for (int i = 0; i < m_debugPoints.Count; ++i)
            {
                Vector2 p0 = tilemap.transform.TransformPoint(m_debugPoints[i]);
                EditorCompatibilityUtils.CircleCap(0, p0, Quaternion.identity, size);
            }
        }

    }
}
#endif