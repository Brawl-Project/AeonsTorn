#if PLAYMAKER && SUPER_TILEMAP_EDITOR
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;
using HutongGames.PlayMaker.Actions;
using System.Collections.Generic;
using System.Linq;

namespace CreativeSpore.SuperTilemapEditor.PlayMakerActions
{
    public abstract class TileColorActionBase : PositionalActionBase
    {
        [ObjectType(typeof(eTileColorPaintMode))]
        [Tooltip("Tile mode will change the 4 vertices of the tile using the same color; Vertex will take into account each vertex of the tile separately.")]
        public FsmEnum tileColorPaintMode;

        [ObjectType(typeof(eBlendMode))]
        [Tooltip("How the color is blended with the previous tile color")]
        public FsmEnum blendingMode;

        public FsmColor tileColor;

        [Tooltip("If true, this action will be applied to all the tilemaps in the same tilemap group the target tilemap belongs to.")]
        public FsmBool applyToTilemapGroup;

        public override void Reset()
        {
            base.Reset();
            tileColorPaintMode = eTileColorPaintMode.Tile;
            blendingMode = eBlendMode.AlphaBlending;
            tileColor = Color.white;
            applyToTilemapGroup = false;
        }
    }
}
#endif