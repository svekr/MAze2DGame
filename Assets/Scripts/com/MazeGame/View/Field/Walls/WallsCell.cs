using UnityEngine;
using com.MazeGame.Model;

namespace com.MazeGame.View.Field.Walls
{
  public class WallsCell : CellView
  {
    [SerializeField]
    private WallDecoration[] _decorationsInner;
    [SerializeField]
    private WallDecoration[] _decorationsOuter;
    [SerializeField]
    private WallDecoration[] _decorationsGates;

    public void Set(Direction directionsInner, Direction directionsOuter, Direction gates)
    {
      foreach (WallDecoration decoration in _decorationsInner) {
        decoration.Set(directionsInner);
      }
      foreach (WallDecoration decoration in _decorationsOuter) {
        decoration.Set(directionsOuter);
      }
      foreach (WallDecoration decoration in _decorationsGates) {
        decoration.Set(gates);
      }
    }

    override public void Destroy() {
      foreach (WallDecoration decoration in _decorationsInner) {
        decoration.Destroy();
      }
      foreach (WallDecoration decoration in _decorationsOuter) {
        decoration.Destroy();
      }
      foreach (WallDecoration decoration in _decorationsGates) {
        decoration.Destroy();
      }
      base.Destroy();
    }
  }
}