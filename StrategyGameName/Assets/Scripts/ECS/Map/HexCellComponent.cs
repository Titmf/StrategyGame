using UnityEngine;

namespace ECS.Map
{
    public struct HexCellComponent
    {
        public Vector3 Position;
        public Color Color;
        public HexCoordinates Coordinates;
    }
}