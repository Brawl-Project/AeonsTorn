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
    [CustomActionEditor(typeof(TilemapVertexPaintCircle))]
    public class TilemapVertexPaintCircleEditor : CustomActionEditor
    {
        protected TilemapVertexPaintCircle m_action;
        protected STETilemap m_tilemap;
        protected Tileset m_tileset;

        public override void OnEnable()
        {
            m_action = target as TilemapVertexPaintCircle;
            m_tileset = GuessTileset();
        }

        public override bool OnGUI()
        {
            if (!m_tilemap)
                m_tilemap = GetTargetTilemap();
            bool isDirty = DrawDefaultInspector();
            if (isDirty && !m_tilemap)
                m_tileset = GuessTileset();
            return isDirty;
        }

        public override void OnSceneGUI()
        {
            base.OnSceneGUI();
            
            if (!m_tilemap && !m_tileset) return;

            Vector2 center = Vector2.zero;
            if ((ePositionType)m_action.positionType.Value == ePositionType.LocalPosition)
                center = m_action.position.Value;
            else// if ((ePositionType)positionType.Value == ePositionType.GridPosition)
            {
                if(m_tilemap)
                    center = TilemapUtils.GetTileCenterPosition(m_tilemap, (int)m_action.position.Value.x, (int)m_action.position.Value.y);
                else if(m_tileset)
                    center = TilemapUtils.GetGridWorldPos((int)m_action.position.Value.x, (int)m_action.position.Value.y, m_tileset.TilePxSize / m_tileset.PixelsPerUnit);

            }
            EditorCompatibilityUtils.CircleCap(0, center, Quaternion.identity, m_action.radius.Value);
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
    }
}
#endif