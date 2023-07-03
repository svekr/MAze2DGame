using System;
using UnityEngine;
using com.Managers.TaskStarter;

namespace com.Managers.Input
{
  public class InputManagerInitializer: ITask
  {
    public string Name => this.GetType().Name;

    public void Start(ILogger logger, Action onComplete)
    {
      if (Main.Managers.InputManager == null)
      {
        Main.Managers.SetManager(new InputManager(logger, GetSwipeController(logger), GetKeyController(logger)));
      }
      onComplete?.Invoke();
    }

    private SwipeController GetSwipeController(ILogger logger)
    {
      if (Utils.PlatformUtil.IsMobilePlatform())
      {
        return new SwipeControllerTouch(logger);
      }
      return new SwipeControllerMouse(logger);
    }

    private IKeyboardController GetKeyController(ILogger logger)
    {
      KeyCode[] keyCodes = new KeyCode[] {
        KeyCode.UpArrow,
        KeyCode.RightArrow,
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.Escape
      };
      return new KeyboardController(logger, keyCodes);
    }
  }
}