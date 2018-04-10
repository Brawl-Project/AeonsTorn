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
    [CustomActionEditor(typeof(DrawEllipse))]
    public class DrawEllipseEditor : PaintActionBaseEditor<DrawEllipse>
    {        
        public override bool OnGUI()
        {
            bool isDirty = false;
            DoDrawBasicFields();
            EditField("endPaintingPosition");
            EditField("isFilled");
            isDirty |= DoDrawPatternFields();
            isDirty |= GUI.changed;
            if (isDirty)
                RecalculateDebugPoints(m_action.startPaintingPosition.Value, m_action.endPaintingPosition.Value, Ellipse);

            return isDirty;
        }

        void Ellipse(int x1, int y1, int x2, int y2, TilemapDrawingUtils.PlotFunction plot)
        {
            TilemapDrawingUtils.Ellipse(x1, y1, x2, y2, false, plot);
        }

        public override void OnSceneGUI()
        {
            base.OnSceneGUI();
            DrawDebugPoints();
        }
    }
}
#endif