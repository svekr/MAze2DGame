using com.MazeGame.Model;

namespace com.Managers.Levels.Controller
{
  public class MazeLevelsManager: LevelsManager<LevelModel>
  {
    public MazeLevelsManager(ILogger logger, ILevelsDataProvider dataProvider): base (logger, dataProvider)
    {
      _builders.Add(new MazeLevelAutoGeneratedBuilder());
      _builders.Add(new MazeLevelPreconfiguredBuilder());
    }
  }
}