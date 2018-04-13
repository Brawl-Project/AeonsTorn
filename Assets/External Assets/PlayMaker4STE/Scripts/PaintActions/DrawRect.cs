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
    [Tooltip("Draws a rectangle using a pattern over a tilemap")]
    //[HelpUrl("")]
    public class DrawRect : PaintActionBase
    {
        [Tooltip(CommonTooltips.k_Position)]
        public FsmVector2 endPaintingPosition;

        [Tooltip("Fills the interior of the rectangle")]
        public FsmBool isFilled;

        [Tooltip("Draws the pattern using 9 slice mode")]
        public FsmBool is9Sliced;

        protected override void DoPaintAction()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (UpdateCache(go))
            {
                STETilemap tilemap = cachedComponent as STETilemap;

                if((ePositionType)positionType.Value == ePositionType.LocalPosition )
                {
                    TilemapDrawingUtils.DrawRect(tilemap as STETilemap, startPaintingPosition.Value, endPaintingPosition.Value, tileSelection.Get2DTileDataArray(), isFilled.Value, is9Sliced.Value, randomizePattern.Value);
                }
                else// if ((ePositionType)positionType.Value == ePositionType.GridPosition)
                {
                    int x0 = (int)startPaintingPosition.Value.x;
                    int y0 = (int)startPaintingPosition.Value.y;
                    int x1 = (int)endPaintingPosition.Value.x;
                    int y1 = (int)endPaintingPosition.Value.y;
                    TilemapDrawingUtils.DrawRect(tilemap as STETilemap, x0, y0, x1, y1, tileSelection.Get2DTileDataArray(), isFilled.Value, is9Sliced.Value, randomizePattern.Value);
                }
                tilemap.UpdateMesh();
            }
        }
    }
}
#endif