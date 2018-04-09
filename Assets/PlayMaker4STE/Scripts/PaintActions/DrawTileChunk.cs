#if PLAYMAKER && SUPER_TILEMAP_EDITOR
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;
using HutongGames.PlayMaker.Actions;

namespace CreativeSpore.SuperTilemapEditor.PlayMakerActions
{
    [ActionCategory("STE: Painting")]
    [Tooltip("Draws a pattern centered in the start painting position.")]
    //[HelpUrl("")]
    public class DrawTileChunk : PaintActionBase
    {

        public override void OnEnter()
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

                if (tileSelection.selectionData.Length == 1)
                    tilemap.SetTileData(gridX, gridY, (uint)tileSelection.selectionData[0]);
                else
                {
                    int w = tileSelection.rowLength;
                    int h = tileSelection.columnLength;
                    int xf = -(w >> 1) + 1 - (w & 1);
                    int yf = -(h >> 1) + 1 - (h & 1);
                    for (int y = h - 1, idx = 0; y >= 0; --y)
                        for (int x = 0; x < w; ++x, ++idx)
                            tilemap.SetTileData(gridX + x + xf, gridY + y + yf, (uint)tileSelection.selectionData[idx]);
                }
                

                tilemap.UpdateMesh();
            }

            Finish();
        }

    }
}
#endif