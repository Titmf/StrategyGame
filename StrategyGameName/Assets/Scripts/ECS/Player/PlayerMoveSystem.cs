using ECS.Player.Components;

using Leopotam.EcsLite;

using UnityEngine;

namespace ECS.Player
{
    public sealed class PlayerMoveSystem : IEcsRunSystem
    {
        private float stepDistance = 1f; // Расстояние на каждый шаг

        public void Run(EcsSystems ecsSystems)
        {
            var filter = ecsSystems.GetWorld().Filter<PlayerComponent>().Inc<PlayerInputComponent>().End();
            var playerPool = ecsSystems.GetWorld().GetPool<PlayerComponent>();
            var playerInputPool = ecsSystems.GetWorld().GetPool<PlayerInputComponent>();

            foreach (var entity in filter)
            {
                ref var playerComponent = ref playerPool.Get(entity);
                ref var playerInputComponent = ref playerInputPool.Get(entity);

                // Переменная для хранения направления движения
                Vector3 moveDirection = playerInputComponent.MoveInput;

                // Применяем шаг к вектору движения
                moveDirection *= stepDistance;

                // Преобразуем направление движения относительно локальной системы координат игрока
                moveDirection = playerComponent.PlayerTransform.TransformDirection(moveDirection);

                // Применяем силу движения к Rigidbody игрока
                playerComponent.PlayerRb.AddForce(moveDirection, ForceMode.Impulse);

                // Выполняем поворот игрока в направлении движения
                RotatePlayerTowardsMoveDirection(ref playerComponent, moveDirection);
            }
        }

        private void RotatePlayerTowardsMoveDirection(ref PlayerComponent playerComponent, Vector3 moveDirection)
        {
            // Если вектор движения ненулевой, производим поворот игрока
            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                playerComponent.PlayerTransform.rotation = Quaternion.Lerp(playerComponent.PlayerTransform.rotation, targetRotation, Time.deltaTime * playerComponent.PlayerRotationSpeed);
            }
        }
    }
}