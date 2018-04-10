#if PLAYMAKER && SUPER_TILEMAP_EDITOR
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;
using HutongGames.PlayMaker.Actions;

namespace CreativeSpore.SuperTilemapEditor.PlayMakerActions
{
    [ActionCategory("STE: Utility")]
    [Tooltip("Uses Mathf.PerlinNoise")]
    //[HelpUrl("")]
    public class MathPerlinNoise : FsmStateAction
    {
        [HutongGames.PlayMaker.ActionSection("Output")]

        [UIHint(UIHint.Variable)]
        [Tooltip("The calculated perlin value.")]
        public FsmFloat perlinValue;

        [HutongGames.PlayMaker.ActionSection("Parameters")]
        [Tooltip("Position input")]
        public FsmVector2 inputPosition;

        [Tooltip("Scale multiplied to the inputPosition.")]
        public FsmFloat scale;

        [Tooltip("Offset added to the inputPosition after multiplying the scale.")]
        public FsmVector2 offset;

        public override void Reset()
        {
            base.Reset();
            inputPosition = null;
            scale = 1f;
            offset = null;
        }

        public override void OnEnter()
        {
            perlinValue.Value = Mathf.PerlinNoise(offset.Value.x + inputPosition.Value.x * scale.Value, offset.Value.y + inputPosition.Value.y * scale.Value);
            Finish();
        }
    }
}
#endif