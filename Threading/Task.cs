using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Threading;

/// <summary>
///     Declare a delegate that takes a single <see cref="ICommand" /> parameter and has no return type.
/// </summary>
/// <param name="command"></param>
public delegate void TaskProcessorHandler(ICommand command);

/// <summary>
///     Declare a delegate that takes no parameter and has no return type.
/// </summary>
public delegate void TaskHandler();

/// <summary>
///     Declare a delegate that takes a parameter and has no return type.
/// </summary>
/// <param name="task"></param>
public delegate void ParameterizedTaskHandler(ITask task);

/// <summary>
///     ITask : to be able to implement a custom Task with special implementation
/// </summary>
public interface ITask : ICommand
{
    /// <summary>
    ///     Gets the state.
    /// </summary>
    /// <value>The state.</value>
    TaskState State { get; }

    /// <summary>
    ///     Gets or sets the thread.
    /// </summary>
    /// <value>
    ///     The thread.
    /// </value>
    Thread? Thread { get; set; }

    /// <summary>
    ///     Gets or sets the target date.
    /// </summary>
    /// <value>
    ///     The target date.
    /// </value>
    DateTime TargetDate { get; set; }

    /// <summary>
    ///     Gets or sets the started.
    /// </summary>
    /// <value>
    ///     The started.
    /// </value>
    DateTime Started { get; set; }

    /// <summary>
    ///     Gets or sets the dispatcher.
    /// </summary>
    /// <value>
    ///     The dispatcher.
    /// </value>
    Dispatcher? Dispatcher { get; set; }

    /// <summary>
    ///     Fires when a Task is processed
    /// </summary>
    event TaskProcessorHandler ExecutedRaised;

    /// <summary>
    ///     Fires when a Task begins to be executed
    /// </summary>
    event TaskProcessorHandler ExecutingRaised;
}

/// <summary>
///     Intends to contain code to be executed by an <see cref="Dispatcher" />
/// </summary>
[DataContract]
public abstract class Task : ITask
{
    // ReSharper disable EmptyConstructor
    static Task()
    {
    }
    // ReSharper restore EmptyConstructor

    #region Implementation of ICommand

    /// <summary>
    ///     Executes this command.
    /// </summary>
    public virtual void Execute()
    {
        InvokeExecutingRaised(this);
        try
        {
            OnExecute();
        }
        finally
        {
            InvokeExecutedRaised(this);
        }
    }

    #endregion

    #region abstract members

    /// <summary>
    ///     Called when task is executed.
    /// </summary>
    protected abstract void OnExecute();

    #endregion

    #region fields

    [NonSerialized] private TaskProcessorHandler? _executedRaised;

    [NonSerialized] private TaskProcessorHandler? _executingRaised;

    /// <summary>
    ///     Fires when a Task is processed
    /// </summary>
    public event TaskProcessorHandler ExecutedRaised
    {
        add => _executedRaised += value;
        remove => _executedRaised -= value;
    }

    /// <summary>
    ///     Fires when a Task begins to be executed
    /// </summary>
    public event TaskProcessorHandler ExecutingRaised
    {
        add => _executingRaised += value;
        remove => _executingRaised -= value;
    }

    #endregion

    #region properties

    /// <summary>
    ///     Gets the state.
    /// </summary>
    /// <value>The state.</value>
    public TaskState State { get; protected set; } = TaskState.NotStarted;

    /// <summary>
    ///     Gets or sets the started.
    /// </summary>
    /// <value>
    ///     The started.
    /// </value>
    public DateTime Started { get; set; }

    /// <summary>
    ///     TargetDateTime
    /// </summary>
    public DateTime TargetDate { get; set; }

    /// <summary>
    ///     Gets or sets the thread.
    /// </summary>
    /// <value>
    ///     The thread.
    /// </value>
    [XmlIgnore]
    [IgnoreDataMember]
    public Thread? Thread { get; set; }

    /// <summary>
    ///     Gets or sets the dispatcher.
    /// </summary>
    /// <value>
    ///     The dispatcher.
    /// </value>
    [XmlIgnore]
    [IgnoreDataMember]
    public Dispatcher? Dispatcher { get; set; }

    #endregion

    #region private members

    /// <summary>
    /// </summary>
    /// <param name="command"></param>
    protected void InvokeExecutedRaised(ICommand command)
    {
        State = TaskState.Executed;
        _executedRaised?.Invoke(command);
        _executedRaised = null;
    }

    /// <summary>
    /// </summary>
    /// <param name="command"></param>
    protected void InvokeExecutingRaised(ICommand command)
    {
        State = TaskState.Executing;
        _executingRaised?.Invoke(command);
        _executingRaised = null;
    }

    #endregion
}