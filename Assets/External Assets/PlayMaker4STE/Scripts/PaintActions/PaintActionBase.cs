#if PLAYMAKER && SUPER_TILEMAP_EDITOR
using UnityEngine;
using System.Collections;
using System.Linq;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;
using HutongGames.PlayMaker.Actions;
using System.Collections.Generic;

namespace CreativeSpore.SuperTilemapEditor.PlayMakerActions
{
    public abstract class PaintActionBase : TilemapActionBase
    {        
        [RequiredField]
        [ObjectType(typeof(Tileset))]
        public FsmObject tileset;

        [ObjectType(typeof(ePositionType))]
        [Tooltip(CommonTooltips.k_PositionType)]
        public FsmEnum positionType;

        [Tooltip(CommonTooltips.k_Position)]
        public FsmVector2 startPaintingPosition;

        [Tooltip("Paints using random tiles from the pattern")]
        public FsmBool randomizePattern;

        public class TileSelectionData
        {
            public int[] selectionData;
            public int rowLength;
            public int columnLength { get { return 1 + (selectionData.Length - 1) / rowLength; } }
            public uint[,] Get2DTileDataArray()
            {
                int w = rowLength;
                int h = columnLength;
                uint[,] output = new uint[w, h];
                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        output[x, h - y - 1] = (uint)selectionData[y * w + x];
                    }
                }
                return output;
            }
        }
        public TileSelectionData tileSelection;

        public override void Reset()
        {
            base.Reset();
            randomizePattern = false;
            tileSelection = new TileSelectionData() { selectionData = new int[] { -1 }, rowLength = 1 };
            FsmObject foundNotNullTileset = State != null ? State.Actions.TakeWhile(x => x != this).Where(x => x is PaintActionBase && (x as PaintActionBase).tileset.Value).Select(x => (x as PaintActionBase).tileset).LastOrDefault() : null;
            tileset = foundNotNullTileset != null ? new FsmObject(foundNotNullTileset) : null;
        }


        public override void OnEnter()
        {
            DoPaintAction();
            Finish();
        }

        protected virtual void DoPaintAction()
        {

        }
    }
}
#endif