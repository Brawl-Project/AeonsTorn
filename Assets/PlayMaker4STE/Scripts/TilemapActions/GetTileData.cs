#if PLAYMAKER && SUPER_TILEMAP_EDITOR
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;
using HutongGames.PlayMaker.Actions;

namespace CreativeSpore.SuperTilemapEditor.PlayMakerActions
{
    [ActionCategory("STE: Tilemap")]
    [Tooltip("Gets the tile data at some position in a tilemap")]
    //[HelpUrl("")]
    public class GetTileData : PositionalActionBase
    {
        [HutongGames.PlayMaker.ActionSection("Output")]

        [UIHint(UIHint.Variable)]
        [Tooltip("The tile id found. Will be -1 if no tile is found.")]
        public FsmInt tileId;

        [UIHint(UIHint.Variable)]
        [Tooltip("The brush id found. Will be -1 if no tile is found.")]
        public FsmInt brushId;

        [UIHint(UIHint.Variable)]
        [Tooltip("The flags for this tile.")]
        public FsmInt tileFlags;

        public override void Reset()
        {
            base.Reset();
            positionType = null;
            position = null;
            tileId = null;
            brushId = null;
            tileFlags = null;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            uint tileData = Tileset.k_TileData_Empty;
            if (UpdateCache(go))
            {
                STETilemap tilemap = cachedComponent as STETilemap;
                if ((ePositionType)positionType.Value == ePositionType.LocalPosition)
                {
                    tileData = tilemap.GetTileData(position.Value);
                }
                else// if ((ePositionType)positionType.Value == ePositionType.GridPosition)
                {
                    tileData = tilemap.GetTileData((int)position.Value.x, (int)position.Value.y);
                }
            }
            tileId.Value = Tileset.GetTileIdFromTileData(tileData);
            brushId.Value = Tileset.GetBrushIdFromTileData(tileData);
            tileFlags.Value = (int)Tileset.GetTileFlagsFromTileData(tileData);

            Finish();
        }

    }
}
#endif