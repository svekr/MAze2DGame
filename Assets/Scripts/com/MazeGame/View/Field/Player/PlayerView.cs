using UnityEngine;
using com.MazeGame.Model;
using Utils;

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

    private string _trigger;
    public void UpdatePosition(Vector2Int position)
    {
      if (_animator != null)
      {
        Direction direction = FieldModelUtil.GetDirectionByPosition(_position, position);
        string clipName = $"PlayerMove{direction}Animation";
        _animator.Play(clipName, 0, 0);
      }
      SetPosition(position);
    }
  }
}