#if PLAYMAKER && SUPER_TILEMAP_EDITOR
using UnityEngine;
using System.Collections;
using System.Linq;
using HutongGames.PlayMakerEditor;
using UnityEditor;
using HutongGames.PlayMaker;

namespace CreativeSpore.SuperTilemapEditor.PlayMakerActions
{
    [CustomActionEditor(typeof(DrawRect))]
    public class DrawRectEditor : PaintActionBaseEditor<DrawRect>
    {
        public override bool OnGUI()
        {
            bool isDirty = false;
            DoDrawBasicFields();
            EditField("endPaintingPosition");
            EditField("isFilled");
            EditField("is9Sliced");
            isDirty |= DoDrawPatternFields();
            isDirty |= GUI.changed;
            if (isDirty)
                RecalculateDebugPoints(m_action.startPaintingPosition.Value, m_action.endPaintingPosition.Value, Rect);

            return isDirty;
        }

        void Rect(int x1, int y1, int x2, int y2, TilemapDrawingUtils.PlotFunction plot)
        {
            TilemapDrawingUtils.Rect(x1, y1, x2, y2, false, plot);
        }

        public override void OnSceneGUI()
        {
            base.OnSceneGUI();
            DrawDebugPoints();
        }
    }
}
#endif