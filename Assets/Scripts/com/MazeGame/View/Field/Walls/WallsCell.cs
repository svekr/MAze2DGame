using UnityEngine;
using com.MazeGame.Model;

namespace com.MazeGame.View.Field.Walls
{
  public class WallsCell : DestroyableBehaviour
  {
    [SerializeField]
    private WallDecoration[] _decorationsInner;
    [SerializeField]
    private WallDecoration[] _decorationsOuter;

    public void Set(Direction directionsInner, Direction directionsOuter)
    {
      foreach (WallDecoration decoration in _decorationsInner) {
        decoration.Set(directionsInner);
      }
      foreach (WallDecoration decoration in _decorationsOuter) {
        decoration.Set(directionsOuter);
      }
    }

    override public void Destroy() {
      foreach (WallDecoration decoration in _decorationsInner) {
        decoration.Destroy();
      }
      foreach (WallDecoration decoration in _decorationsOuter) {
        decoration.Destroy();
      }
      base.Destroy();
    }
  }
}