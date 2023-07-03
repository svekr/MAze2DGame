using System;

namespace com.Managers.UserData.Controller
{
  public interface IDataProvider
  {
    void LoadData<T>(Action<T> onLoadComplete, string path);

    void SaveData<T>(T data, string path);
  }
}