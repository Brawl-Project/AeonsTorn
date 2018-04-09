#if PLAYMAKER && SUPER_TILEMAP_EDITOR

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;
using HutongGames.PlayMaker.Actions;

namespace CreativeSpore.SuperTilemapEditor.PlayMakerActions
{
    [ActionTarget(typeof(STETilemap))]
    [ActionCategory("STE: Color Painting")]
    [Tooltip("Clear the color channel with a single color")]
    //[HelpUrl("")]
    public class ClearColorChannel : TilemapActionBase
    {

        public FsmColor clearColor;

        public override void Reset()
        {
            base.Reset();
            clearColor = Color.white;
        }

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
                tilemap.ClearColorChannel(clearColor.Value);
                tilemap.UpdateMesh();
            }
        }
    }
}
#endif