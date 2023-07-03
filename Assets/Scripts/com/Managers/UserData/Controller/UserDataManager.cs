using System;
using com.Managers.UserData.Model;

namespace com.Managers.UserData.Controller
{
  public class UserDataManager
  {
    private readonly string _path = "UserData";
    private readonly ILogger _logger;
    private readonly IDataProvider _dataProvider;

    private UserModel _userModel = null;
    private Action _completeHandler = null;

    public int Level => Math.Max(_userModel?.level ?? 1, 1);

    public bool IsLastLevelWin { get; set;} = false;

    public UserModel UserModel
    {
      get
      {
        ValidateUserModel();
        return _userModel;
      }
    }

    public UserDataManager(ILogger logger, IDataProvider dataProvider)
    {
      _logger = logger;
      _dataProvider = dataProvider;
      ValidateUserModel();
    }

    public void LoadData(Action onComplete)
    {
      _completeHandler = onComplete;
      _dataProvider.LoadData<UserModel>(LoadHandler, _path);
    }

    public void LevelComplete(long elapsedTime, int distancePassed)
    {
      ValidateUserModel();
      _userModel.level++;
      _userModel.stats.lastTime = elapsedTime;
      _userModel.stats.lastDistance = distancePassed;
      UpdateStats();
      _dataProvider.SaveData(_userModel, _path);
    }

    private void UpdateStats()
    {
      if (_userModel.stats.bestTime > 0)
      {
        _userModel.stats.bestTime = Math.Min(_userModel.stats.bestTime, _userModel.stats.lastTime);
      } else {
        _userModel.stats.bestTime = _userModel.stats.lastTime;
      }
      if (_userModel.stats.bestDistance > 0)
      {
        _userModel.stats.bestDistance = Math.Min(_userModel.stats.bestDistance, _userModel.stats.lastDistance);
      } else {
        _userModel.stats.bestDistance = _userModel.stats.lastDistance;
      }
    }

    private void LoadHandler(UserModel model)
    {
      if (model != null)
      {
        _userModel = model;
        ValidateUserModel();
      }
      _completeHandler?.Invoke();
      _completeHandler = null;
    }

    private void ValidateUserModel()
    {
      if (_userModel == null)
      {
        _userModel = new UserModel();
      }
      if (_userModel.stats == null)
      {
        _userModel.stats = new UserStats();
      }
      if (_userModel.level < 1)
      {
        _userModel.level = 1;
      }
    }
  }
}