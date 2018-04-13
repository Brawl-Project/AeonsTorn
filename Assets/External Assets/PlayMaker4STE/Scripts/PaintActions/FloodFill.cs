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
    [Tooltip("Use the flood fill tool over a tilemap")]
    //[HelpUrl("")]
    public class FloodFill : PaintActionBase
    {
        protected override void DoPaintAction()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (UpdateCache(go))
            {
                STETilemap tilemap = cachedComponent as STETilemap;
                int gridX;
                int gridY;
                if((ePositionType)positionType.Value == ePositionType.LocalPosition )
                {
                    gridX = TilemapUtils.GetGridX(tilemap, startPaintingPosition.Value);
                    gridY = TilemapUtils.GetGridY(tilemap, startPaintingPosition.Value);
                }
                else// if ((ePositionType)positionType.Value == ePositionType.GridPosition)
                {
                    gridX = (int)startPaintingPosition.Value.x;
                    gridY = (int)startPaintingPosition.Value.y;
                }
                TilemapDrawingUtils.FloodFill(tilemap as STETilemap, gridX, gridY, tileSelection.Get2DTileDataArray(), randomizePattern.Value);
                tilemap.UpdateMesh();
            }
        }
    }
}
#endif