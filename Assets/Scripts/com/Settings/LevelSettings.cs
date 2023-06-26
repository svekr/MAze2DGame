using UnityEngine;

namespace com.Settings
{
  [CreateAssetMenu(fileName = "Level", menuName = "Settings/LevelSettings", order = 2)]
  public class LevelSettings : ScriptableObject
  {
    [SerializeField]
    private int _fieldWidth = 10;
    [SerializeField]
    private int _fieldHeight = 10;
    [SerializeField]
    private int _gatesCount = 1;

    public int FieldWidth => _fieldWidth;

    public int FieldHeight => _fieldHeight;

    public int GatesCount => _gatesCount;
  }
}