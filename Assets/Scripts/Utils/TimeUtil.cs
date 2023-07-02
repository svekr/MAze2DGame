using System;

namespace Utils
{
  static public class TimeUtil
  {
    static public string FormatAsMMSS(long value)
    {
      long seconds = value % 60;
      long minutes = (value / 60);
      return $"{minutes:D2}:{seconds:D2}";
    }

    static public string FormatAsHHMMSS(long value)
    {
      long seconds = value % 60;
      long minutes = ((value / 60)) % 60;
      long hours = value / 3600;
      return $"{hours:D2}:{minutes:D2}:{seconds:D2}";
    }
  }
}