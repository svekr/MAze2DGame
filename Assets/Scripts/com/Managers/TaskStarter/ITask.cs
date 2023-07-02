using System;

namespace com.Managers.TaskStarter
{
  public interface ITask
  {
    string Name { get; }

    void Start(ILogger logger, Action onComplete);
  }
}
