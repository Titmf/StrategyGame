using Hex;

namespace ECS.Player.Components
{
    public struct PlayerHandComponent
    {
        public HexCoordinates[] HexCellsInHand;
        public bool IsHandHoldingSomething;
    }
}