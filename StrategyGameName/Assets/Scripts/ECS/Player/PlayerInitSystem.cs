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

            var playerGo = Object.Instantiate(gameSceneData.GamePrefabsSo.PlayerPrefab,
                HexCoordinates.HexToCartesian(Constants.PlayerDefaultConfiguration.PlayerStartPositionByHexCoordinates) + Constants.PlayerDefaultConfiguration.PlayerStartPositionOffset, 
                 Constants.PlayerDefaultConfiguration.PlayerRotationOffsetAtInit);
            
            var playerCamera = Object.Instantiate(gameSceneData.GamePrefabsSo.PlayerCameraPrefab,
                playerGo.transform);
            
            playerCamera.GetComponentInChildren<CinemachineVirtualCamera>().Follow = playerGo.transform;
            playerCamera.GetComponentInChildren<CinemachineVirtualCamera>().LookAt = playerGo.transform;

            playerGo.GetComponentInChildren<GroundCheckerView>().GroundedPool = ecsSystems.GetWorld().GetPool<GroundedComponent>();
            playerGo.GetComponentInChildren<GroundCheckerView>().PlayerEntity = playerEntity;
            playerGo.GetComponentInChildren<CollisionCheckerView>().EcsWorld = ecsWorld;
            
            playerComponent.PlayerTransform = playerGo.transform;
            playerComponent.PlayerRb = playerGo.GetComponent<Rigidbody>();
            playerComponent.PlayerPositionByHexCoordinates = Constants.PlayerDefaultConfiguration.PlayerStartPositionByHexCoordinates;
        }
    }
}