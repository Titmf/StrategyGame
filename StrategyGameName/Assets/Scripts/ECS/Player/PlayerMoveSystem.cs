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
                
                if (playerInputComponent.MoveInput == Vector3.zero) continue;
                
                var moveDirection = playerComponent.PlayerTransform.TransformDirection(playerInputComponent.MoveInput);

                var position = playerComponent.PlayerTransform.position;
                
                var targetPosition = position + moveDirection * Constants.PlayerDefaultCharacteristics.PlayerDefaultStepDistance;
                
                playerComponent.PlayerTransform.DOMove(targetPosition, Constants.PlayerDefaultCharacteristics.PlayerDefaultStepDuration).SetEase(Ease.InOutSine);
                
                /*playerComponent.PlayerTransform.position = position;*/

                playerInputComponent.MoveInput = Vector3.zero;
            }
        }
    }
}