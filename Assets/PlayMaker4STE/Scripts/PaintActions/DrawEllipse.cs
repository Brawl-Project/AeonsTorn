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
    [Tooltip("Draws an ellipse using a pattern over a tilemap")]
    //[HelpUrl("")]
    public class DrawEllipse : PaintActionBase
    {
        [Tooltip(CommonTooltips.k_Position)]
        public FsmVector2 endPaintingPosition;

        [Tooltip("Fills the interior of the ellipse")]
        public FsmBool isFilled;

        protected override void DoPaintAction()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (UpdateCache(go))
            {
                STETilemap tilemap = cachedComponent as STETilemap;

                if((ePositionType)positionType.Value == ePositionType.LocalPosition )
                {
                    TilemapDrawingUtils.DrawEllipse(tilemap as STETilemap, startPaintingPosition.Value, endPaintingPosition.Value, tileSelection.Get2DTileDataArray(), isFilled.Value, randomizePattern.Value);
                }
                else// if ((ePositionType)positionType.Value == ePositionType.GridPosition)
                {
                    int x0 = (int)startPaintingPosition.Value.x;
                    int y0 = (int)startPaintingPosition.Value.y;
                    int x1 = (int)endPaintingPosition.Value.x;
                    int y1 = (int)endPaintingPosition.Value.y;
                    TilemapDrawingUtils.DrawEllipse(tilemap as STETilemap, x0, y0, x1, y1, tileSelection.Get2DTileDataArray(), isFilled.Value, randomizePattern.Value);
                }
                tilemap.UpdateMesh();
            }
        }
    }
}
#endif