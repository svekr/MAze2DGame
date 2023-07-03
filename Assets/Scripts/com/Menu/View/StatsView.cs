using UnityEngine;
using TMPro;
using com.Managers.UserData.Model;

namespace com.Menu.View
{
  public class StatsView: MonoBehaviour
  {
    [SerializeField]
    private TMP_Text _bestTime;
    [SerializeField]
    private TMP_Text _lastTime;
    [SerializeField]
    private TMP_Text _bestDistance;
    [SerializeField]
    private TMP_Text _lastDistance;

    public void Show(UserStats stats)
    {
      if (stats == null)
      {
        return;
      }
      _bestTime.text = Utils.TimeUtil.FormatAsHHMMSS(stats.bestTime);
      _lastTime.text = Utils.TimeUtil.FormatAsHHMMSS(stats.lastTime);
      _bestDistance.text = stats.bestDistance.ToString();
      _lastDistance.text = stats.lastDistance.ToString();
      gameObject.SetActive(true);
    }

    public void Hide()
    {
      gameObject.SetActive(false);
    }
  }
}