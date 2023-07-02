using System;
using UnityEngine;
using com.MazeGame.Model;

namespace com.MazeGame.View.Field.Cells
{
  public class Cell : CellView
  {
    [SerializeField]
    private GameObject _marker;

    public void Set(Direction directions)
    {
      _marker.SetActive(GetDirectionsCount(directions) > 2);
    }

    private UInt32 GetDirectionsCount(Direction directions)
    {
      UInt32 value = (UInt32) directions;
      value = value - ((value >> 1) & 0x55555555);
      value = (value & 0x33333333) + ((value >> 2) & 0x33333333);
      UInt32 count = ((value + (value >> 4) & 0xF0F0F0F) * 0x1010101) >> 24;
      return count;
    }
  }
}