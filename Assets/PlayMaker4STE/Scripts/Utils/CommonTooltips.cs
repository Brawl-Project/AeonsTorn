using UnityEngine;
using System.Collections;

namespace CreativeSpore.SuperTilemapEditor.PlayMakerActions
{
    public static class CommonTooltips
    {
        public const string k_PositionType = "LocalPosition will set the tile contained at local position relative to the tilemap. Grid position will set the tile at tilemap grid position.";
        public const string k_Position = "If the position type is Grid, it will be an a positions in horizontal and vertical cells. If the position type is LocalPosition, it will get the cell at this local position.";
    }
}