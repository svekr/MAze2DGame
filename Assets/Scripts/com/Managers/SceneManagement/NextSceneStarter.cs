using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace com.Managers.SceneManagement
{
  public class NextSceneStarter: MonoBehaviour
  {
    [SerializeField]
    private string _nextSceneAssetPath;
    [SerializeField]
    private LoadSceneMode _loadSceneMode = LoadSceneMode.Single;
    [SerializeField]
    private TransitionSceneObject _transitionPrefab;

    private Action<Scene> _completeHandler = null;

    public void StartNextScene(Action<Scene> onComplete = null) {
      StartNextScene(_nextSceneAssetPath, onComplete);
    }

    public void StartNextScene(string sceneAssetPath, Action<Scene> onComplete)
    {
      if (string.IsNullOrEmpty(sceneAssetPath))
      {
        return;
      }
      _completeHandler = onComplete;
      if (_transitionPrefab != null) {
        LoadNextWithTransitionObject(sceneAssetPath);
        return;
      }
      if (_loadSceneMode == LoadSceneMode.Single)
      {
        SceneManager.LoadScene(sceneAssetPath);
      } else {
        StartCoroutine(LoadNextWithTransitionObject(sceneAssetPath, null));
      }
    }

    private void LoadNextWithTransitionObject(string sceneName)
    {
      TransitionSceneObject transitionObject = GameObject.Instantiate(_transitionPrefab);
      DontDestroyOnLoad(transitionObject.gameObject);
      transitionObject.Show(
        () => StartCoroutine(LoadNextWithTransitionObject(sceneName, transitionObject)),
        OnTransitionObjectHideComplete
      );
    }

    private void OnTransitionObjectHideComplete()
    {
      _completeHandler?.Invoke(SceneManager.GetActiveScene());
    }

    private IEnumerator LoadNextWithTransitionObject(string sceneName, TransitionSceneObject transitionObject)
    {
      GameObject eventSystemObject = UnityEngine.EventSystems.EventSystem.current?.gameObject;
      Scene nextScene = SceneManager.GetSceneByPath(sceneName);
      if (!nextScene.isLoaded)
      {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, _loadSceneMode);
        if (transitionObject != null)
        {
          asyncLoad.completed += transitionObject.Hide;
        }
        while (!asyncLoad.isDone)
        {
          yield return null;
        }
      } else {
        SceneManager.SetActiveScene(nextScene);
        if (transitionObject != null)
        {
          transitionObject.Hide();
        } else {
          OnTransitionObjectHideComplete();
        }
      }
      eventSystemObject?.SetActive(false);
    }
  }
}