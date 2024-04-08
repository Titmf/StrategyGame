using ECS.Data;

using Leopotam.EcsLite;

using UnityEngine;

namespace ECS.Systems.Player
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

                playerInputComponent.MoveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);

                if (Input.GetKeyDown(KeyCode.R))
                {
                    ReloadScene(gameSceneData);
                }
            }
        }
        private async void ReloadScene(GameSceneData gameSceneData)
        {
            await gameSceneData.SceneServiceLoader.LoadSceneGroup(1);
        }
    }
}