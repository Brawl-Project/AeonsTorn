#if PLAYMAKER && SUPER_TILEMAP_EDITOR
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;
using HutongGames.PlayMaker.Actions;

namespace CreativeSpore.SuperTilemapEditor.PlayMakerActions
{
    [ActionCategory("STE: Tilemap")]
    [Tooltip("Changes the tilemap tint color")]
    //[HelpUrl("")]
    public class SetTilemapTintColor : TilemapActionBase
    {
        [Tooltip("The tilemap tint color")]
        public FsmColor tintColor;        

        public override void Reset()
        {
            base.Reset();
            tintColor = Color.white;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (UpdateCache(go))
            {
                STETilemap tilemap = cachedComponent as STETilemap;
                tilemap.TintColor = tintColor.Value;
            }

            Finish();
        }

    }
}
#endif