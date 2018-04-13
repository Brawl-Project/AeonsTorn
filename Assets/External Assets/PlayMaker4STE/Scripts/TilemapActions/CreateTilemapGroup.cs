#if PLAYMAKER && SUPER_TILEMAP_EDITOR
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

namespace CreativeSpore.SuperTilemapEditor.PlayMakerActions
{
    [ActionCategory("STE: Tilemap")]
    [Tooltip("Creates a tilemap group")]
    //[HelpUrl("")]
    public class CreateTilemapGroup : FsmStateAction
    {

        [UIHint(UIHint.Variable)]
        [Tooltip("New Tilemap Group game object or the one where the component will be attached to")]
        public FsmOwnerDefault gameObject;

        [Tooltip("The name of the tilemap group")]
        public FsmString name;

        [HutongGames.PlayMaker.ActionSection("Output")]

        [ObjectType(typeof(STETilemap)), UIHint(UIHint.Variable)]
        [Tooltip("New tilemap group component")]
        public FsmObject tilemapGroupComponent;


        public override void Reset()
        {
            name = "";
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (!go) go = new GameObject();
            if (!name.IsNone) go.name = name.Value;
            var tilemapGroupComp = go.AddComponent<TilemapGroup>();

            gameObject.GameObject.Value = go;
            tilemapGroupComponent.Value = tilemapGroupComp;

            Finish();
        }

    }
}
#endif