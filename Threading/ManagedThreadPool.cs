using System.Collections;

namespace Threading;

/// <summary>
///     Another implementation of a .NET thread pool.
///     This thread pool is more convenient for real-time constraints than the official .NET thread pool (which is more
///     convenient for
///     OS cross-process availability).
/// </summary>
/// <remarks>Coded by Stephen Toub (stoub@microsoft.com), 4/27/04</remarks>
public static class ManagedThreadPool
{
    #region Constants

    /// <summary>Maximum number of threads the thread pool has at its disposal.</summary>
    private const int MaxWorkerThreads = 10;

    #endregion

    /// <summary>
    ///     Used to hold a callback delegate and the state for that delegate.
    /// </summary>
    private class WaitingCallback
    {
        #region Construction

        /// <summary>Initialize the callback holding object.</summary>
        /// <param name="callback">Callback delegate for the callback.</param>
        /// <param name="state">State with which to call the callback delegate.</param>
        public WaitingCallback(WaitCallback callback, object state)
        {
            _callback = callback;
            _state = state;
        }

        #endregion

        #region Member Variables

        /// <summary>Callback delegate for the callback.</summary>
        private readonly WaitCallback _callback;

        /// <summary>State with which to call the callback delegate.</summary>
        private readonly object _state;

        #endregion

        #region Properties

        /// <summary>Gets the callback delegate for the callback.</summary>
        public WaitCallback Callback => _callback;

        /// <summary>Gets the state with which to call the callback delegate.</summary>
        public object State => _state;

        #endregion
    }

    #region Member Variables

    /// <summary>Queue of all the callbacks waiting to be executed.</summary>
    private static readonly Queue _waitingCallbacks = new();

    /// <summary>
    ///     Used to signal that a worker thread is needed for processing.  Note that multiple
    ///     threads may be needed simultaneously and as such we use a semaphore instead of
    ///     an auto reset event.
    /// </summary>
    private static readonly Semaphore WorkerThreadNeeded = new(0);

    /// <summary>List of all worker threads at the disposal of the thread pool.</summary>
    private static readonly ArrayList WorkerThreads = new();

    /// <summary>Number of threads currently active.</summary>
    private static int _inUseThreads;

    /// <summary>Lockable object for the pool.</summary>
    private static readonly object PoolLock = new();

    #endregion

    #region Construction and Finalization

    /// <summary>Initialize the thread pool.</summary>
    static ManagedThreadPool()
    {
        Initialize();
    }

    /// <summary>Initializes the thread pool.</summary>
    private static void Initialize()
    {
        // Create our thread stores; we handle synchronization ourself
        // as we may run into situations where multiple operations need to be atomic.
        // We keep track of the threads we've created just for good measure; not actually
        // needed for any core functionality.
        // Create all of the worker threads

        for (var i = 0; i < MaxWorkerThreads; i++)
        {
            // Create a new thread and add it to the list of threads.
            var newThread = new Thread(ProcessQueuedItems);
            WorkerThreads.Add(newThread);

            // Configure the new thread and start it
            newThread.Name = "ManagedThreadPool #" + i;
            newThread.IsBackground = true;
            newThread.Start();
        }
    }

    #endregion

    #region Public Methods

    /// <summary>Queues a user work item to the thread pool.</summary>
    /// <param name="callback">
    ///     A WaitCallback representing the delegate to invoke when the thread in the
    ///     thread pool picks up the work item.
    /// </param>
    public static void QueueUserWorkItem(WaitCallback callback)
    {
        // Queue the delegate with no state
        QueueUserWorkItem(callback, null);
    }

    /// <summary>Queues a user work item to the thread pool.</summary>
    /// <param name="callback">
    ///     A WaitCallback representing the delegate to invoke when the thread in the
    ///     thread pool picks up the work item.
    /// </param>
    /// <param name="state">
    ///     The object that is passed to the delegate when serviced from the thread pool.
    /// </param>
    public static void QueueUserWorkItem(WaitCallback callback, object state)
    {
        // Create a waiting callback that contains the delegate and its state.
        // At it to the processing queue, and signal that data is waiting.
        var waiting = new WaitingCallback(callback, state);
        lock (PoolLock)
        {
            _waitingCallbacks.Enqueue(waiting);
        }

        WorkerThreadNeeded.AddOne();
    }

    #endregion

    #region Properties

    /// <summary>Gets the number of threads at the disposal of the thread pool.</summary>
    public static int MaxThreads => MaxWorkerThreads;

    /// <summary>Gets the number of currently active threads in the thread pool.</summary>
    public static int ActiveThreads => _inUseThreads;

    /// <summary>Gets the number of callback delegates currently waiting in the thread pool.</summary>
    public static int WaitingCallbacks
    {
        get
        {
            lock (PoolLock)
            {
                return _waitingCallbacks.Count;
            }
        }
    }

    #endregion

    #region Thread Processing

    /// <summary>Event raised when there is an exception on a threadpool thread.</summary>
    public static event UnhandledExceptionEventHandler? UnhandledException;

    /// <summary>A thread worker function that processes items from the work queue.</summary>
    private static void ProcessQueuedItems()
    {
        // Process indefinitely
        while (true)
        {
            WorkerThreadNeeded.WaitOne();

            // Get the next item in the queue.  If there is nothing there, go to sleep
            // for a while until we're woken up when a callback is waiting.
            WaitingCallback? callback = null;

            // Try to get the next callback available.  We need to lock on the 
            // queue in order to make our count check and retrieval atomic.
            lock (PoolLock)
            {
                if (_waitingCallbacks.Count > 0)
                    try
                    {
                        callback = (WaitingCallback) _waitingCallbacks.Dequeue()!;
                    }
                    catch
                    {
                    } // make sure not to fail here
            }

            if (callback != null)
                // We now have a callback.  Execute it.  Make sure to accurately
                // record how many callbacks are currently executing.
                try
                {
                    Interlocked.Increment(ref _inUseThreads);
                    callback.Callback(callback.State);
                }
                catch (Exception exc)
                {
                    try
                    {
                        var handler = UnhandledException;
                        if (handler != null)
                            handler(typeof(ManagedThreadPool), new UnhandledExceptionEventArgs(exc, false));
                    }
                    catch
                    {
                    }
                }
                finally
                {
                    Interlocked.Decrement(ref _inUseThreads);
                }
        }
    }

    #endregion
}