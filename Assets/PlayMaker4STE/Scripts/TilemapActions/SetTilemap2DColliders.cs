#if PLAYMAKER && SUPER_TILEMAP_EDITOR
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;
using HutongGames.PlayMaker.Actions;

namespace CreativeSpore.SuperTilemapEditor.PlayMakerActions
{
    [ActionCategory("STE: Tilemap")]
    [Tooltip("Sets the tilemap 2D colliders")]
    //[HelpUrl("")]
    public class SetTilemap2DColliders : TilemapActionBase
    {

        [ObjectType(typeof(e2DColliderType))]
        [Tooltip("The type of collider used when ColliderType is eColliderType._2D")]
        public FsmEnum collider2DType;

        [ObjectType(typeof(PhysicsMaterial2D))]
        [Tooltip("The PhysicsMaterial2D that is applied to this tilemap colliders.")]
        public FsmObject physicMaterial2D;

        [Tooltip("Sets the isTrigger property of the collider. You need call Refresh to update the colliders after changing it.")]
        public FsmBool isTrigger;

        public override void Reset()
        {
            base.Reset();
            collider2DType = e2DColliderType.EdgeCollider2D;
            isTrigger = false;
            physicMaterial2D = null;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (UpdateCache(go))
            {
                STETilemap tilemap = cachedComponent as STETilemap;
                tilemap.ColliderType = eColliderType._2D;
                tilemap.Collider2DType = (e2DColliderType)collider2DType.Value;
                tilemap.PhysicMaterial2D = physicMaterial2D.Value as PhysicsMaterial2D;
                tilemap.IsTrigger = isTrigger.Value;
                tilemap.Refresh(false, true);
            }

            Finish();
        }

    }
}
#endif