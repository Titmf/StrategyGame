using ECS.Data;
using ECS.Player.Components;

using Leopotam.EcsLite;

using UnityEngine;

namespace ECS.Player
{
    public sealed class PlayerInputSystem : IEcsRunSystem
    {
        public void Run(EcsSystems ecsSystems)
        {
            var filter = ecsSystems.GetWorld().Filter<PlayerInputComponent>().End();
            var playerInputPool = ecsSystems.GetWorld().GetPool<PlayerInputComponent>();
            var gameSceneData = ecsSystems.GetShared<GameSceneData>();

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
                ref var playerInputComponent = ref playerInputPool.Get(entity);

                if (Input.GetKey(KeyCode.W))
                    playerInputComponent.MoveInput = Vector3.forward;

                if (Input.GetKey(KeyCode.S))
                    playerInputComponent.MoveInput = Vector3.back;
                
                if (Input.GetKey(KeyCode.A))
                    playerInputComponent.RotateInput = 1f;

                if (Input.GetKey(KeyCode.D))
                    playerInputComponent.RotateInput = -1f;

                if (Input.GetKeyDown(KeyCode.R))
                    ReloadScene(gameSceneData);

                if (Input.anyKey) continue;

                playerInputComponent.MoveInput = Vector3.zero;
                playerInputComponent.RotateInput = 0f;
            }
        }
        
        private async void ReloadScene(GameSceneData gameSceneData)
        {
            await gameSceneData.GamePrefabsSo.SceneServiceLoader.LoadSceneGroup(1);
        }
    }
}