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
    [Tooltip("Gets the tile vertex color")]
    //[HelpUrl("")]
    public class GetTileColor : PositionalActionBase
    {
        [UIHint(UIHint.Variable)]
        public FsmColor tileColor;

        public override void Reset()
        {
            base.Reset();
            tileColor = Color.white;
        }

        public override void OnEnter()
        {
            DoAction();
            Finish();
        }

        protected void DoAction()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (UpdateCache(go))
            {
                STETilemap tilemap = cachedComponent as STETilemap;
                TileColor32 tileColor32;
                if ((ePositionType)positionType.Value == ePositionType.LocalPosition)
                    tileColor32 = tilemap.GetTileColor(position.Value);
                else// if ((ePositionType)positionType.Value == ePositionType.GridPosition)
                    tileColor32 = tilemap.GetTileColor((int)position.Value.x, (int)position.Value.y);
                tileColor.Value = tileColor32.c0;
            }
        }
    }
}
#endif