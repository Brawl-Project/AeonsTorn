#if PLAYMAKER && SUPER_TILEMAP_EDITOR
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;
using HutongGames.PlayMaker.Actions;

namespace CreativeSpore.SuperTilemapEditor.PlayMakerActions
{
    [ActionCategory("STE: Tilemap")]
    [Tooltip("Force an update of all tilechunks. This is called after changing sensitive data like CellSize, Collider depth, etc")]
    //[HelpUrl("")]
    public class RefreshTilemap : TilemapActionBase
    {        
        [Tooltip("Refresh the rendered tiles")]
        public FsmBool refreshRenderMesh;
        [Tooltip("Refresh the collider mesh")]
        public FsmBool refreshColliderMesh;
        [Tooltip("Refresh the objects attached to any tile, including restoring all its properties like position, scale, etc")]
        public FsmBool refreshTileObjects;
        [Tooltip("Invalidate all brushes. All tiles painted using a brush will be updated. For example a random brush could change the tile again.")]
        public FsmBool invalidateBrushes;

        public override void Reset()
        {
            base.Reset();
            refreshRenderMesh = true;
            refreshColliderMesh = true;
            refreshTileObjects = false;
            invalidateBrushes = false;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (UpdateCache(go))
            {
                STETilemap tilemap = cachedComponent as STETilemap;
                tilemap.Refresh(refreshRenderMesh.Value, refreshColliderMesh.Value, refreshTileObjects.Value, invalidateBrushes.Value);
            }

            Finish();
        }

    }
}
#endif