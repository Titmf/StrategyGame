using ECS.Services.SceneManagement;

using SceneManagement;

using UnityEngine;
using UnityEngine.UIElements;

namespace MainMenu
{
    public class MainMenuEvents : MonoBehaviour
    {
        private UIDocument _uiDocument;
        private Button _newGameButton;
        
        [SerializeField] private SceneLoader _sceneLoader;

        private void Awake()
        {
            _uiDocument = GetComponent<UIDocument>();
            
            _newGameButton = _uiDocument.rootVisualElement.Q<VisualElement>("NewGame") as Button;
            _newGameButton.RegisterCallback<ClickEvent>(OnStartGameClicked);
        }
        private void OnDestroy()
        {
            _newGameButton.UnregisterCallback<ClickEvent>(OnStartGameClicked);
        }
        private void OnStartGameClicked(ClickEvent evt)
        {
            Debug.Log("Start game");
            LoadScene();
        }
        async void LoadScene()
        {
            await _sceneLoader.LoadSceneGroup(1);
        }
    }
}