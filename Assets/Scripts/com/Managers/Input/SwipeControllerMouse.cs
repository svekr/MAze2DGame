namespace com.Managers.Input
{
  public class SwipeControllerMouse: SwipeController
  {
    public SwipeControllerMouse(ILogger logger): base(logger)
    {

    }

    override public void OnTimeUpdate(float deltaTime)
    {
      if (UnityEngine.Input.GetMouseButtonDown(0))
      {
        _startPosition = UnityEngine.Input.mousePosition;
        _endPosition = UnityEngine.Input.mousePosition;
        _startTime = CurrentTime;
      }
      if (UnityEngine.Input.GetMouseButtonUp(0))
      {
        _startPosition = UnityEngine.Input.mousePosition;
        _endTime = CurrentTime;
        CheckSwipe();
      }
    }
  }
}