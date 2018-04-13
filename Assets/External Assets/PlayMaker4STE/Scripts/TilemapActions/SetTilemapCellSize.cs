#if PLAYMAKER && SUPER_TILEMAP_EDITOR
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;
using HutongGames.PlayMaker.Actions;

namespace CreativeSpore.SuperTilemapEditor.PlayMakerActions
{
    [ActionCategory("STE: Tilemap")]
    [Tooltip("The size of the cell containing the tiles. You should call Refresh() after changing this value to apply the effect.")]
    //[HelpUrl("")]
    public class SetTilemapCellSize : TilemapActionBase
    {
        [Tooltip("The tilemap material")]
        public FsmVector2 cellSize;

        [Tooltip("If false, you should manually refresh the tilemap to see the changes.")]
        public FsmBool refreshTilemap;

        public override void Reset()
        {
            base.Reset();
            cellSize = Vector2.one;
            refreshTilemap = true;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (UpdateCache(go))
            {
                STETilemap tilemap = cachedComponent as STETilemap;
                tilemap.CellSize = cellSize.Value;
                if (refreshTilemap.Value)
                    tilemap.Refresh();
            }

            Finish();
        }

    }
}
#endif