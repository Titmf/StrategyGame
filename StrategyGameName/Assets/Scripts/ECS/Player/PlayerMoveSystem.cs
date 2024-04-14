using ECS.Player.Components;

using Leopotam.EcsLite;

using UnityEngine;

namespace ECS.Player
{
    public sealed class PlayerMoveSystem : IEcsRunSystem
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

                Vector3 moveDirection = playerInputComponent.MoveInput;
                
                moveDirection = playerComponent.PlayerTransform.TransformDirection(moveDirection);

                playerComponent.PlayerRb.AddForce(moveDirection * playerComponent.PlayerSpeed, ForceMode.Impulse);
                
                if (playerInputComponent.MoveInput.magnitude > 0.1f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                    playerComponent.PlayerTransform.rotation = Quaternion.Lerp(playerComponent.PlayerTransform.rotation, targetRotation, Time.deltaTime * playerComponent.PlayerRotationSpeed);
                }
            }
        }
    }
}