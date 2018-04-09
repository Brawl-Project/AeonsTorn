#if PLAYMAKER && SUPER_TILEMAP_EDITOR
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;
using HutongGames.PlayMaker.Actions;
using System.Collections.Generic;
using System.Linq;

namespace CreativeSpore.SuperTilemapEditor.PlayMakerActions
{
    public enum ePositionType
    {
        GridPosition,
        LocalPosition,
    }

    public abstract class PositionalActionBase : TilemapActionBase
    {
        [HutongGames.PlayMaker.ActionSection("Tilemap Position")]
        [ObjectType(typeof(ePositionType))]
        [Tooltip(CommonTooltips.k_PositionType)]
        public FsmEnum positionType;

        [Tooltip(CommonTooltips.k_Position)]
        public FsmVector2 position;
    }
}
#endif