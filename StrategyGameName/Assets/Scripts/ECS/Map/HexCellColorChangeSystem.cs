using ECS.Data;

using Leopotam.EcsLite;

namespace ECS.Map
{
    public class HexCellColorChangeSystem : IEcsRunSystem
    {
        public void Run(EcsSystems ecsSystems)
        {
            var filter = ecsSystems.GetWorld().Filter<HexCellColorComponent>().End();
            var hexColorCompPool = ecsSystems.GetWorld().GetPool<HexCellColorComponent>();
            var hexCellMaterialPool = ecsSystems.GetWorld().GetPool<HexCellMaterialComponent>();

            foreach (var entity in filter)
            {
                ref var hexCellColorComponent = ref hexColorCompPool.Get(entity);
                ref var hexCellMaterialComponent = ref hexCellMaterialPool.Get(entity);

                if (hexCellColorComponent.IsChanged) hexCellMaterialComponent.Material.SetColor(Constants.Tags.EmissionColorTag, hexCellColorComponent.Color);
            }
        }
    }
}