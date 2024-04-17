using ECS.Data;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECS.Map {
    public class MapInitSystem : IEcsInitSystem {
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
                    
                    var cellEntity = ecsWorld.NewEntity();
                    var cellPool = ecsWorld.GetPool<HexCellComponent>();
                    cellPool.Add(cellEntity);
                    ref var cellComponent = ref cellPool.Get(cellEntity);

                    cellComponent.Position = position;
                    cellComponent.Color = Constants.HexDefaultConfiguration.DefaultColor;
                    cellComponent.Coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
                }
            }
        }
    }
}