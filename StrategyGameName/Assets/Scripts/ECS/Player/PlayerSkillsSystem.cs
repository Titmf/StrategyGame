using DG.Tweening;

using ECS.Map;
using ECS.Player.Components;

using Leopotam.EcsLite;

using UnityEngine;

namespace ECS.Player
{
    public class PlayerSkillsSystem : IEcsRunSystem
    {
        public void Run(EcsSystems ecsSystems)
        {
            var ecsWorld = ecsSystems.GetWorld();
            var playerFilter = ecsWorld.Filter<PlayerComponent>().Inc<PlayerHandComponent>().End();
            var playerHandComponentPool = ecsWorld.GetPool<PlayerHandComponent>();
            var playerInputPool = ecsWorld.GetPool<PlayerInputComponent>();
            var cellsFilter = ecsWorld.Filter<HexCellPositionComponent>().End();
            var hexCellPositionPool = ecsWorld.GetPool<HexCellPositionComponent>();
            var hexCellGameObjectPool = ecsWorld.GetPool<HexCellGameObjectComponent>();

            foreach (var playerEntity in playerFilter)
            {
                ref var playerHandComponent = ref playerHandComponentPool.Get(playerEntity);
                ref var playerInputComponent = ref playerInputPool.Get(playerEntity);

                if (playerHandComponent.IsHandHoldingSomething)
                {
                    if (playerInputComponent.OperationNumber != 0)
                    {
#if UNITY_EDITOR
                Debug.Log("Start skill " + playerInputComponent.OperationNumber);     
#endif
                        foreach (var cellEntity in cellsFilter)
                        {
                            ref var hexCellPos = ref hexCellPositionPool.Get(cellEntity);

                            foreach (var hexInHand in playerHandComponent.HexCellsInHand)
                            {
                                if (hexCellPos.Coordinates != hexInHand) continue;

                                ref var hexCellGo = ref hexCellGameObjectPool.Get(cellEntity);
                                
                                switch (playerInputComponent.OperationNumber)
                                {
                                    case 1:
                                        Object.Destroy(hexCellGo.GameObject);
                                        ecsWorld.DelEntity(cellEntity);
                                        break;
                                    case 2:
                                        hexCellGo.GameObject.transform.DOMoveY( hexCellGo.GameObject.transform.position.y+65f, 1);
                                        break;
                                    default:
                                        throw new System.NotImplementedException("There is no such skill");
                                }
                            }
                        }

                        playerHandComponent.IsHandHoldingSomething = false;
                        playerHandComponent.HexCellsInHand = null;
                        playerInputComponent.OperationNumber = 0;
                    }
                }
            }
        }
    }
}