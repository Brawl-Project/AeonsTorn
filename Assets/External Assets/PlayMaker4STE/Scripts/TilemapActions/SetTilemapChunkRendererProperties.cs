#if PLAYMAKER && SUPER_TILEMAP_EDITOR
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;
using HutongGames.PlayMaker.Actions;
using UnityEngine.Rendering;

namespace CreativeSpore.SuperTilemapEditor.PlayMakerActions
{
    [ActionCategory("STE: Tilemap")]
    [Tooltip("Changes the renderer properties of all the chunks in a tilemap")]
    //[HelpUrl("")]
    public class SetTilemapChunkRendererProperties : TilemapActionBase
    {
        [ObjectType(typeof(ShadowCastingMode))]
        public FsmEnum castShadows;
        public FsmBool receiveShadows;
#if UNITY_5_4_OR_NEWER
        [ObjectType(typeof(LightProbeUsage))]
        public FsmEnum useLightProbes;
#else
        public FsmBool useLightProbes;
#endif
        [ObjectType(typeof(ReflectionProbeUsage))]
        public FsmEnum reflextionProbesUsage;
         
        [ObjectType(typeof(Transform))]
        public FsmObject anchorOverride;

        public override void Reset()
        {
            base.Reset();
            castShadows = ShadowCastingMode.Off;
            receiveShadows = false;
#if UNITY_5_4_OR_NEWER
            useLightProbes = LightProbeUsage.Off;
#else
            useLightProbes = false;
#endif
            reflextionProbesUsage = ReflectionProbeUsage.Off;
            anchorOverride = null;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (UpdateCache(go))
            {
                STETilemap tilemap = cachedComponent as STETilemap;
                if(!castShadows.IsNone) tilemap.ChunkRendererProperties.castShadows = (ShadowCastingMode)castShadows.Value;
                if (!receiveShadows.IsNone) tilemap.ChunkRendererProperties.receiveShadows = receiveShadows.Value;
#if UNITY_5_4_OR_NEWER
                if(!useLightProbes.IsNone) tilemap.ChunkRendererProperties.useLightProbes = (LightProbeUsage)useLightProbes.Value;
#else
                if (!useLightProbes.IsNone) tilemap.ChunkRendererProperties.useLightProbes = useLightProbes.Value;
#endif
                if (!reflextionProbesUsage.IsNone) tilemap.ChunkRendererProperties.reflectionProbeUsage = (ReflectionProbeUsage)reflextionProbesUsage.Value;
                if (!anchorOverride.IsNone) tilemap.ChunkRendererProperties.anchorOverride = anchorOverride.Value as Transform;

                tilemap.UpdateChunkRenderereProperties();
            }

            Finish();
        }

    }
}
#endif