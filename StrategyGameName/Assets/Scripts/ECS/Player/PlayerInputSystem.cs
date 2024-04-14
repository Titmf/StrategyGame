using ECS.Data;
using ECS.Player.Components;

using Leopotam.EcsLite;

using UnityEngine;

namespace ECS.Player
{
    public sealed class PlayerInputSystem : IEcsRunSystem
    {
        public float stepTime = 0.5f; // Время для одного шага
        private float stepTimer = 0f;
        public void Run(EcsSystems ecsSystems)
        {
            var filter = ecsSystems.GetWorld().Filter<PlayerInputComponent>().End();
            var playerInputPool = ecsSystems.GetWorld().GetPool<PlayerInputComponent>();
            var gameSceneData = ecsSystems.GetShared<GameSceneData>();

            foreach (var entity in filter)
            {
                ref var playerInputComponent = ref playerInputPool.Get(entity);

                HandleMovementInput(ref playerInputComponent);

                if (Input.GetKeyDown(KeyCode.R))
                    ReloadScene(gameSceneData);

                if (!Input.anyKey)
                    playerInputComponent.MoveInput = Vector3.zero;
            }
        }
        private void HandleMovementInput(ref PlayerInputComponent playerInputComponent)
        {
            // Увеличение таймера шага
            stepTimer += Time.deltaTime;

            // Проверка клавиш для движения и увеличение вектора движения при их удержании
            if (Input.GetKey(KeyCode.W))
                HandleMoveInput(ref playerInputComponent, Vector3.forward);
            if (Input.GetKey(KeyCode.A))
                HandleMoveInput(ref playerInputComponent, Vector3.left);
            if (Input.GetKey(KeyCode.S))
                HandleMoveInput(ref playerInputComponent, Vector3.back);
            if (Input.GetKey(KeyCode.D))
                HandleMoveInput(ref playerInputComponent, Vector3.right);

            // Если прошло достаточно времени для шага, сбрасываем таймер
            if (stepTimer >= stepTime)
                stepTimer = 0f;
        }

        private void HandleMoveInput(ref PlayerInputComponent playerInputComponent, Vector3 direction)
        {
            // Если таймер шага превысил заданное время, увеличиваем вектор движения
            if (stepTimer >= stepTime)
            {
                playerInputComponent.MoveInput += direction;
                stepTimer = 0f; // Сбрасываем таймер для следующего шага
            }
        }
        private async void ReloadScene(GameSceneData gameSceneData)
        {
            await gameSceneData.GamePrefabsSo.SceneServiceLoader.LoadSceneGroup(1);
        }
    }
}