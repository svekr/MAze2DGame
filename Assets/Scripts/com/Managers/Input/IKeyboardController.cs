using System;
using UnityEngine;

namespace com.Managers.Input
{
  public interface IKeyboardController
  {
    event Action<KeyCode> OnKeyDown;

    void OnTimeUpdate(float deltaTime);
  }
}