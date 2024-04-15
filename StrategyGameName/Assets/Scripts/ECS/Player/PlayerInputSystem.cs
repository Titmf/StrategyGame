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

            foreach (var entity in filter)
            {
                ref var playerInputComponent = ref playerInputPool.Get(entity);

                if (Input.GetKey(KeyCode.W))
                    playerInputComponent.MoveInput = Vector3.forward;

                if (Input.GetKeyDown(KeyCode.W))
                {
                    playerInputComponent.MoveInput = Vector3.forward;
                    playerInputComponent.StepAccumulator = Constants.InputConfigs.StepThreshold;
                }

                if (Input.GetKey(KeyCode.S))
                    playerInputComponent.MoveInput = Vector3.back;

                if (Input.GetKeyDown(KeyCode.S))
                {
                    playerInputComponent.MoveInput = Vector3.back;
                    playerInputComponent.StepAccumulator = Constants.InputConfigs.StepThreshold;
                }
                
                if (Input.GetKey(KeyCode.A))
                    playerInputComponent.RotateInput = -1f;
                
                if (Input.GetKeyDown(KeyCode.A))
                {
                    playerInputComponent.RotateInput = -1f;
                    playerInputComponent.RotationStepAccumulator = Constants.InputConfigs.RotationStepThreshold;
                }

                if (Input.GetKey(KeyCode.D))
                    playerInputComponent.RotateInput = 1f;
                
                if (Input.GetKeyDown(KeyCode.D))
                {
                    playerInputComponent.RotateInput = 1f;
                    playerInputComponent.RotationStepAccumulator = Constants.InputConfigs.RotationStepThreshold;
                }

                if (Input.GetKeyDown(KeyCode.R))
                    ReloadScene(gameSceneData);

                /*if (!Input.GetKeyDown(KeyCode.W) || !Input.GetKeyDown(KeyCode.S))
                    playerInputComponent.MoveInput = Vector3.zero;

                if (!Input.GetKey(KeyCode.A) || !Input.GetKey(KeyCode.D))
                    playerInputComponent.RotateInput = 0f;*/
            }
        }
        
        private async void ReloadScene(GameSceneData gameSceneData)
        {
            await gameSceneData.GamePrefabsSo.SceneServiceLoader.LoadSceneGroup(1);
        }
    }
}