using SceneManagement;

using UnityEngine;
using UnityEngine.UIElements;

using Zenject;

namespace MainMenu
{
    public class MainMenuEvents : MonoBehaviour
    {
        private UIDocument _uiDocument;
        private Button _newGameButton;

        [Inject]
        private SceneLoader _sceneLoader;

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
        async void OnStartGameClicked(ClickEvent evt)
        {
            Debug.Log("Start game");
            await _sceneLoader.LoadSceneGroup(1);
        }
    }
}