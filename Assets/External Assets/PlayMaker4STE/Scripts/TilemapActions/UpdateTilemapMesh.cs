#if PLAYMAKER && SUPER_TILEMAP_EDITOR
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;
using HutongGames.PlayMaker.Actions;

namespace CreativeSpore.SuperTilemapEditor.PlayMakerActions
{
    [ActionCategory("STE: Tilemap")]
    [Tooltip("Updates the render mesh and mesh collider of all tile chunks. This should be called once after making all modifications to the tilemap with SetTileData.")]
    //[HelpUrl("")]
    public class UpdateTilemapMesh : TilemapActionBase
    {
        [Tooltip("Force the update to be done immediately instead of during the next tilemap update")]
        public FsmBool forceUpdate;

        public override void Reset()
        {
            base.Reset();
            forceUpdate = false;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (UpdateCache(go))
            {
                STETilemap tilemap = cachedComponent as STETilemap;
                if (forceUpdate.Value)
                    tilemap.UpdateMeshImmediate();
                else
                    tilemap.UpdateMesh();
            }

            Finish();
        }

    }
}
#endif