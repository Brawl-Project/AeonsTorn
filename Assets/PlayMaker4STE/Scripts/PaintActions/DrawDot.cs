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
    [Tooltip("Draws a dot using a pattern over a tilemap")]
    //[HelpUrl("")]
    public class DrawDot : PaintActionBase
    {
        protected override void DoPaintAction()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (UpdateCache(go))
            {
                STETilemap tilemap = cachedComponent as STETilemap;

                if((ePositionType)positionType.Value == ePositionType.LocalPosition )
                {
                    TilemapDrawingUtils.DrawDot(tilemap as STETilemap, startPaintingPosition.Value, tileSelection.Get2DTileDataArray(), randomizePattern.Value);
                }
                else// if ((ePositionType)positionType.Value == ePositionType.GridPosition)
                {
                    int x0 = (int)startPaintingPosition.Value.x;
                    int y0 = (int)startPaintingPosition.Value.y;
                    TilemapDrawingUtils.DrawDot(tilemap as STETilemap, x0, y0, tileSelection.Get2DTileDataArray(), randomizePattern.Value);
                }
                tilemap.UpdateMesh();
            }
        }
    }
}
#endif