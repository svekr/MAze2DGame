using System;

namespace com.Managers.TaskStarter
{
  public interface ITaskStarter
  {
    bool Paused { get; set; }

    bool IsStarted { get; }

    bool IsComplete { get; }

    bool HasTasks { get; }

    void AppendTask(ITask task);

    void Reset();

    void Start(Action onComplete);
  }
}