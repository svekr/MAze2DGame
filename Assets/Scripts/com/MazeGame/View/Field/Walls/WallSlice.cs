using UnityEngine;
using com.MazeGame.Model;

namespace com.MazeGame.View.Field.Walls
{
   public class WallSlice : DestroyableBehaviour
   {
      [SerializeField]
      protected Direction _side;
      [SerializeField]
      protected bool _inverse = false;

      public void Set(Direction directions)
      {
        if (_inverse)
        {
          gameObject.SetActive((directions & _side) == Direction.None);
        } else {
          gameObject.SetActive((directions & _side) == _side);
        }
      }
   }
}