using ECS.Data;

using Leopotam.EcsLite;

using Unity.Mathematics;

using UnityEngine;

namespace ECS.Map {
    public class HexCellViewInitSystem : IEcsInitSystem {
        public void Init(EcsSystems ecsSystems) {
            var world = ecsSystems.GetWorld();
            var cellPool = world.GetPool<HexCellComponent>();
            var viewPool = world.GetPool<HexCellViewComponent>();
            var gameSceneData = ecsSystems.GetShared<GameSceneData>();
            
            var cellContainer = new GameObject("CellContainer");
            
            foreach (var entity in world.Filter<HexCellComponent>().End()) {
                if (!viewPool.Has(entity)) {
                    ref var cellComponent = ref cellPool.Get(entity);

                    var cellGo = GameObject.Instantiate(gameSceneData.GamePrefabsSo.HexModelPrefab, 
                        cellComponent.Position, Constants.HexDefaultConfiguration.RotationOffsetAtInit, cellContainer.transform);

                    viewPool.Add(entity);
                    ref var viewComponent = ref viewPool.Get(entity);
                    viewComponent.GameObject = cellGo;
                }
            }
        }
    }
}