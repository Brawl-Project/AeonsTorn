#if PLAYMAKER && SUPER_TILEMAP_EDITOR
using UnityEngine;
using System.Collections;
using System.Linq;
using HutongGames.PlayMakerEditor;
using UnityEditor;
using HutongGames.PlayMaker;

namespace CreativeSpore.SuperTilemapEditor.PlayMakerActions
{
    [CustomActionEditor(typeof(DrawLineMirrored))]
    public class DrawLineMirroredEditor : DrawLineEditor
    {
        protected override void DrawDebugPoints()
        {
            if (m_debugPoints.Count == 0) return;
            STETilemap tilemap = GetTargetTilemap();
            Vector2 cellSizeDiv2 = tilemap.CellSize / 2f;
            Vector2 realCellSizeDiv2 = tilemap.transform.TransformPoint(cellSizeDiv2);
            float size = Mathf.Min(realCellSizeDiv2.x, realCellSizeDiv2.y);
            Vector2 s0 = m_action.startPaintingPosition.Value;
            Vector2 s1 = m_action.endPaintingPosition.Value;
            Vector2 dir0 = s1 - s0;
            Vector2 dir1 = m_debugPoints[m_debugPoints.Count - 1] - m_debugPoints[0];
            int idx = Mathf.Sign(dir0.x) != Mathf.Sign(dir1.x) || Mathf.Sign(dir0.y) != Mathf.Sign(dir1.y) ?
                m_debugPoints.Count - 1
                :
                0;
            Vector2 c0 = tilemap.transform.TransformPoint(m_debugPoints[idx]);
            for (int i = 0; i < m_debugPoints.Count; ++i)
            {
                Vector2 p0 = tilemap.transform.TransformPoint(m_debugPoints[i]);
                EditorCompatibilityUtils.CircleCap(0, p0, Quaternion.identity, size);
                //Handles.Label(p0, i.ToString());
                if(i != idx)
                    EditorCompatibilityUtils.CircleCap(0, 2 * c0 - p0, Quaternion.identity, size);
            }
        }
    }
}
#endif