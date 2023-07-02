using UnityEngine;
using UnityEngine.EventSystems;

namespace com.MazeGame.View.Field
{
  [RequireComponent(typeof(RectTransform))]
  public class ChildTransformScaleFitter: UIBehaviour
  {
    public enum ScaleMode
    {
      ChildFitInParent,
      ChildEnvelopeParent
    }

    [SerializeField]
    private ScaleMode _scaleMode = ScaleMode.ChildFitInParent;

    private RectTransform _childTransform = null;
    private RectTransform _transform = null;
    private Vector2 _size = Vector2.zero;

    public RectTransform RectTransform
    {
      get
      {
        if (_transform == null)
        {
          _transform = GetComponent<RectTransform>();
          _size = _transform.rect.size;
        }
        return _transform;
      }
    }

    public RectTransform ChildTransform
    {
      get
      {
        if (_childTransform == null)
        {
          if (transform.childCount > 0) {
            _childTransform = transform.GetChild(0).GetComponent<RectTransform>();
          }
        }
        return _childTransform;
      }
    }

    protected override void OnRectTransformDimensionsChange()
    {
      UpdateChildScale(false);
    }

    public void UpdateChildScale()
    {
      UpdateChildScale(true);
    }

    public void UpdateChildScale(bool forced)
    {
      if (ChildTransform == null)
      {
        return;
      }

      Vector2 childSize = ChildTransform.rect.size;
      if (childSize.x == 0 || childSize.y == 0)
      {
        return;
      }

      Vector2 size = RectTransform.rect.size;
      if (size.Equals(_size) && !forced)
      {
        return;
      }
      _size = size;

      ChildTransform.localScale = Vector3.one * GetScale(_scaleMode, childSize, size);
    }

    private float GetScale(ScaleMode scaleMode, Vector2 childSize, Vector2 parentSize)
    {
      float scaleX = parentSize.x / childSize.x;
      float scaleY = parentSize.y / childSize.y;

      if (scaleMode == ScaleMode.ChildFitInParent) {
        return Mathf.Min(scaleX, scaleY);
      }

      return Mathf.Max(scaleX, scaleY);
    }
  }
}