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

                if (Input.GetKeyDown(KeyCode.W)) playerInputComponent.MoveInput = 1;
                if (Input.GetKeyDown(KeyCode.S)) playerInputComponent.MoveInput = -1;
                if (Input.GetKeyDown(KeyCode.A)) playerInputComponent.RotateInput = -1;
                if (Input.GetKeyDown(KeyCode.D)) playerInputComponent.RotateInput = 1;
                
                if (Input.GetKeyDown(KeyCode.Alpha1)) playerInputComponent.FormationNumber = 1;
                if (Input.GetKeyDown(KeyCode.Alpha2)) playerInputComponent.FormationNumber = 2;
                if (Input.GetKeyDown(KeyCode.Alpha3)) playerInputComponent.FormationNumber = 3;
                if (Input.GetKeyDown(KeyCode.Alpha4)) playerInputComponent.FormationNumber = 4;
                if (Input.GetKeyDown(KeyCode.Q)) playerInputComponent.FormationNumber = 0;
                
                if (Input.GetKeyDown(KeyCode.F)) playerInputComponent.OperationNumber = 1;
                if (Input.GetKeyDown(KeyCode.Space)) playerInputComponent.OperationNumber = 2;

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