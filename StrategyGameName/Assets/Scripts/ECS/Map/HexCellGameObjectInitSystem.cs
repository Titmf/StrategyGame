using ECS.Data;

using Leopotam.EcsLite;

using UnityEngine;

namespace ECS.Map {
    public class HexCellGameObjectInitSystem : IEcsInitSystem {
        public void Init(EcsSystems ecsSystems) {
            var world = ecsSystems.GetWorld();
            var cellPool = world.GetPool<HexCellPositionComponent>();
            var gameObjectCellPool = world.GetPool<HexCellGameObjectComponent>();
            var gameSceneData = ecsSystems.GetShared<GameSceneData>();
            var cellMaterialPool = world.GetPool<HexCellRendererComponent>();
            
            var cellContainer = new GameObject("CellContainer");
            
            foreach (var entity in world.Filter<HexCellPositionComponent>().End()) {
                if (!gameObjectCellPool.Has(entity)) {
                    ref var cellComponent = ref cellPool.Get(entity);

                    var cellGo = GameObject.Instantiate(gameSceneData.GamePrefabsSo.HexModelPrefab, 
                        cellComponent.Position, Constants.HexDefaultConfiguration.RotationOffsetAtInit, cellContainer.transform);
                    
                    cellMaterialPool.Add(entity);
                    ref var hexCellMaterialComponent = ref cellMaterialPool.Get(entity);
                    hexCellMaterialComponent.MeshRenderer = cellGo.GetComponent<MeshRenderer>();

                    gameObjectCellPool.Add(entity);
                    ref var viewComponent = ref gameObjectCellPool.Get(entity);
                    viewComponent.GameObject = cellGo;
                }
            }
        }
    }
}