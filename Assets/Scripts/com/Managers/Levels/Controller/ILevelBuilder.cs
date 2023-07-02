using com.Managers.Levels.Model;

namespace com.Managers.Levels.Controller
{

  public interface ILevelBuilder<T> where T: ILevelModel
  {
    bool TryBuildLevel(object levelSettings, out T level);
  }
}