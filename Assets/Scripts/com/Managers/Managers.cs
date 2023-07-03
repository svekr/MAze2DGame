using System;
using com.Managers.Input;
using com.Managers.Levels.Controller;
using com.Managers.UserData.Controller;

namespace com.Managers
{
  public class Managers
  {
    public UserDataManager UserDataManager { get; private set; }

    public MazeLevelsManager MazeLevelsManager { get; private set; }

    public InputManager InputManager { get; private set; }

    public void SetManager<T>(T manager)
    {
      Type thisType = this.GetType();
      Type managerType = typeof(T);
      System.Reflection.PropertyInfo[] properties = thisType.GetProperties();
      foreach (System.Reflection.PropertyInfo property in properties)
      {
        if (property.PropertyType == managerType)
        {
          property.SetValue(this, manager);
          return;
        }
      }
    }
  }
}