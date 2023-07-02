using UnityEngine;

namespace com.MazeGame.View.Field
{
  public class CellView: DestroyableBehaviour
  {
    public Vector2 Size => GetComponent<RectTransform>().rect.size;
  }
}