using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ZhukovskyGamesPlugin;

public class LoadingManager : MonoBehaviour {
    private string _sceneName;

    [SerializeField]
    private float _delayBeforeSceneSwitch = 2.5f;

    [SerializeField]
    private Slider _progressSlider;

    public void StartLoading() {
        Application.targetFrameRate = -1;
        LoadManagers().Forget();
    }

    private async UniTask LoadManagers() {
        //TODO отрефакторить чтобы зависимости сами решались, написать норм DI, а лучше использовать готовый
        CustomMonoBehaviour[] preloadedManagers = FindObjectsByType<CustomMonoBehaviour>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)
            .OrderBy(m => m.InitPriority).ToArray();

        foreach (CustomMonoBehaviour manager in preloadedManagers) {
            if (manager is IPreloadable preloadable) {
                preloadable.Init();
                await UniTask.Yield();
            }
        }

        SaveLoadManager.LoadGame();
        SaveLoadManager.SaveGame();

        if (SceneManager.GetActiveScene().name == "LoadingScene") {
            LoadGameScene().Forget();
        }
    }

    private async UniTask LoadGameScene() {
        _sceneName = "GameScene";
        var op = SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Additive);
        op.allowSceneActivation = false;

        float curTime = 0;
        while (curTime <= _delayBeforeSceneSwitch) {
            curTime += Time.deltaTime;
            await UniTask.WaitForEndOfFrame();
            _progressSlider.value = (curTime / _delayBeforeSceneSwitch) * 0.9f;
        }

        // ждём загрузку до 90% (Unity не даёт больше, пока allowSceneActivation = false)
        await UniTask.WaitUntil(() => op.progress >= 0.9f);
        _progressSlider.value = 0.9f;

        // разрешаем активацию
        op.allowSceneActivation = true;

        await UniTask.WaitUntil(() => op.isDone);
        _progressSlider.value = 1f;
        await UniTask.WaitForEndOfFrame();

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(_sceneName));
        SceneManager.UnloadSceneAsync("LoadingScene");
    }
}