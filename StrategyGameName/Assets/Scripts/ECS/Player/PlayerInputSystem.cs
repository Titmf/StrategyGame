using ECS.Data;
using ECS.Player.Components;

using Leopotam.EcsLite;

using UnityEngine;

namespace ECS.Player
{
    public class PlayerInputSystem : IEcsRunSystem
    {
        public void Run(EcsSystems ecsSystems)
        {
            var filter = ecsSystems.GetWorld().Filter<PlayerInputComponent>().End();
            var playerInputPool = ecsSystems.GetWorld().GetPool<PlayerInputComponent>();
            var gameSceneData = ecsSystems.GetShared<GameSceneData>();

            foreach (var entity in filter)
            {
                ref var playerInputComponent = ref playerInputPool.Get(entity);

                if (Input.GetKeyDown(KeyCode.W)) playerInputComponent.MoveInput = new Vector3( 10, 0, 0);
                if (Input.GetKeyUp(KeyCode.A)) playerInputComponent.MoveInput = new Vector3( 0, 0, 10);
                if (Input.GetKeyUp(KeyCode.S)) playerInputComponent.MoveInput = new Vector3(-10, 0, 0);
                if (Input.GetKeyDown(KeyCode.D)) playerInputComponent.MoveInput = new Vector3( 0, 0,  -10);

                if (Input.GetKeyDown(KeyCode.R))
                {
                    ReloadScene(gameSceneData);
                }
            }
        }
        private async void ReloadScene(GameSceneData gameSceneData)
        {
            await gameSceneData.GamePrefabs.SceneServiceLoader.LoadSceneGroup(1);
        }
    }
}