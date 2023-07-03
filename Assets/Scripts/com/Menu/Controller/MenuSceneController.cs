using UnityEngine;
using UnityEngine.UI;
using com.Managers.SceneManagement;
using com.Managers.TaskStarter;
using com.Managers.UserData.Controller;
using com.Menu.View;
using TMPro;

namespace com.Menu.Controller
{
  public class MenuSceneController: SceneController
  {
    [SerializeField]
    private TMP_Text _progressTF;
    [SerializeField]
    private Button _playButton;
    [SerializeField]
    private Button _statsButton;
    [SerializeField]
    private StatsView _statsView;

    override protected void InitLogger()
    {
      if (Main.Logger == null)
      {
        Main.Logger = new UnityLogger();
      }
      Logger = Main.Logger;
    }

    override protected void AwakeHandler()
    {
      _playButton.enabled = false;
      InitializeManagers();
    }

    private void InitializeManagers()
    {
      TaskStarter managersStarter = new TaskStarter(Logger);
      managersStarter.AppendTask(new UserDataManagerInitializer(Logger));
      managersStarter.Start(OnReady);
    }

    private void OnReady()
    {
      _progressTF.text = Main.Managers.UserDataManager.Level.ToString();
      _playButton.enabled = true;
      _statsButton.gameObject.SetActive(Main.Managers.UserDataManager.Level > 1);
      if (Main.Managers.UserDataManager.IsLastLevelWin && _statsView != null)
      {
        _statsView.Show(Main.Managers.UserDataManager.UserModel.stats);
      }
    }

    public void ShowStats()
    {
      _statsView.Show(Main.Managers.UserDataManager.UserModel.stats);
    }
  }
}