using System;

using DG.Tweening;

using ECS.Data;
using ECS.Map;
using ECS.Player.Components;

using Leopotam.EcsLite;

namespace ECS.Player
{
    public class PlayerMoveSystem : IEcsRunSystem
    {
        private int[] X = new int[] { 1, 1, 0, -1, -1, 0 };
        private int[] Z = new int[] { 0, -1, -1, 0, 1, 1 };
        
        private const int Upper = 6;
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

                if (StepByHexValidator(ecsSystems, targetHexPos))
                {
                    playerComponent.PlayerPositionByHexCoordinates = targetHexPos;
                    var targetPosition = HexCoordinates.ToPosition(targetHexPos) + Constants.PlayerDefaultConfiguration.PlayerPositionOffset;

                    playerComponent.PlayerTransform.
                        DOMove(targetPosition, Constants.PlayerDefaultCharacteristics.PlayerDefaultStepDuration).
                        SetEase(Ease.InOutSine);
                    
                    StepEffect(targetHexPos, ecsSystems);
                }
                
                playerInputComponent.MoveInput = 0;
            }
        }
        private HexCoordinates HexCoordinatesByDirectionIndex(int currentDirectionIndex)
        {
            return currentDirectionIndex switch
            {
                0 => new HexCoordinates(1, 0),
                1 => new HexCoordinates(1, -1),
                2 => new HexCoordinates(0, -1),
                3 => new HexCoordinates(-1, 0),
                4 => new HexCoordinates(-1, 1),
                5 => new HexCoordinates(0, 1),
                _ => throw new ArgumentOutOfRangeException(nameof(currentDirectionIndex), currentDirectionIndex, "Direction index out of range wtf")
            };
        }

        private bool StepByHexValidator(EcsSystems ecsSystems, HexCoordinates targetHexPos)
        {
            var isValid = false;

            var world = ecsSystems.GetWorld();
            var cellPool = world.GetPool<HexCellPositionComponent>();
            var cellFilter = world
                .Filter<HexCellPositionComponent>()
                .Inc<HexCellGameObjectComponent>()
                .End();
            foreach (var entity in cellFilter)
            {
                ref var cellComponent = ref cellPool.Get(entity);
                if (cellComponent.Coordinates.X == targetHexPos.X &&
                    cellComponent.Coordinates.Z == targetHexPos.Z)
                {
                    isValid = true;
                    break;
                }
            }
            return isValid;
        }

        private void StepEffect(HexCoordinates CompleteStepHexCoordinates, EcsSystems ecsSystems)
        {
            var world = ecsSystems.GetWorld();
            var ecsFilter = world.Filter<HexCellPositionComponent>().Inc<HexCellInputColorComponent>().End();
            
            var poolHexCellPosition = world.GetPool<HexCellPositionComponent>();
            var poolHexCellInputColor = world.GetPool<HexCellInputColorComponent>();

            foreach (var entity in ecsFilter)
            {
                ref var hexCellPos = ref poolHexCellPosition.Get(entity);
                ref var hexCellInputColorComponent = ref poolHexCellInputColor.Get(entity);
                
                for (var i = 0; i < Upper; i++)
                    if (hexCellPos.Coordinates.X == (X[i] + CompleteStepHexCoordinates.X) && hexCellPos.Coordinates.Z == (Z[i] + CompleteStepHexCoordinates.Z)) hexCellInputColorComponent.IsChanged = true;
            }
        }
    }
}