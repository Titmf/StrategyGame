using ECS.Components;
using ECS.Data;
using ECS.Start;

using Leopotam.EcsLite;

using UnityEngine;

namespace ECS.Systems.Player
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        public void Init(EcsSystems ecsSystems)
        {
            var ecsWorld = ecsSystems.GetWorld();
            var gameData = ecsSystems.GetShared<GameSceneData>();

            var playerEntity = ecsWorld.NewEntity();

            var playerPool = ecsWorld.GetPool<PlayerComponent>();
            playerPool.Add(playerEntity);
            ref var playerComponent = ref playerPool.Get(playerEntity);
            var playerInputPool = ecsWorld.GetPool<PlayerInputComponent>();
            playerInputPool.Add(playerEntity);
            ref var playerInputComponent = ref playerInputPool.Get(playerEntity);

            var playerGO = GameObject.FindGameObjectWithTag("Player"); //TO DO remove this
            
            playerGO.GetComponentInChildren<GroundCheckerView>().GroundedPool = ecsSystems.GetWorld().GetPool<GroundedComponent>();
            playerGO.GetComponentInChildren<GroundCheckerView>().PlayerEntity = playerEntity;
            playerGO.GetComponentInChildren<CollisionCheckerView>().EcsWorld = ecsWorld;
            playerComponent.PlayerSpeed = gameData.configuration.playerSpeed;
            playerComponent.PlayerTransform = playerGO.transform;
            playerComponent.PlayerCollider = playerGO.GetComponent<CapsuleCollider>();
            playerComponent.PlayerRb = playerGO.GetComponent<Rigidbody>();
        }
    }
}