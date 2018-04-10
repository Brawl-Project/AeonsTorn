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
    [Tooltip("Remove the color channel of a tilemap")]
    //[HelpUrl("")]
    public class RemoveColorChannel : TilemapActionBase
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
                tilemap.RemoveColorChannel();
                tilemap.UpdateMesh();
            }
        }
    }
}
#endif