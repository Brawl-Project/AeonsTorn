#if PLAYMAKER && SUPER_TILEMAP_EDITOR
using UnityEngine;
using System.Collections;
using System.Linq;
using HutongGames.PlayMakerEditor;
using UnityEditor;
using HutongGames.PlayMaker;

namespace CreativeSpore.SuperTilemapEditor.PlayMakerActions
{
    [CustomActionEditor(typeof(DrawDot))]
    public class DrawDotEditor : PaintActionBaseEditor<DrawDot>
    {
        public override bool OnGUI()
        {
            bool isDirty = base.OnGUI();            
            if (isDirty)
                RecalculateDebugPoints(m_action.startPaintingPosition.Value, m_action.startPaintingPosition.Value, Line);
            return isDirty;
        }

        void Line(int x1, int y1, int x2, int y2, TilemapDrawingUtils.PlotFunction plot)
        {
            TilemapDrawingUtils.Line(x1, y1, x2, y2, plot);
        }

        public override void OnSceneGUI()
        {
            base.OnSceneGUI();
            DrawDebugPoints();
        }
    }
}
#endif