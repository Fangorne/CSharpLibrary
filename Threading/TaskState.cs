namespace Threading;

/// <summary>
///     state of a task
/// </summary>
public enum TaskState
{
    /// <summary>
    ///     NotStarted
    /// </summary>
    NotStarted,

    /// <summary>
    ///     Executing
    /// </summary>
    Executing,

    /// <summary>
    ///     Executed
    /// </summary>
    Executed,

    /// <summary>
    ///     FailedOnException
    /// </summary>
    FailedOnException,

    /// <summary>
    ///     Failed
    /// </summary>
    Failed
}