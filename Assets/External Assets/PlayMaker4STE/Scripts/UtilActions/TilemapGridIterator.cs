#if PLAYMAKER && SUPER_TILEMAP_EDITOR
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;
using HutongGames.PlayMaker.Actions;

namespace CreativeSpore.SuperTilemapEditor.PlayMakerActions
{
    [ActionCategory("STE: Utility")]
    [Tooltip("Iterate through the cells in a tilemap, storing the current cell position in a variable.")]
    //[HelpUrl("")]
    public class TilemapGridIterator : TilemapActionBase
    {
        [HutongGames.PlayMaker.ActionSection("Output")]

        [UIHint(UIHint.Variable)]
        [Tooltip("The current cell position.")]
        public FsmVector2 storePosition;

        [UIHint(UIHint.Variable)]
        [Tooltip("Current Grid X position")]
        public FsmInt gridX;

        [UIHint(UIHint.Variable)]
        [Tooltip("Current Grid Y position")]
        public FsmInt gridY;

        [HutongGames.PlayMaker.ActionSection("Events")]
        [Tooltip("Event to send each iteration.")]
        public FsmEvent iterationNextElement;

        [HutongGames.PlayMaker.ActionSection("Tilemap Position")]
        [ObjectType(typeof(ePositionType))]
        [Tooltip(CommonTooltips.k_PositionType)]
        public FsmEnum positionType;

        private STETilemap m_tilemap;

        public override void Reset()
        {
            base.Reset();
            positionType = null;
            storePosition = null;
            gridX = null;
            gridY = null;
        }

        public override void OnEnter()
        {
            if(!m_tilemap)
            {
                var go = Fsm.GetOwnerDefaultTarget(gameObject);
                if (UpdateCache(go))
                {
                    m_tilemap = cachedComponent as STETilemap;
                    gridX.Value = m_tilemap.MinGridX - 1; //NOTE: OnEnter will add 1 the first time
                    gridY.Value = m_tilemap.MinGridY;
                    storePosition.Value = (ePositionType)positionType.Value == ePositionType.LocalPosition ?
                    (Vector2)TilemapUtils.GetGridWorldPos(m_tilemap, gridX.Value, gridY.Value)
                    :
                    new Vector2(gridX.Value, gridY.Value);
                }
            }
            
            if (gridX.Value < m_tilemap.MaxGridX) ++gridX.Value;
            else if (gridY.Value < m_tilemap.MaxGridY)
            {
                ++gridY.Value;
                gridX.Value = m_tilemap.MinGridX;
            }
            else
            {
                Finish();
                return;
            }

            storePosition.Value = (ePositionType)positionType.Value == ePositionType.LocalPosition?
                (Vector2)TilemapUtils.GetGridWorldPos(m_tilemap, gridX.Value, gridY.Value)
                :
                new Vector2(gridX.Value, gridY.Value);

            Fsm.Event(iterationNextElement);
        }

    }
}
#endif