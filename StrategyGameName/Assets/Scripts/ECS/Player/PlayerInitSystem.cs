using Cinemachine;

using ECS.Data;
using ECS.Player.Components;
using ECS.Start;
using ECS.Systems;

using Leopotam.EcsLite;

using UnityEngine;

namespace ECS.Player
{
    public sealed class PlayerInitSystem : IEcsInitSystem
    {
        public void Init(EcsSystems ecsSystems)
        {
            var ecsWorld = ecsSystems.GetWorld();
            var gameSceneData = ecsSystems.GetShared<GameSceneData>();

            var playerEntity = ecsWorld.NewEntity();
            
            var playerPool = ecsWorld.GetPool<PlayerComponent>();
            playerPool.Add(playerEntity);
            ref var playerComponent = ref playerPool.Get(playerEntity);
            
            var playerInputPool = ecsWorld.GetPool<PlayerInputComponent>();
            playerInputPool.Add(playerEntity);
            ref var playerInputComponent = ref playerInputPool.Get(playerEntity);

            var playerGO = Object.Instantiate(gameSceneData.GamePrefabsSo.PlayerPrefab,
                gameSceneData.PlayerSpawnPoint.position,
                gameSceneData.PlayerSpawnPoint.rotation * Constants.PlayerDefaultConfiguration.PlayerRotationOffsetAtInit);
            
            var playerCamera = Object.Instantiate(gameSceneData.GamePrefabsSo.PlayerCameraPrefab,
                playerGO.transform);
            
            playerCamera.GetComponentInChildren<CinemachineVirtualCamera>().Follow = playerGO.transform;
            playerCamera.GetComponentInChildren<CinemachineVirtualCamera>().LookAt = playerGO.transform;

            playerGO.GetComponentInChildren<GroundCheckerView>().GroundedPool = ecsSystems.GetWorld().GetPool<GroundedComponent>();
            playerGO.GetComponentInChildren<GroundCheckerView>().PlayerEntity = playerEntity;
            playerGO.GetComponentInChildren<CollisionCheckerView>().EcsWorld = ecsWorld;
            
            playerComponent.PlayerTransform = playerGO.transform;
            playerComponent.PlayerRb = playerGO.GetComponent<Rigidbody>();
        }
    }
}