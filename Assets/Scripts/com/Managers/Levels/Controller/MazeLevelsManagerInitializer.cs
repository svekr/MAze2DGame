using System;
using System.Collections.Generic;
using com.Managers.TaskStarter;
using com.Settings;

namespace com.Managers.Levels.Controller
{
  public class MazeLevelsManagerInitializer: ITask
  {
    private List<LevelSettingsBase> _levels;

    public string Name => this.GetType().Name;

    public MazeLevelsManagerInitializer(List<LevelSettingsBase> levels)
    {
      _levels = levels;
    }

    public void Start(ILogger logger, Action onComplete)
    {
      if (Main.Managers.MazeLevelsManager == null)
      {
        Main.Managers.SetManager(new MazeLevelsManager(logger, new MazeLevelsLocalDataProvider(logger, _levels)));
      }
      onComplete?.Invoke();
    }
  }
}