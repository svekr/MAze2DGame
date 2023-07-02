using UnityEngine;

namespace com.Settings
{
  public class LevelSettingsBase : ScriptableObject
  {
    [SerializeField]
    private int id = -1;
    [SerializeField]
    private int _durationSeconds = 0;

    public int Id
    {
      get => id;
      set => id = value;
    }

    public int Duration => _durationSeconds;
  }
}