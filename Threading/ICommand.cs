namespace Threading;

/// <summary>
///     Encapsulates a request as an object.
/// </summary>
public interface ICommand
{
    /// <summary>
    ///     Executes this command.
    /// </summary>
    void Execute();
}