using ECS.Services.SceneManagement;
using ECS.Start;

using Platformer;

using SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace ECS.Data
{
    public class GameSceneData
    {
        public ConfigPlayerSo configuration;
        public Text coinCounter;
        public GameObject playerWonPanel;
        public GameObject gameOverPanel;
        public SceneLoader SceneServiceLoader;
    }
}