namespace Threading;

/// <summary>
///     Declare a delegate that takes no parameter and has return type.
/// </summary>
public delegate T TaskHandler<out T>();

/// <summary>
///     Declare a delegate that takes one parameter and has return type.
/// </summary>
public delegate T TaskHandlerNotifying<out T>(NotifyHandler notifyHandler);

/// <summary>
///     Declare a delegate that takes one parameter and has no return type.
/// </summary>
public delegate void NotifyHandler(object data);

/// <summary>
///     LambdaTask use to execute a delegate.
/// </summary>
public class LambdaTask : Task
{
    #region Overrides of Task

    /// <summary>
    ///     Called when execute.
    /// </summary>
    protected override void OnExecute()
    {
        if (_parameterizedTaskHandler != null)
            _parameterizedTaskHandler(this);
        else
            _taskHandler?.Invoke();
    }

    #endregion

    #region fields

    private readonly TaskHandler? _taskHandler;
    private readonly ParameterizedTaskHandler? _parameterizedTaskHandler;

    #endregion

    #region constructor

    /// <summary>
    ///     LambdaTask
    /// </summary>
    /// <param name="taskHandler">the executed delegate</param>
    public LambdaTask(TaskHandler taskHandler)
    {
        _taskHandler = taskHandler ?? throw new ArgumentNullException(nameof(taskHandler));
    }

    /// <summary>
    ///     LambdaTask
    /// </summary>
    /// <param name="parameterizedTaskHandler"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public LambdaTask(ParameterizedTaskHandler parameterizedTaskHandler)
    {
        _parameterizedTaskHandler = parameterizedTaskHandler ?? throw new ArgumentNullException(nameof(parameterizedTaskHandler));
    }

    #endregion
}
