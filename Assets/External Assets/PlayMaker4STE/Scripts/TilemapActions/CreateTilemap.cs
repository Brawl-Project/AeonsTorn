#if PLAYMAKER && SUPER_TILEMAP_EDITOR
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;

namespace CreativeSpore.SuperTilemapEditor.PlayMakerActions
{
    [ActionCategory("STE: Tilemap")]
    [Tooltip("Creates a tilemap with the map bounds specified")]
    //[HelpUrl("")]
    public class CreateTilemap : FsmStateAction
    {

        [Tooltip("New Tilemap game object or the one where the component will be attached to")]
        public FsmOwnerDefault gameObject;

        [Tooltip("The name of the tilemap")]
        public FsmString name;
        [Tooltip("The parent for this tilemap")]
        public FsmOwnerDefault parentGameObject;
        [Tooltip("The tilemap material")]
        public FsmMaterial material;
        [UIHint(UIHint.SortingLayer)]
        [Tooltip("The tilemap sorting layer")]
        public FsmString sortingLayer;
        [Tooltip("The tilemap sorting order")]
        public FsmInt orderInLayer;
        [RequiredField, ObjectType(typeof(Tileset))]
        [Tooltip("The tileset for this tilemap")]
        public FsmObject tileset;        
        [Tooltip("Minimum grid X position")]
        public FsmInt minGridX;
        [Tooltip("Minimum grid Y position")]
        public FsmInt minGridY;
        [Tooltip("Maximum grid X position")]
        public FsmInt maxGridX;
        [Tooltip("Maximum grid Y position")]
        public FsmInt maxGridY;        

        [HutongGames.PlayMaker.ActionSection("Output")]

        [ObjectType(typeof(STETilemap)), UIHint(UIHint.Variable)]
        [Tooltip("New tilemap component")]
        public FsmObject tilemapComponent;

        public override void Reset()
        {
            name = "";
            tileset = null;
            //parentGameObject = new FsmOwnerDefault() { OwnerOption = OwnerDefaultOption.SpecifyGameObject,  GameObject = { UseVariable = true } };
            parentGameObject = null;
            material = new FsmMaterial() { Value = TilemapUtils.FindDefaultSpriteMaterial() };
            sortingLayer = "Default";
            orderInLayer = 0;
            minGridX = 0;
            minGridY = 0;
            maxGridX = 0;
            maxGridY = 0;
        }

        public override void OnEnter()
        {
            var parentGo = Fsm.GetOwnerDefaultTarget(parentGameObject);
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            //if (!go && gameObject.OwnerOption == OwnerDefaultOption.UseOwner) go = parentGo;
            if (!go) go = new GameObject(name.Value);
            var tilemapComp = go.AddComponent<STETilemap>();
            tilemapComp.Tileset = tileset.Value as Tileset;
            tilemapComp.Material = material.Value;
            tilemapComp.SortingLayerName = sortingLayer.Value;
            tilemapComp.OrderInLayer = orderInLayer.Value;
            tilemapComp.SetMapBounds(minGridX.Value,minGridY.Value, maxGridX.Value, maxGridY.Value);

            if (parentGo)
                go.transform.SetParent(parentGo.transform);
            gameObject.GameObject.Value = go;
            tilemapComponent.Value = tilemapComp;

            Finish();
        }

    }
}
#endif