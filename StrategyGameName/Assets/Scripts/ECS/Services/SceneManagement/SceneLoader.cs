using System;
using System.Threading.Tasks;

using SceneManagement;

using UnityEngine;
using UnityEngine.UI;

namespace ECS.Services.SceneManagement {
    public class SceneLoader : MonoBehaviour { 
        [SerializeField] Image loadingBar;
        [SerializeField] float fillSpeed = 0.5f;
        [SerializeField] Canvas loadingCanvas;
        [SerializeField] UnityEngine.Camera loadingCamera;
        [SerializeField] SceneGroup[] sceneGroups;

        float targetProgress;
        bool isLoading;

        public readonly SceneGroupManager manager = new SceneGroupManager();

        private void Awake() {
            // TODO can remove
            manager.OnSceneLoaded += sceneName => Debug.Log("Loaded: " + sceneName);
            manager.OnSceneUnloaded += sceneName => Debug.Log("Unloaded: " + sceneName);
            manager.OnSceneGroupLoaded += () => Debug.Log("Scene group loaded");
        }

        private async void Start() {
            await LoadSceneGroup(0);
        }

        private void Update() {
            if (!isLoading) return;
            
            float currentFillAmount = loadingBar.fillAmount;
            float progressDifference = Mathf.Abs(currentFillAmount - targetProgress);

            float dynamicFillSpeed = progressDifference * fillSpeed;
    
            loadingBar.fillAmount = Mathf.Lerp(currentFillAmount, targetProgress, Time.deltaTime * dynamicFillSpeed);
        }

        public async Task LoadSceneGroup(int index) {
            loadingBar.fillAmount = 0f;
            targetProgress = 1f;

            if (index < 0 || index >= sceneGroups.Length) {
                Debug.LogError("Invalid scene group index: " + index);
                return;
            }

            LoadingProgress progress = new LoadingProgress();
            progress.Progressed += target => targetProgress = Mathf.Max(target, targetProgress);
            
            EnableLoadingCanvas();
            await manager.LoadScenes(sceneGroups[index], progress);
            EnableLoadingCanvas(false);
        }

        private void EnableLoadingCanvas(bool enable = true) {
            isLoading = enable;
            loadingCanvas.gameObject.SetActive(enable);
            loadingCamera.gameObject.SetActive(enable);
        }
        
    }

    public class LoadingProgress : IProgress<float> {
        public event Action<float> Progressed;

        const float ratio = 1f;

        public void Report(float value) {
            Progressed?.Invoke(value / ratio);
        }
    }
}