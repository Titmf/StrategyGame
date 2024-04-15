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

#if UNITY_EDITOR
            bool filterIsEmpty = true;

            foreach (var entity in filter)
            {
                filterIsEmpty = false;
                break;
            }

            if (filterIsEmpty) Debug.Log("Фильтр пустой");
#endif
            
            foreach (var entity in filter)
            {
                ref var playerComponent = ref playerPool.Get(entity);
                ref var playerInputComponent = ref playerInputPool.Get(entity);
                
                if (playerInputComponent.RotateInput == 0f) continue;
                
                playerInputComponent.RotationStepAccumulator += 
                    Mathf.Abs(playerInputComponent.RotateInput) *
                    Constants.InputConfigs.RotationStepAccelerationRate * Time.deltaTime;

                if (playerInputComponent.RotationStepAccumulator >= Constants.InputConfigs.RotationStepThreshold)
                {
                    float angle = playerInputComponent.RotateInput * Constants.PlayerDefaultCharacteristics.RotationStepAngle;
                    var rotation = playerComponent.PlayerTransform.rotation;
                    Quaternion targetRotation = Quaternion.Euler(0f, angle, 0f) * rotation;
                    rotation = Quaternion.Lerp(rotation, targetRotation, Time.deltaTime * Constants.InputConfigs.PlayerDefaultRotationSpeed);
#if UNITY_EDITOR
                    Debug.Log(targetRotation);         
#endif                    
                    playerComponent.PlayerTransform.rotation = rotation;
                    playerInputComponent.RotationStepAccumulator -= Constants.InputConfigs.RotationStepThreshold;
                }
            }
        }
    }
}