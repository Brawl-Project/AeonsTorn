#if PLAYMAKER && SUPER_TILEMAP_EDITOR
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;
using HutongGames.PlayMaker.Actions;

namespace CreativeSpore.SuperTilemapEditor.PlayMakerActions
{
    [ActionCategory("STE: Tilemap")]
    [Tooltip("Gets the parameter value of a tile at some position in a tilemap")]
    //[HelpUrl("")]
    public class GetTileBoolParameter : PositionalActionBase
    {
        [HutongGames.PlayMaker.ActionSection("Output")]

        [UIHint(UIHint.Variable)]
        [Tooltip("The value of the parameter.")]
        public FsmBool parameterValue;

        [HutongGames.PlayMaker.ActionSection("Input")]
        [Tooltip("The parameter name.")]
        public FsmString parameterName;

        [Tooltip("The default parameter name if no parameter was set.")]
        public FsmBool defaultValue;


        public override void Reset()
        {
            base.Reset();
            positionType = null;
            position = null;
            parameterName = null;
            defaultValue = null;
            parameterValue = null;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            Tile tile = null;
            if (UpdateCache(go))
            {
                STETilemap tilemap = cachedComponent as STETilemap;
                if ((ePositionType)positionType.Value == ePositionType.LocalPosition)
                {
                    tile = tilemap.GetTile(position.Value);
                }
                else// if ((ePositionType)positionType.Value == ePositionType.GridPosition)
                {
                    tile = tilemap.GetTile((int)position.Value.x, (int)position.Value.y);
                }
                if (tile == null)
                    parameterValue.Value = defaultValue.Value;
                else
                    parameterValue.Value = tile.paramContainer.GetBoolParam(parameterName.Value, defaultValue.Value);
            }

            Finish();
        }

    }
}
#endif