#if PLAYMAKER && SUPER_TILEMAP_EDITOR
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;
using HutongGames.PlayMaker.Actions;
using System.Collections.Generic;

namespace CreativeSpore.SuperTilemapEditor.PlayMakerActions
{
    [ActionTarget(typeof(STETilemap))]
    [ActionCategory("STE: Painting")]
    [Tooltip("Like DrawLine, draws a line from the start position to the end position and another one from start position in the opposite direction.")]
    //[HelpUrl("")]
    public class DrawLineMirrored : DrawLine
    {
        protected override void DoPaintAction()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (UpdateCache(go))
            {
                STETilemap tilemap = cachedComponent as STETilemap;

                if((ePositionType)positionType.Value == ePositionType.LocalPosition )
                {
                    TilemapDrawingUtils.DrawLineMirrored(tilemap as STETilemap, startPaintingPosition.Value, endPaintingPosition.Value, tileSelection.Get2DTileDataArray(), randomizePattern.Value);
                }
                else// if ((ePositionType)positionType.Value == ePositionType.GridPosition)
                {
                    int x0 = (int)startPaintingPosition.Value.x;
                    int y0 = (int)startPaintingPosition.Value.y;
                    int x1 = (int)endPaintingPosition.Value.x;
                    int y1 = (int)endPaintingPosition.Value.y;
                    TilemapDrawingUtils.DrawLineMirrored(tilemap as STETilemap, x0, y0, x1, y1, tileSelection.Get2DTileDataArray(), randomizePattern.Value);
                }
            }
        }
    }
}
#endif