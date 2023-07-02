using System.Collections.Generic;
using UnityEngine;

namespace com.Settings {
  [CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/GameSettings", order = 1)]
  public class GameSettings : ScriptableObject
  {
    [SerializeField]
    private List<LevelSettingsBase> _levels;

    public List<LevelSettingsBase> Levels => _levels;
  }
}
