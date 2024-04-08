using ECS.Components;
using ECS.Data;

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
            cameraComponent.CameraSmoothness = gameData.configuration.cameraFollowSmoothness;
            cameraComponent.CurVelocity = Vector3.zero;
            cameraComponent.Offset = new Vector3(0f, 1f, -9f);

            this._cameraEntity = cameraEntity;
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

                cameraComponent.CameraTransform.position = Vector3.SmoothDamp(currentPosition, targetPoint, ref cameraComponent.CurVelocity, cameraComponent.CameraSmoothness);
            }    
        }
    }
}