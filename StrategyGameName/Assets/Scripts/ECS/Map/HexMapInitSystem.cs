using ECS.Data;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECS.Map {
    public class HexMapInitSystem : IEcsInitSystem {
        public void Init(EcsSystems ecsSystems) {
            var ecsWorld = ecsSystems.GetWorld();
            var gameSceneData = ecsSystems.GetShared<GameSceneData>();

            var chunkCountX = gameSceneData.MapConfigurationSo.ChunkCountX;
            var chunkCountZ = gameSceneData.MapConfigurationSo.ChunkCountZ;
            var cellCountX = chunkCountX * HexMetrics.chunkSizeX;
            var cellCountZ = chunkCountZ * HexMetrics.chunkSizeZ;

            for (int z = 0, i = 0; z < cellCountZ; z++) {
                for (var x = 0; x < cellCountX; x++, i++)
                {
                    Vector3 position;
                    position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
                    position.y = 0f;
                    position.z = z * (HexMetrics.outerRadius * 1.5f);

                    #region CellInit
                    var cellEntity = ecsWorld.NewEntity();
                    
                    var cellCoordinatePool = ecsWorld.GetPool<HexCellPositionComponent>();
                    cellCoordinatePool.Add(cellEntity);
                    ref var hexCellPositionComponent = ref cellCoordinatePool.Get(cellEntity);

                    var cellColorPool = ecsWorld.GetPool<HexCellInputColorComponent>();
                    cellColorPool.Add(cellEntity);
                    ref var hexCellColorComponent = ref cellColorPool.Get(cellEntity);

                    hexCellPositionComponent.Position = position;
                    hexCellPositionComponent.Coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
                    
                    hexCellColorComponent.Color = Constants.HexDefaultConfiguration.DefaultColor;
                    #endregion
                }
            }
        }
    }
}