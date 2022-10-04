using System.Diagnostics;

namespace Threading;

/// <summary>
///     Base class for all dispatcher
/// </summary>
/// <threadsafety instance="true" />
public abstract class Dispatcher : IDispatcher
{
    #region constructor

    /// <summary>
    ///     Constructors
    /// </summary>
    protected Dispatcher()
    {
    }

    #endregion

    #region Implementation of IDisposable

    /// <summary>
    ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    #endregion

    #region finalizer

    /// <summary>
    ///     Allows an <see cref="T:System.Object" /> to attempt to free resources and perform other cleanup operations before
    ///     the <see cref="T:System.Object" /> is reclaimed by garbage collection.
    /// </summary>
    ~Dispatcher()
    {
        Dispose(false);
    }

    #endregion

    /// <summary>
    /// Instanciates and returns an <see cref="Exception"/> of the same type as the exception exception, that encapsulates exception.
    /// </summary>
    /// <param name="exception">The <see cref="Exception"/> to encapsulate</param>
    /// <returns>An exception that encapsulate exception (that could be retrieved as the InnerException)</returns>
    /// <remarks>
    ///     Since it's only possible to read the <see cref="StackTrace"/> property of an exception from a catch blocks, it's not possible
    ///     to access the <see cref="StackTrace"/> property of an exception from a <see cref="AppDomain.UnhandledException"/> event handler.
    ///     One way to do that is to catch any exception for each thread's routine (to build the <see cref="StackTrace"/>) and to re-throw
    ///     it to let the <see cref="AppDomain.UnhandledException"/> event handler handle it.
    ///     
    ///     There is still a problem : when you throw an exception, the CLR resets the starting point for this exception (and so its
    ///     <see cref="StackTrace"/>).
    ///     
    ///     The purpose of this method is to provide a way to keep the Exception's <see cref="StackTrace"/> by encapsulating the original <see cref="Exception"/> in an 
    ///     enveloppe (an <see cref="Exception"/> of the same type that will inner the original <see cref="Exception"/>).
    ///     As a consequence, only the enveloppe will suffer from a reseted <see cref="StackTrace"/> : the InnerException of the enveloppe 
    ///     could be retrieved with its original <see cref="StackTrace"/>.
    ///     
    ///     Usage guide : Encloses each thread's routine (worker threads or threads pool) by the following try/catch statement to ensure the
    ///     InnerException of the <see cref="Exception"/> retrieved in the UnhandledException event of the main AppDomain will have its 
    ///     <see cref="StackTrace"/>.
    ///     
    ///     try
    ///     {
    ///         // the thread routine code
    ///     }
    ///     catch(Exception ex)
    ///     {
    ///         throw GetEncapsulatedException(ex);
    ///     }
    /// </remarks>
    public static Exception EncapsulateExceptionToProtectStackTrace(Exception exception)
    {
        object[] exceptionConstructorArgs = new object[2];
        exceptionConstructorArgs[0] = exception.Message;
        exceptionConstructorArgs[1] = exception;

        try
        {
            if (exception is System.Threading.ThreadAbortException)
            {
                return new Exception(exception.ToString(), exception);
            }

            return (Exception)Activator.CreateInstance(exception.GetType(), exceptionConstructorArgs);
        }
        catch (Exception)
        {
            // This exception has no default constructor with message (string) and inner exception (exception)
            return new Exception(exception.ToString(), exception);
        }
    }

    #region public methods

    /// <summary>
    ///     Posts the specified <see cref="TaskHandler" />.
    /// </summary>
    /// <param name="taskHandler">The <see cref="TaskHandler" />.</param>
    public void Post(TaskHandler taskHandler)
    {
        if (taskHandler == null) throw new ArgumentNullException(nameof(taskHandler));
        Post(new LambdaTask(taskHandler));
    }

    /// <summary>
    ///     Posts the specified task handler.
    /// </summary>
    /// <param name="parameterizedTaskHandler">The task handler.</param>
    public void Post(ParameterizedTaskHandler parameterizedTaskHandler)
    {
        if (parameterizedTaskHandler == null) throw new ArgumentNullException(nameof(parameterizedTaskHandler));
        Post(new LambdaTask(parameterizedTaskHandler));
    }

    #endregion

    #region Implementation of IDispatcher

    /// <summary>
    ///     Aborts this instance.
    /// </summary>
    /// <returns></returns>
    public virtual bool Abort()
    {
        return false;
    }


    /// <summary>
    ///     Posts the specified <see cref="Task" />.
    /// </summary>
    /// <param name="task">The <see cref="Task" />.</param>
    public abstract void Post(ITask task);

    /// <summary>
    ///     Releases unmanaged and - optionally - managed resources
    /// </summary>
    /// <param name="disposing">
    ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
    ///     unmanaged resources.
    /// </param>
    protected abstract void Dispose(bool disposing);

    #endregion
}