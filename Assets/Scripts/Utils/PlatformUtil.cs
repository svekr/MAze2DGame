using UnityEngine;

namespace Utils
{
  static public class PlatformUtil
  {
    static public bool IsMobilePlatform()
    {
      bool result = false;
      if (Application.platform == RuntimePlatform.Android)
      {
        result = true;
      } else if (Application.platform == RuntimePlatform.IPhonePlayer)
      {
        result = true;
      }
#if UNITY_EDITOR
      result = false;
#endif
      return result;
    }
  }
}