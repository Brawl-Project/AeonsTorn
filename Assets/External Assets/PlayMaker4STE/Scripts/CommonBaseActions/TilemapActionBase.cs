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
    public abstract class TilemapActionBase : ComponentAction<STETilemap>
    {
        [HutongGames.PlayMaker.ActionSection("Tilemap Target")]
        [RequiredField]
        [CheckForComponent(typeof(STETilemap))]
        [Tooltip("The game object where the tilemap component is located.")]
        public FsmOwnerDefault gameObject;

        public override void Reset()
        {
            gameObject = State != null ? new FsmOwnerDefault(State.Actions.TakeWhile(x => x != this).Where(x => x is TilemapActionBase).Select(x => (x as TilemapActionBase).gameObject).LastOrDefault()) : null;
        }
    }
}
#endif