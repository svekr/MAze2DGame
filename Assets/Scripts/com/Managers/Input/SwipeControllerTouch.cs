using UnityEngine;

namespace com.Managers.Input
{
  public class SwipeControllerTouch: SwipeController
  {
    public SwipeControllerTouch(ILogger logger): base(logger)
    {

    }

    override public void OnTimeUpdate(float deltaTime)
    {
      foreach (Touch touch in UnityEngine.Input.touches)
      {
        if (touch.phase == TouchPhase.Began)
        {
          _startPosition = touch.position;
          _endPosition = touch.position;
          _startTime = CurrentTime;
        }
        if (touch.phase == TouchPhase.Ended)
        {
          _startPosition = touch.position;
          _endTime = CurrentTime;
          CheckSwipe();
        }
      }
    }
  }
}