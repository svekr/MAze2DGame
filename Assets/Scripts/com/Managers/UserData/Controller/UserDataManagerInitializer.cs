using System;
using com.Managers.TaskStarter;

namespace com.Managers.UserData.Controller
{
  public class UserDataManagerInitializer: ITask
  {
    public string Name => this.GetType().Name;

    public UserDataManagerInitializer(ILogger logger)
    {

    }

    public void Start(ILogger logger, Action onComplete)
    {
      if (Main.Managers.UserDataManager == null)
      {
        Main.Managers.SetManager(new UserDataManager(logger, new LocalDataProvider(logger)));
        Main.Managers.UserDataManager.LoadData(onComplete);
      } else {
        onComplete?.Invoke();
      }
    }
  }
}