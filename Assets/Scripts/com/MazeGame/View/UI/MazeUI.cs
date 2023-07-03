using com.MazeGame.Controller;
using TMPro;
using UnityEngine;
using Utils;

namespace com.MazeGame.View.UI
{
  public class MazeUI : MonoBehaviour
  {
    [SerializeField]
    private TMP_Text _elapsedTimeTF;
    [SerializeField]
    private TMP_Text _remainTimeTF;
    [SerializeField]
    private GameObject _remainTimeContainer = null;
    [SerializeField]
    private TMP_Text _distancePassedTF;
    [SerializeField]
    private GameObject _loseLevelDecoration;
    [SerializeField]
    private GameObject _winLevelDecoration;

    private ILogger _logger;
    private IMazeGameController _controller;

    public void Initialize(ILogger logger, IMazeGameController controller)
    {
      _logger = logger;
      _controller = controller;
      StartListenController();
      UpdateData();
    }

    public void Release()
    {
      StopListenController();
    }

    private void UpdateData()
    {
      UpdateElapsedTime(_controller.TimeElapsed);
      UpdateRemainTime(_controller.TimeRemain);
      UpdateDistancePassed(_controller.DistancePassed);
      _remainTimeContainer?.SetActive(_controller.HasTimeLimit);
      _loseLevelDecoration.SetActive(false);
      _winLevelDecoration.SetActive(false);
    }

    private void StartListenController()
    {
      _controller.OnTimeElapsedChanged += UpdateElapsedTime;
      _controller.OnTimeRemainChanged += UpdateRemainTime;
      _controller.OnDistancePassedChanged += UpdateDistancePassed;
      _controller.OnLevelFailed += OnLevelFailed;
      _controller.OnLevelWin += OnLevelWin;
    }

    private void StopListenController()
    {
      _controller.OnTimeElapsedChanged -= UpdateElapsedTime;
      _controller.OnTimeRemainChanged -= UpdateRemainTime;
      _controller.OnDistancePassedChanged -= UpdateDistancePassed;
      _controller.OnLevelFailed -= OnLevelFailed;
      _controller.OnLevelWin -= OnLevelWin;
    }

    private void UpdateElapsedTime(long seconds)
    {
      _elapsedTimeTF.text = TimeUtil.FormatAsMMSS(seconds);
    }

    private void UpdateRemainTime(long seconds)
    {
      _remainTimeTF.text = TimeUtil.FormatAsMMSS(seconds);
    }

    private void UpdateDistancePassed(int distance)
    {
      _distancePassedTF.text = distance.ToString();
    }

    private void OnLevelFailed()
    {
      _loseLevelDecoration.SetActive(true);
    }

    private void OnLevelWin()
    {
      _winLevelDecoration.SetActive(true);
    }
  }
}