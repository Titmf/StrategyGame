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
                
                if (playerInputComponent.MoveInput == Vector3.zero) continue;
                
                Vector3 moveDirection = playerInputComponent.MoveInput;
                moveDirection = playerComponent.PlayerTransform.TransformDirection(moveDirection);

                playerInputComponent.StepAccumulator += playerInputComponent.MoveInput.magnitude * Constants.InputConfigs.StepAccelerationRate * Time.deltaTime;

                if (playerInputComponent.StepAccumulator >= Constants.InputConfigs.StepThreshold)
                {
                    playerComponent.PlayerTransform.position = Vector3.Lerp(
                        playerComponent.PlayerTransform.position,
                        playerComponent.PlayerTransform.position + moveDirection * Constants.PlayerDefaultCharacteristics.PlayerDefaultStepDistance,
                        Constants.PlayerDefaultCharacteristics.PlayerDefaultStepLerpSpeed);

                    playerInputComponent.StepAccumulator -= Constants.InputConfigs.StepThreshold;
                }
            }
        }
    }
}