using ECS.Data;

using Leopotam.EcsLite;

using UnityEngine;

namespace ECS.Map
{
    public class MapInitSystem : IEcsInitSystem
    {
        public void Init(EcsSystems ecsSystems)
        {
            /*var ecsWorld = ecsSystems.GetWorld();*/
            var gameSceneData = ecsSystems.GetShared<GameSceneData>();
            
            /*
            var envEntity = ecsWorld.NewEntity();
            
            var envPool = ecsWorld.GetPool<MapComponent>();
            envPool.Add(envEntity);
            ref var envComponent = ref envPool.Get(envEntity);*/
            
            HexGrid mapGO = Object.Instantiate(gameSceneData.GamePrefabsSo.HexGridPrefab);
            
            mapGO.chunkCountX = gameSceneData.GameSceneConfigurationSo.ChunkCountX;
            mapGO.chunkCountZ = gameSceneData.GameSceneConfigurationSo.ChunkCountZ;
        }
    }
}