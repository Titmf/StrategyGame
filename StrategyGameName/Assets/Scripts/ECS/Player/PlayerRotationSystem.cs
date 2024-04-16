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
                
                var angle = playerInputComponent.RotateInput * Constants.PlayerDefaultCharacteristics.RotationStepAngle;

                playerComponent.PlayerTransform.DORotate(new Vector3(0f, angle, 0f), Constants.PlayerDefaultCharacteristics.PlayerDefaultRotationDuration, RotateMode.WorldAxisAdd).SetEase(Ease.InOutSine);
                
                playerInputComponent.RotateInput = 0f;
            }
        }
    }
}