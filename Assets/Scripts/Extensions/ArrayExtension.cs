using System;

namespace Extensions
{
  static public class ArrayExtension
  {
    static private readonly Random _random = new Random();

    static public void Shuffle(this Array array)
    {
      int indexMax = array.Length;
      while (indexMax > 1)
      {
        indexMax--;
        int index = _random.Next(indexMax + 1);
        object value = array.GetValue(index);
        array.SetValue(array.GetValue(indexMax), index);
        array.SetValue(value, indexMax);
      }
    }
  }
}