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

                var position = playerComponent.PlayerTransform.position;
                position = Vector3.Lerp(
                    position,
                    position + moveDirection * Constants.PlayerDefaultCharacteristics.PlayerDefaultStepDistance,
                    1);

                playerComponent.PlayerTransform.position = position;

                playerInputComponent.MoveInput = Vector3.zero;
            }
        }
    }
}