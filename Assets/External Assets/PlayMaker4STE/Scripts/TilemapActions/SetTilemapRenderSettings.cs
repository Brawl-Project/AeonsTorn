#if PLAYMAKER && SUPER_TILEMAP_EDITOR
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;
using HutongGames.PlayMaker.Actions;

namespace CreativeSpore.SuperTilemapEditor.PlayMakerActions
{
    [ActionCategory("STE: Tilemap")]
    [Tooltip("Changes the renderer settings of a tilemap")]
    //[HelpUrl("")]
    public class SetTilemapRenderSettings : TilemapActionBase
    {
        [Tooltip("The tilemap material")]
        public FsmMaterial material;
        [Tooltip("The tilemap tint color")]
        public FsmColor tintColor;
        [Tooltip("The tilemap parallax factor")]
        public FsmVector2 parallaxFactor;
        [Tooltip("Enables pixel snap property used by Sprite/Default and Sprite/Diffuse materials")]
        public FsmBool pixelSnap;
        [UIHint(UIHint.SortingLayer)]
        [Tooltip("The tilemap sorting layer")]
        public FsmString sortingLayer;
        [Tooltip("The tilemap sorting order")]
        public FsmInt orderInLayer;
        [Tooltip("The tilemap inner padding. Extrude the tile uv rect a little to fix line artifacts.")]
        public FsmFloat innerPadding;
        [Tooltip("The tilemap visibility")]
        public FsmBool visible;

        public override void Reset()
        {
            base.Reset();
            material = new FsmMaterial() { Value = TilemapUtils.FindDefaultSpriteMaterial() };
            tintColor = Color.white;
            parallaxFactor = Vector2.one;
            pixelSnap = false;
            sortingLayer = "Default";
            orderInLayer = 0;
            innerPadding = 0f;
            visible = true;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (UpdateCache(go))
            {
                STETilemap tilemap = cachedComponent as STETilemap;
                if(!material.IsNone) tilemap.Material = material.Value;
                if (!tintColor.IsNone) tilemap.TintColor = tintColor.Value;
                if (!parallaxFactor.IsNone) tilemap.ParallaxFactor = parallaxFactor.Value;
                if (!pixelSnap.IsNone) tilemap.PixelSnap = pixelSnap.Value;
                if (!sortingLayer.IsNone) tilemap.SortingLayerName = sortingLayer.Value;
                if (!orderInLayer.IsNone) tilemap.OrderInLayer = orderInLayer.Value;
                if (!innerPadding.IsNone) tilemap.InnerPadding = innerPadding.Value;
                if (!visible.IsNone) tilemap.IsVisible = visible.Value;
            }

            Finish();
        }

    }
}
#endif