using System;
using com.Managers.Levels.Controller;

namespace com.Managers
{
  public class Managers
  {
    public MazeLevelsManager MazeLevelsManager { get; private set; }

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