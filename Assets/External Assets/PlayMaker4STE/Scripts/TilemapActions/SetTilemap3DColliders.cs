#if PLAYMAKER && SUPER_TILEMAP_EDITOR
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;
using HutongGames.PlayMaker.Actions;

namespace CreativeSpore.SuperTilemapEditor.PlayMakerActions
{
    [ActionCategory("STE: Tilemap")]
    [Tooltip("Sets the tilemap 3D colliders")]
    //[HelpUrl("")]
    public class SetTilemap3DColliders : TilemapActionBase
    {
        [Tooltip("The depth size of the collider. You need to call Refresh(false, true) after changing this value to refresh the collider")]
        public FsmFloat colliderDepth;

        [ObjectType(typeof(PhysicMaterial))]
        [Tooltip("The PhysicsMaterial that is applied to this tilemap colliders.")]
        public FsmObject physicMaterial;

        [Tooltip("Sets the isTrigger property of the collider. You need call Refresh to update the colliders after changing it.")]
        public FsmBool isTrigger;

        public override void Reset()
        {
            base.Reset();
            colliderDepth = 0.1f;
            isTrigger = false;
            physicMaterial = null;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (UpdateCache(go))
            {
                STETilemap tilemap = cachedComponent as STETilemap;
                tilemap.ColliderType = eColliderType._2D;
                tilemap.ColliderDepth = colliderDepth.Value;
                tilemap.PhysicMaterial = physicMaterial.Value as PhysicMaterial;
                tilemap.IsTrigger = isTrigger.Value;
                tilemap.Refresh(false, true);
            }

            Finish();
        }

    }
}
#endif