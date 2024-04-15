using ECS.Data;
using ECS.Map;
using ECS.Player;
using ECS.ScriptableObjects;

using Leopotam.EcsLite;

using UnityEngine;

namespace ECS.Start {
    sealed class EcsStartup : MonoBehaviour {
        
        private EcsWorld _ecsWorld;
        private EcsSystems _ecsInitSystem;
        private EcsSystems _updateSystem;
        private EcsSystems _fixedUpdateSystem;

        [SerializeField] private GamePrefabsSo _gamePrefabsSo;
        [SerializeField] private GameSceneConfigurationSo _gameSceneConfigurationSo;
        [SerializeField] private Transform _playerSpawnPoint;

        private void Start ()
        {
            _ecsWorld = new EcsWorld();
            var gameSceneData = new GameSceneData();
            
            gameSceneData.GamePrefabsSo = _gamePrefabsSo;
            gameSceneData.GameSceneConfigurationSo = _gameSceneConfigurationSo;
            gameSceneData.PlayerSpawnPoint = _playerSpawnPoint;

            _ecsInitSystem = new EcsSystems (_ecsWorld, gameSceneData)
                .Add( new MapInitSystem())
                .Add (new PlayerInitSystem())
                
                // register additional worlds here, for example:
                // .AddWorld (new EcsWorld (), "events")
                
#if UNITY_EDITOR
                // add debug systems for custom worlds here, for example:
                // .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ("events"))
                .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ())
#endif
                ;
            
            _ecsInitSystem.Init();

            _updateSystem = new EcsSystems(_ecsWorld, gameSceneData)
                .Add(new PlayerInputSystem());
            
            _updateSystem.Init();
            
            _fixedUpdateSystem = new EcsSystems(_ecsWorld, gameSceneData)
                .Add(new PlayerMoveSystem())
                .Add(new PlayerRotationSystem())
                ;
            
            _fixedUpdateSystem.Init();
        }

        private void Update () {
            _updateSystem?.Run();
        }
        
        private void FixedUpdate () {
            _fixedUpdateSystem?.Run ();
        }

        private void OnDestroy ()
        {
            if (_ecsInitSystem == null) return;

            _ecsInitSystem.Destroy ();

            _updateSystem.Destroy ();
            
            _fixedUpdateSystem.Destroy ();
            
            // add here cleanup for custom worlds, for example:
            // _systems.GetWorld ("events").Destroy ();
            
            _ecsInitSystem.GetWorld ().Destroy ();
            
            _fixedUpdateSystem = null;
            _updateSystem = null;
            _ecsInitSystem = null;
        }
    }
}