using UnityEngine;
using com.MazeGame.Model;
using com.MazeGame.View.Field.Player;

namespace com.MazeGame.View.Field
{
  public class FieldView : MonoBehaviour
  {
    [SerializeField]
    private RectTransform _layersContainer;
    [SerializeField]
    private CellsView[] _layers;
    [SerializeField]
    private RectTransform _playerContainer;
    [SerializeField]
    private PlayerView _playerPrefab;

    private PlayerView _player;

    public void Initialize(LevelModel levelModel)
    {
      InitializeLayers(levelModel.FieldModel);
      UpdateFieldSize(_layers[0].Size);
      InitializePlayer(levelModel.PlayerPosition);
    }

    public void UpdatePlayerPosition(LevelModel levelModel)
    {
      _player.UpdatePosition(levelModel.PlayerPosition);
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

    private void InitializePlayer(Vector2Int position)
    {
      _player = GameObject.Instantiate<PlayerView>(_playerPrefab, _playerContainer);
      _player.name = _playerPrefab.name;
      _player.SetPosition(position);
    }
  }
}