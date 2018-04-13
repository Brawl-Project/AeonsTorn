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
    [Tooltip("Paints the tilemap tiles with a circle shape")]
    //[HelpUrl("")]
    public class TilemapVertexPaintCircle : TileColorActionBase
    {        
        public FsmAnimationCurve intensityCurve;
        public FsmFloat radius;

        public override void Reset()
        {
            base.Reset();
            intensityCurve = new FsmAnimationCurve()
            {
                curve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f, 2f, 2f), new Keyframe(1f, 1f) })
            };
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
                Vector2 center;
                if ((ePositionType)positionType.Value == ePositionType.LocalPosition)
                    center = position.Value;
                else// if ((ePositionType)positionType.Value == ePositionType.GridPosition)
                    center = TilemapUtils.GetTileCenterPosition(tilemap, (int)position.Value.x, (int)position.Value.y);
                if (applyToTilemapGroup.Value && tilemap.ParentTilemapGroup)
                {
                    tilemap.ParentTilemapGroup.IterateTilemapWithAction(
                        (STETilemap tmap) =>
                        {
                            TilemapVertexPaintUtils.VertexPaintCircle(tmap, center, radius.Value, tileColor.Value, (eBlendMode)blendingMode.Value, (eTileColorPaintMode)tileColorPaintMode.Value == eTileColorPaintMode.Vertex, intensityCurve.curve);
                            tmap.UpdateMesh();
                        }
                        );
                }
                else
                {
                    TilemapVertexPaintUtils.VertexPaintCircle(tilemap, center, radius.Value, tileColor.Value, (eBlendMode)blendingMode.Value, (eTileColorPaintMode)tileColorPaintMode.Value == eTileColorPaintMode.Vertex, intensityCurve.curve);
                    tilemap.UpdateMesh();
                }
            }
        }        
    }
}
#endif