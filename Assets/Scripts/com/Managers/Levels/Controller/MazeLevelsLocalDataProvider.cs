using System.Collections.Generic;
using com.Settings;

namespace com.Managers.Levels.Controller
{
  public class MazeLevelsLocalDataProvider: ILevelsDataProvider
  {
    private ILogger _logger;
    private List<LevelSettingsBase> _levels;

    public MazeLevelsLocalDataProvider(ILogger logger, List<LevelSettingsBase> levels)
    {
      _logger = logger;
      _levels = PrepareLevels(levels);
      if (_levels == null || _levels.Count == 0)
      {
        _logger?.LogError("Invalid levels settings");
        return;
      }
    }

    public object GetLevelSettings(int id)
    {
      return _levels?.Find(level => level?.Id == id);
    }

    private List<LevelSettingsBase> PrepareLevels(List<LevelSettingsBase> levels)
    {
      if (levels == null || levels.Count == 0)
      {
        return null;
      }
      for (int i = levels.Count - 1; i >= 0; i--)
      {
        if (levels[i] == null)
        {
          levels.RemoveAt(i);
        } else if (levels[i].Id <= 0)
        {
          levels[i].Id = i + 1;
        }
      }
      return levels;
    }
  }
}