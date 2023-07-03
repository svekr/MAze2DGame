using UnityEngine;

namespace com.MazeGame.View.Field.Player
{
  public class PlayerView : MonoBehaviour
  {
    [SerializeField]
    private RectTransform _transform;
    [SerializeField]
    private Animator _animator;

    private Vector2Int _position;

    public void SetPosition(Vector2Int position)
    {
      _position = position;
      float x = _transform.rect.size.x * (position.x + 1);
      float y = _transform.rect.size.y * (position.y + 1);
      _transform.localPosition = new Vector2(x, y);
    }

    public void UpdatePosition(Vector2Int position)
    {
      SetPosition(position);
    }
  }
}