using ECS.Player.Components;

using Hex;

using Leopotam.EcsLite;

using UnityEngine;

namespace ECS.Player
{
    public class PlayerGrabHandSystem : IEcsRunSystem
    {
        public void Run(EcsSystems ecsSystems)
        {
            var filter = ecsSystems.GetWorld().Filter<PlayerComponent>().Inc<PlayerInputComponent>().End();
            var playerHandComponentPool = ecsSystems.GetWorld().GetPool<PlayerHandComponent>();
            var playerPool = ecsSystems.GetWorld().GetPool<PlayerComponent>();
            var playerInputPool = ecsSystems.GetWorld().GetPool<PlayerInputComponent>();

            foreach (var entity in filter)
            {
                ref var playerHandComponent = ref playerHandComponentPool.Get(entity);
                ref var playerInputComponent = ref playerInputPool.Get(entity);
                ref var playerComponent = ref playerPool.Get(entity);

                if (playerInputComponent.FormationNumber != 0)
                {
                    var emptyHex = new HexCoordinates(0, 0);

                    playerHandComponent.HexCellsInHand = playerInputComponent.FormationNumber switch
                    {
                        1 => new[] { playerComponent.PlayerPositionByHexCoordinates }, //self
                        2 => new[]
                        {
                            playerComponent.PlayerPositionByHexCoordinates +
                            emptyHex.DirectionToHex(playerComponent.PlayerDirectionIndex),
                            playerComponent.PlayerPositionByHexCoordinates +
                            emptyHex.DirectionToHex(playerComponent.PlayerDirectionIndex + 1),
                            playerComponent.PlayerPositionByHexCoordinates +
                            emptyHex.DirectionToHex(playerComponent.PlayerDirectionIndex - 1)
                        }, //shield
                        3 => new[]
                        {
                            playerComponent.PlayerPositionByHexCoordinates +
                            playerComponent.PlayerPositionByHexCoordinates.DirectionToHex(playerComponent.
                                PlayerDirectionIndex)
                        }, //face block
                        4 => new[]
                        {
                            playerComponent.PlayerPositionByHexCoordinates +
                            emptyHex.DirectionToHex(playerComponent.PlayerDirectionIndex),
                            playerComponent.PlayerPositionByHexCoordinates +
                            emptyHex.DirectionToHex(playerComponent.PlayerDirectionIndex) * 2,
                            playerComponent.PlayerPositionByHexCoordinates +
                            emptyHex.DirectionToHex(playerComponent.PlayerDirectionIndex) * 3,
                            playerComponent.PlayerPositionByHexCoordinates +
                            emptyHex.DirectionToHex(playerComponent.PlayerDirectionIndex) * 4
                        }, //line of 4
                        _ => throw new System.NotImplementedException("There is no such formation")
                    };
#if UNITY_EDITOR
                    Debug.Log(playerHandComponent.HexCellsInHand.Length + " cells in hand");
#endif
                    
                    playerHandComponent.IsHandHoldingSomething = true;
                    playerInputComponent.FormationNumber = 0;
                }
            }
        }
    }
}