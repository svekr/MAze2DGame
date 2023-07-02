using UnityEngine;

namespace com.Settings
{
  public class LevelSettingsBase : ScriptableObject
  {
    [SerializeField]
    private int _id = -1;
    [SerializeField]
    private int _durationSeconds = 0;

    public int Id
    {
      get => _id;
      set => _id = value;
    }

    public int Duration => _durationSeconds;
  }
}