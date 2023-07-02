using System;
using System.Collections.Generic;

namespace com.Managers.TaskStarter
{
  public class TaskStarter: ITaskStarter
  {
    private List<ITask> _tasks = new List<ITask>();
    private int _taskIndex = 0;
    private Action _completeHandler = null;

    protected readonly ILogger _logger;

    virtual protected string Name => this.GetType().Name;

    public bool Paused { get; set; } = false;

    public bool IsStarted { get; private set; } = false;

    public bool IsComplete { get; private set; } = false;

    public bool HasTasks => _tasks.Count > 0;

    public TaskStarter(ILogger logger)
    {
      _logger = logger;
    }

    public void AppendTask(ITask task) {
      if (task != null)
      {
        _tasks.Add(task);
      }
    }

    public void Reset()
    {
      Paused = false;
      IsComplete = true;
      IsStarted = false;
      _taskIndex = 0;
    }

    public void Start(Action onComplete) {
      if (!HasTasks)
      {
        onComplete?.Invoke();
        return;
      }
      if (IsStarted)
      {
        return;
      }
      Paused = false;
      IsComplete = false;
      IsStarted = true;
      _completeHandler = onComplete;
      OnStart();
      StartNextTask();
    }

    private void StartNextTask() {
      if (_tasks.Count > _taskIndex) {
        if (!Paused)
        {
          ITask task = _tasks[_taskIndex];
          _taskIndex++;
          OnBeforeTaskStart(task);
          task.Start(_logger, TaskCompleteHandler);
        }
      } else {
        OnComplete();
        _taskIndex = 0;
        Paused = false;
        IsComplete = false;
        _completeHandler?.Invoke();
        _completeHandler = null;
      }
    }

    private void TaskCompleteHandler()
    {
      if (_taskIndex > 0 && _taskIndex <= _tasks.Count)
      {
        OnTaskComplete(_tasks[_taskIndex - 1]);
      } else {
        _logger?.LogError($"{Name}.TaskCompleteHandler() | Invalid taskIndex {_taskIndex - 1}, tasks count {_tasks.Count}.");
      }
      StartNextTask();
    }

    virtual protected void OnStart()
    {
      _logger?.Log($"{Name}.OnStart()");
    }

    virtual protected void OnComplete()
    {
      _logger?.Log($"{Name}.OnComplete()");
    }

    virtual protected void OnBeforeTaskStart(ITask task)
    {
      _logger?.Log($"{Name}.OnBeforeTaskStart({task?.Name})");
    }

    virtual protected void OnTaskComplete(ITask task)
    {
      _logger?.Log($"{Name}.OnTaskComplete({task?.Name})");
    }
  }
}