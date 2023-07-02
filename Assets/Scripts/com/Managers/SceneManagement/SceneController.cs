using UnityEngine;

namespace com.Managers.SceneManagement
{
  abstract public class SceneController: MonoBehaviour
  {
    [SerializeField]
    private string _location;
    [SerializeField]
    protected NextSceneStarter _nextSceneStarter;

    public string Location => _location;

    public ILogger Logger { get; set; } = null;

    protected void Awake()
    {
      InitLogger();
      Logger?.Log($"Scene '{Location}' Awake");
      AwakeHandler();
    }

    protected void Start()
    {
      Logger?.Log($"Scene '{Location}' Start");
      StartHandler();
    }

    protected void OnDestroy()
    {
      Logger?.Log($"Scene '{Location}' Destroy");
      DestroyHandler();
    }

    abstract protected void InitLogger();

    virtual protected void AwakeHandler()
    {

    }

    virtual protected void StartHandler()
    {

    }

    virtual protected void DestroyHandler()
    {

    }

    virtual protected void NextSceneReadyHandler(SceneController nextSceneController)
    {

    }

    virtual protected void SceneReadyHandler()
    {

    }

    public void GoToNextScene(NextSceneStarter sceneStarter)
    {
      if (sceneStarter != null)
      {
        Logger?.Log($"Scene '{Location}' tries to start next scene");
        sceneStarter.StartNextScene(OnNextSceneReady);
      }
    }

    [ContextMenu("GoToNextScene")]
    public void GoToNextScene()
    {
      GoToNextScene(_nextSceneStarter);
    }

    [ContextMenu("GoToNextScene", true)]
    private bool IsApplicationRunning()
    {
      return Application.isPlaying;
    }

    private void OnNextSceneReady(UnityEngine.SceneManagement.Scene nextScene) {
      SceneController nextSceneController = FindSceneController(nextScene);
      if (nextSceneController != null)
      {
        Logger?.Log($"Scene '{nextScene.name}' is ready. New location is '{nextSceneController.Location}'");
        nextSceneController.SceneReadyHandler();
      } else {
        Logger?.LogWarning($"Scene '{nextScene.name}' is ready (SceneController not found)");
      }
      NextSceneReadyHandler(nextSceneController);
    }

    private SceneController FindSceneController(UnityEngine.SceneManagement.Scene scene)
    {
      GameObject[] rootObjects = scene.GetRootGameObjects();
      if (rootObjects == null)
      {
        return null;
      }
      foreach (GameObject rootObject in rootObjects)
      {
        SceneController sceneController = rootObject?.GetComponentInChildren<SceneController>();
        if (sceneController != null)
        {
          return sceneController;
        }
      }
      return null;
    }
  }
}