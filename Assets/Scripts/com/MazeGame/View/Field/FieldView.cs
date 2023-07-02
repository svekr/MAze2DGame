using UnityEngine;
using com.MazeGame.Model;

namespace com.MazeGame.View.Field
{
  public class FieldView : MonoBehaviour
  {
    [SerializeField]
    private RectTransform _layersContainer;
    [SerializeField]
    private CellsView[] _layers;

    public void Initialize(CellModel[,] fieldModel)
    {
      InitializeLayers(fieldModel);
      UpdateFieldSize(_layers[0].Size);
    }

    private void InitializeLayers(CellModel[,] fieldModel)
    {
      foreach (CellsView layer in _layers)
      {
        layer.SetData(fieldModel);
      }
    }

    private void UpdateFieldSize(Vector2 size) {
      _layersContainer.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
      _layersContainer.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);
      _layersContainer.ForceUpdateRectTransforms();

      Vector2 parentSize = _layersContainer.parent.GetComponent<RectTransform>().rect.size;
      float scale = Mathf.Min(parentSize.x / size.x, parentSize.y / size.y);
      _layersContainer.localScale = Vector3.one * scale;
    }
  }
}