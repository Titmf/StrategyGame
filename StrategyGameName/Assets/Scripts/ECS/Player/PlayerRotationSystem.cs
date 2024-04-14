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

                if (playerInputComponent.RotateInput == 0) continue;

                float angle = playerInputComponent.RotateInput * 60f;
                var rotation = playerComponent.PlayerTransform.rotation;
                Quaternion targetRotation = Quaternion.Euler(0f, angle, 0f) * rotation;
                rotation = Quaternion.Lerp(rotation, targetRotation, Time.deltaTime * playerComponent.PlayerRotationSpeed);
                playerComponent.PlayerTransform.rotation = rotation;
            }
        }
    }
}