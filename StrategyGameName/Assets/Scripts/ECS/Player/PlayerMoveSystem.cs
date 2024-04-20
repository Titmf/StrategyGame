using System;

using DG.Tweening;

using ECS.Data;
using ECS.Player.Components;

using Leopotam.EcsLite;

using UnityEngine;

namespace ECS.Player
{
    public class PlayerMoveSystem : IEcsRunSystem
    {
        public void Run(EcsSystems ecsSystems)
        {
            var filter = ecsSystems.GetWorld().Filter<PlayerComponent>().Inc<PlayerInputComponent>().End();
            var playerPool = ecsSystems.GetWorld().GetPool<PlayerComponent>();
            var playerInputPool = ecsSystems.GetWorld().GetPool<PlayerInputComponent>();

            foreach (var entity in filter)
            {
                ref var playerComponent = ref playerPool.Get(entity);
                ref var playerInputComponent = ref playerInputPool.Get(entity);

                var moveInput = playerInputComponent.MoveInput;
                if (moveInput == 0) continue;

                var currPlayerPosByHex = playerComponent.PlayerPositionByHexCoordinates;
                var currHexCoordinateByDirIndex = HexCoordinatesByDirectionIndex(playerComponent.PlayerDirectionIndex);
                var targetHexPos = new HexCoordinates(currPlayerPosByHex.X + currHexCoordinateByDirIndex.X * moveInput, currPlayerPosByHex.Z + currHexCoordinateByDirIndex.Z * moveInput);

                playerComponent.PlayerPositionByHexCoordinates = targetHexPos;
                var targetPosition = HexCoordinates.ToPosition(targetHexPos) + Constants.PlayerDefaultConfiguration.PlayerPositionOffset;
                
                playerComponent.PlayerTransform.DOMove(targetPosition, Constants.PlayerDefaultCharacteristics.PlayerDefaultStepDuration).SetEase(Ease.InOutSine);

                playerInputComponent.MoveInput = 0;
            }
        }
        private HexCoordinates HexCoordinatesByDirectionIndex(int currentDirectionIndex)
        {
            return currentDirectionIndex switch
            {
                5 => new HexCoordinates(0, 1),
                0 => new HexCoordinates(1, 0),
                1 => new HexCoordinates(1, -1),
                2 => new HexCoordinates(0, -1),
                3 => new HexCoordinates(-1, 0),
                4 => new HexCoordinates(-1, 1),
                _ => throw new ArgumentOutOfRangeException(nameof(currentDirectionIndex), currentDirectionIndex, "Direction index out of range wtf")
            };
        }
    }
}