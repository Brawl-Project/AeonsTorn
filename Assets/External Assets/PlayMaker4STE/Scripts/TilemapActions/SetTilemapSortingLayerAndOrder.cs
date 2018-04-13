#if PLAYMAKER && SUPER_TILEMAP_EDITOR
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;
using HutongGames.PlayMaker.Actions;

namespace CreativeSpore.SuperTilemapEditor.PlayMakerActions
{
    [ActionCategory("STE: Tilemap")]
    [Tooltip("Changes the tilemap sorting layer and order")]
    //[HelpUrl("")]
    public class SetTilemapSortingLayerAndOrder : TilemapActionBase
    {
        [UIHint(UIHint.SortingLayer)]
        [Tooltip("The tilemap sorting layer")]
        public FsmString sortingLayer;
        [Tooltip("The tilemap sorting order")]
        public FsmInt orderInLayer; 

        public override void Reset()
        {
            base.Reset();
            sortingLayer = "Default";
            orderInLayer = 0;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (UpdateCache(go))
            {
                STETilemap tilemap = cachedComponent as STETilemap;
                if (!sortingLayer.IsNone) tilemap.SortingLayerName = sortingLayer.Value;
                if (!orderInLayer.IsNone) tilemap.OrderInLayer = orderInLayer.Value;
            }

            Finish();
        }

    }
}
#endif