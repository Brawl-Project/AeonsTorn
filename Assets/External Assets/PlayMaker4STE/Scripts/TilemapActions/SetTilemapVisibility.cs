#if PLAYMAKER && SUPER_TILEMAP_EDITOR
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;
using HutongGames.PlayMaker.Actions;

namespace CreativeSpore.SuperTilemapEditor.PlayMakerActions
{
    [ActionCategory("STE: Tilemap")]
    [Tooltip("Changes the tilemap visibility")]
    //[HelpUrl("")]
    public class SetTilemapVisibility : TilemapActionBase
    {
        [Tooltip("The tilemap visibility")]
        public FsmBool visible;

        public override void Reset()
        {
            base.Reset();
            visible = true;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (UpdateCache(go))
            {
                STETilemap tilemap = cachedComponent as STETilemap;
                tilemap.IsVisible = visible.Value;
            }

            Finish();
        }

    }
}
#endif