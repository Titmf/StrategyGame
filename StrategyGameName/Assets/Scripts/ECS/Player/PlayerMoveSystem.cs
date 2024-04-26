using System.Linq;

using DG.Tweening;

using ECS.Data;
using ECS.Map;
using ECS.Player.Components;

using Hex;

using Leopotam.EcsLite;

namespace ECS.Player
{
    public class PlayerMoveSystem : IEcsRunSystem
    {
        private readonly HexCoordinates[] _stepHexesToEffect = {
            new(1, 0), new(1, -1), new(0, -1), new(-1, 0),
            new(-1, 1), new(0, 1)
        };
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
                var currHexCoordinateByDirIndex = new HexCoordinates(0, 0).DirectionToHex(playerComponent.PlayerDirectionIndex);
                var targetHexPos = currPlayerPosByHex + currHexCoordinateByDirIndex * moveInput;

                if (StepByHexValidator(ecsSystems, targetHexPos))
                {
                    playerComponent.PlayerPositionByHexCoordinates = targetHexPos;
                    var targetPosition = HexCoordinates.ToPosition(targetHexPos) + Constants.PlayerDefaultConfiguration.PlayerPositionOffset;

                    var mySequence = DOTween.Sequence();

                    mySequence.Append(playerComponent.PlayerTransform.
                        DOMove(targetPosition, Constants.PlayerDefaultCharacteristics.PlayerDefaultStepDuration).
                        SetEase(Ease.InOutSine));
                    mySequence.AppendCallback(() => StepEffect(targetHexPos, ecsSystems));
                    mySequence.AppendInterval(0.3f);
                    mySequence.AppendCallback(() => StepCascadeEffect(targetHexPos, ecsSystems));
                }
                playerInputComponent.MoveInput = 0;
            }
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
                if (cellComponent.Coordinates == targetHexPos)
                {
                    isValid = true;
                    break;
                }
            }
            return isValid;
        }
        // TODO: To Hex Own Sys?
        private void StepEffect(HexCoordinates completeStepHexCoordinates, EcsSystems ecsSystems)
        {
            var world = ecsSystems.GetWorld();
            var ecsFilter = world.Filter<HexCellPositionComponent>().Inc<HexCellInputColorComponent>().End();
            
            var poolHexCellPosition = world.GetPool<HexCellPositionComponent>();
            var poolHexCellInputColor = world.GetPool<HexCellInputColorComponent>();

            foreach (var entity in ecsFilter)
            {
                ref var hexCellPos = ref poolHexCellPosition.Get(entity);
                ref var hexCellInputColorComponent = ref poolHexCellInputColor.Get(entity);
                
                if (hexCellPos.Coordinates == completeStepHexCoordinates)
                {
                    hexCellInputColorComponent.IsChanged = true;
                    hexCellInputColorComponent.Color = Constants.EffectColors.RedStep;
                }
            }
        }
        private void StepCascadeEffect(HexCoordinates completeStepHexCoordinates, EcsSystems ecsSystems)
        {
            var world = ecsSystems.GetWorld();
            var ecsFilter = world.Filter<HexCellPositionComponent>().Inc<HexCellInputColorComponent>().End();
            
            var poolHexCellPosition = world.GetPool<HexCellPositionComponent>();
            var poolHexCellInputColor = world.GetPool<HexCellInputColorComponent>();    

            foreach (var entity in ecsFilter)
            {
                ref var hexCellPos = ref poolHexCellPosition.Get(entity);
                ref var hexCellInputColorComponent = ref poolHexCellInputColor.Get(entity);

                var pos = hexCellPos;
                
                foreach (var t in _stepHexesToEffect.Where(t => pos.Coordinates == t + completeStepHexCoordinates))
                {
                    hexCellInputColorComponent.IsChanged = true;
                    hexCellInputColorComponent.Color = Constants.EffectColors.BlueStep;
                }
            }
        }
    }
}