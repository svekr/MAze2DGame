using System;
using UnityEngine;

namespace com.Managers.SceneManagement
{
  public class TransitionSceneObject: MonoBehaviour
  {
    private readonly string SHOW_COMPLETE_EVENT = "ShowComplete";
    private readonly string HIDE_COMPLETE_EVENT = "HideComplete";
    private readonly string CAN_HIDE = "CanHide";

    [SerializeField]
    private Animator _animator;

    private bool _isHideRequired = false;
    private Action _showCompleteHandler = null;
    private Action _hideCompleteHandler = null;

    public void Show(Action onShowComplete, Action onHideComplete)
    {
      _showCompleteHandler = onShowComplete;
      _hideCompleteHandler = onHideComplete;
      if (_animator == null)
      {

      } else {
        _isHideRequired = false;
      }
    }

    public void Hide(AsyncOperation sceneLoadAsync)
    {
      sceneLoadAsync.completed -= Hide;
      Hide();
    }

    public void Hide()
    {
      if (_animator == null)
      {
        OnHideComplete();
      } else {
        _isHideRequired = true;
        _animator.SetTrigger(CAN_HIDE);
      }
    }

    private void AnimationEventHandler(string eventName)
    {
      if (eventName == SHOW_COMPLETE_EVENT)
      {
        _showCompleteHandler?.Invoke();
        _showCompleteHandler = null;
        return;
      }
      if (eventName == HIDE_COMPLETE_EVENT)
      {
        OnHideComplete();
        return;
      }
      if (eventName == CAN_HIDE)
      {
        if (_isHideRequired)
        {
          OnHideComplete();
        }
        return;
      }
    }

    private void OnHideComplete()
    {
      UnityEngine.Object.Destroy(gameObject);
      _hideCompleteHandler?.Invoke();
      _hideCompleteHandler = null;
    }
  }
}