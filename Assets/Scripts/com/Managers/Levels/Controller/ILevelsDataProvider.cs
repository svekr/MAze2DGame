using System;

namespace com.Managers.Levels.Controller
{
  public interface ILevelsDataProvider
  {
    object GetLevelSettings(int id);
  }
}