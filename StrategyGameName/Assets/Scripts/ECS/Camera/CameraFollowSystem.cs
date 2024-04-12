using ECS.Components;
using ECS.Data;
using ECS.Player;
using ECS.Player.Components;

using Leopotam.EcsLite;

using UnityEngine;

namespace ECS.Systems
{
    public class CameraFollowSystem : IEcsInitSystem, IEcsRunSystem
    {
        private int _cameraEntity;

        public void Init(EcsSystems ecsSystems)
        {
            var gameData = ecsSystems.GetShared<GameSceneData>();

            var cameraEntity = ecsSystems.GetWorld().NewEntity();

            var cameraPool = ecsSystems.GetWorld().GetPool<CameraComponent>();
            cameraPool.Add(cameraEntity);
            ref var cameraComponent = ref cameraPool.Get(cameraEntity);

            cameraComponent.CameraTransform = Camera.main.transform;
            cameraComponent.CameraSmoothness = Constants.Numbers.CameraFollowSmoothness;
            cameraComponent.CurVelocity = Vector3.zero;
            cameraComponent.Offset = Constants.Vectors.CameraFollowOffset;

            _cameraEntity = cameraEntity;
        }

        public void Run(EcsSystems ecsSystems)
        {
            var filter = ecsSystems.GetWorld().Filter<PlayerComponent>().End();
            var playerPool = ecsSystems.GetWorld().GetPool<PlayerComponent>();
            var cameraPool = ecsSystems.GetWorld().GetPool<CameraComponent>();

            ref var cameraComponent = ref cameraPool.Get(_cameraEntity);

            foreach(var entity in filter)
            {
                ref var playerComponent = ref playerPool.Get(entity);

                Vector3 currentPosition = cameraComponent.CameraTransform.position;
                Vector3 targetPoint = playerComponent.PlayerTransform.position + cameraComponent.Offset;

                cameraComponent.CameraTransform.position = Vector3.Lerp(currentPosition, targetPoint, cameraComponent.CameraSmoothness);
            }    
        }
    }
}