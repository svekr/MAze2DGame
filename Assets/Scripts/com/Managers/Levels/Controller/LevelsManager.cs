using System.Collections.Generic;
using com.Managers.Levels.Model;

namespace com.Managers.Levels.Controller
{
  abstract public class LevelsManager<TLevel> where TLevel: ILevelModel
  {
    protected readonly List<ILevelBuilder<TLevel>> _builders = new List<ILevelBuilder<TLevel>>();
    protected readonly ILogger _logger;
    protected readonly ILevelsDataProvider _dataProvider;

    public LevelsManager(ILogger logger, ILevelsDataProvider dataProvider)
    {
      _logger = logger;
      _dataProvider = dataProvider;
    }

    public TLevel GetLevel(int id)
    {
      object settings = _dataProvider.GetLevelSettings(id);
      return BuildLevel(settings);
    }

    private TLevel BuildLevel(object settings)
    {
      TLevel level;
      foreach (ILevelBuilder<TLevel> builder in _builders)
      {
        if (builder.TryBuildLevel(settings, out level))
        {
          return level;
        }
      }
      return default(TLevel);
    }
  }
}