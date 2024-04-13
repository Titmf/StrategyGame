using Leopotam.EcsLite;

namespace ECS.Start
{
    class PlayerCameraInitSystem : IEcsInitSystem
    {
        public void Init(EcsSystems ecsSystems)
        {
            var world = ecsSystems.GetWorld();
            var pool = world.GetPool<PlayerCameraComponent>();
            var newEntity = world.NewEntity();
            pool.Add(newEntity);
        }
    }
}