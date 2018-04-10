#if PLAYMAKER && SUPER_TILEMAP_EDITOR
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;
using HutongGames.PlayMaker.Actions;

namespace CreativeSpore.SuperTilemapEditor.PlayMakerActions
{
    [ActionCategory("STE: Tilemap")]
    [Tooltip("Changes the tilemap material")]
    //[HelpUrl("")]
    public class SetTilemapMaterial : TilemapActionBase
    {
        [Tooltip("The tilemap material")]
        public FsmMaterial material;        

        public override void Reset()
        {
            base.Reset();
            material = new FsmMaterial() { Value = TilemapUtils.FindDefaultSpriteMaterial() };
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (UpdateCache(go))
            {
                STETilemap tilemap = cachedComponent as STETilemap;
                tilemap.Material = material.Value;
            }

            Finish();
        }

    }
}
#endif