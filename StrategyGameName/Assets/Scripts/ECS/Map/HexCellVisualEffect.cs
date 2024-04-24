using ECS.Data;

using Leopotam.EcsLite;

namespace ECS.Map
{
    public class HexCellVisualEffect : IEcsRunSystem
    {
        public void Run(EcsSystems ecsSystems)
        {
            var filter = ecsSystems.GetWorld().Filter<HexCellInputColorComponent>().End();
            var hexColorCompPool = ecsSystems.GetWorld().GetPool<HexCellInputColorComponent>();
            var hexCellMaterialPool = ecsSystems.GetWorld().GetPool<HexCellRendererComponent>();

            foreach (var entity in filter)
            {
                ref var hexCellColorComponent = ref hexColorCompPool.Get(entity);
                ref var hexCellMaterialComponent = ref hexCellMaterialPool.Get(entity);

                if (hexCellColorComponent.IsChanged) hexCellMaterialComponent.MeshRenderer.material.SetColor(Constants.Tags.EmissionColorTag, hexCellColorComponent.Color);
            }
        }
    }
}