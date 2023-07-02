using System;
using UnityEngine;
using com.MazeGame.Model;

namespace com.Settings
{
  [CreateAssetMenu(fileName = "LevelPreconfigured", menuName = "Settings/Levels/Preconfigured", order = 3)]
  public class LevelPreconfiguredSettings : LevelSettingsBase
  {
    [Header("List of field cells")]
    [Space]
    [SerializeField]
    public CellSettings[] _cells;

    public CellSettings[] Cells => _cells;
  }

  [Serializable]
  public class CellSettings
  {
    public Vector2Int index;
    public Direction directions = Direction.None;
    public Direction gates = Direction.None;
  }
}