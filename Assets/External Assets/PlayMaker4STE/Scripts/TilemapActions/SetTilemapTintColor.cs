#if PLAYMAKER && SUPER_TILEMAP_EDITOR
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;
using HutongGames.PlayMaker.Actions;

namespace CreativeSpore.SuperTilemapEditor.PlayMakerActions
{
    [ActionCategory("STE: Tilemap")]
    [Tooltip("Changes the tilemap parallax factor")]
    //[HelpUrl("")]
    public class SetTilemapParallaxFactor : TilemapActionBase
    {
        [Tooltip("The tilemap parallax factor")]
        public FsmVector2 parallaxFactor;        

        public override void Reset()
        {
            base.Reset();
            parallaxFactor = Vector2.one;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (UpdateCache(go))
            {
                STETilemap tilemap = cachedComponent as STETilemap;
                tilemap.ParallaxFactor = parallaxFactor.Value;
            }

            Finish();
        }

    }
}
#endif