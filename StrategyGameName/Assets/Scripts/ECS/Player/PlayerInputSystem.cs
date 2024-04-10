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

                if (Input.GetKey(KeyCode.W)) playerInputComponent.MoveInput = Vector3.forward;
                if (Input.GetKey(KeyCode.A)) playerInputComponent.MoveInput = Vector3.left;
                if (Input.GetKey(KeyCode.S)) playerInputComponent.MoveInput = Vector3.back;
                if (Input.GetKey(KeyCode.D)) playerInputComponent.MoveInput = Vector3.right;

                if (Input.GetKeyDown(KeyCode.R)) ReloadScene(gameSceneData);
                
                if (!Input.anyKey) playerInputComponent.MoveInput = Vector3.zero;
            }
        }
        private async void ReloadScene(GameSceneData gameSceneData)
        {
            await gameSceneData.GamePrefabsSo.SceneServiceLoader.LoadSceneGroup(1);
        }
    }
}