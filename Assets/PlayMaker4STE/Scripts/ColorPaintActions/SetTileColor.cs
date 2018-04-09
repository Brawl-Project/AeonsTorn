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
    [ActionCategory("STE: Color Painting")]
    [Tooltip("Sets the tile vertex color")]
    //[HelpUrl("")]
    public class SetTileColor : TileColorActionBase
    {
        public override void OnEnter()
        {
            DoColorPaintAction();
            Finish();
        }

        protected void DoColorPaintAction()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (UpdateCache(go))
            {
                STETilemap tilemap = cachedComponent as STETilemap;
                if ((ePositionType)positionType.Value == ePositionType.LocalPosition)
                    tilemap.SetTileColor(position.Value, tileColor.Value, (eBlendMode)blendingMode.Value);
                else// if ((ePositionType)positionType.Value == ePositionType.GridPosition)
                    tilemap.SetTileColor((int)position.Value.x, (int)position.Value.y, tileColor.Value, (eBlendMode)blendingMode.Value);
                tilemap.UpdateMesh();
            }
        }
    }
}
#endif