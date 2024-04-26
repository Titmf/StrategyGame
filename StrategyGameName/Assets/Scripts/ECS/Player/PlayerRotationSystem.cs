using System;

using DG.Tweening;

using ECS.Data;
using ECS.Player.Components;

using Leopotam.EcsLite;

using UnityEngine;

namespace ECS.Player
{
    public sealed class PlayerRotationSystem : IEcsRunSystem
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
                
                if (playerInputComponent.RotateInput == 0f) continue;

                playerComponent.PlayerDirectionIndex = CyclicIndexLimitation(playerComponent.PlayerDirectionIndex + playerInputComponent.RotateInput);

                var angle = playerComponent.PlayerDirectionIndex * Constants.PlayerDefaultCharacteristics.RotationStepAngle;

                playerComponent.PlayerTransform.DORotate(new Vector3(0f, 90f + angle, 0f), Constants.PlayerDefaultCharacteristics.PlayerDefaultRotationDuration).SetEase(Ease.InOutSine);
                
                playerInputComponent.RotateInput = 0;
            }
        }

        private Func<int, int> CyclicIndexLimitation = (currentIndex) =>
        {
            return currentIndex switch
            {
                < Constants.InputRotation.minValue => Constants.InputRotation.maxValue,
                > Constants.InputRotation.maxValue => Constants.InputRotation.minValue,
                _ => currentIndex
            };
        };
    }
}