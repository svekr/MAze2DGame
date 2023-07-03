using System;

namespace com.Managers.UserData.Model
{
  [Serializable]
  public class UserStats
  {
    public long bestTime = 0;
    public long lastTime = 0;
    public int bestDistance = 0;
    public int lastDistance = 0;
  }
}