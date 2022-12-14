namespace Threading;

/// <summary>
///     Implementation of Dijkstra's PV Semaphore based on the Monitor class.
/// </summary>
/// <remarks>Coded by Stephen Toub (stoub@microsoft.com), 4/27/04</remarks>
public class Semaphore
{
    #region Member Variables

    /// <summary>The number of units alloted by this semaphore.</summary>
    private int _count;

    /// <summary>Lock for the semaphore.</summary>
    private readonly object _semLock = new();

    #endregion

    #region Construction

    /// <summary> Initialize the semaphore as a binary semaphore.</summary>
    public Semaphore() : this(1)
    {
    }

    /// <summary> Initialize the semaphore as a counting semaphore.</summary>
    /// <param name="count">Initial number of threads that can take out units from this semaphore.</param>
    /// <exception cref="ArgumentException">Throws if the count argument is less than 0.</exception>
    public Semaphore(int count)
    {
        if (count < 0) throw new ArgumentException("Semaphore must have a count of at least 0.", "count");
        _count = count;
    }

    #endregion

    #region Synchronization Operations

    /// <summary>
    ///     V the semaphore (add 1 unit to it).
    /// </summary>
    public void AddOne()
    {
        V();
    }

    /// <summary>
    ///     P the semaphore (take out 1 unit from it).
    /// </summary>
    public void WaitOne()
    {
        P();
    }

    /// <summary>
    ///     P the semaphore (take out 1 unit from it).
    /// </summary>
    public void P()
    {
        // Lock so we can work in peace.  This works because lock is actually
        // built around Monitor.
        lock (_semLock)
        {
            // Wait until a unit becomes available.  We need to wait
            // in a loop in case someone else wakes up before us.  This could
            // happen if the Monitor.Pulse statements were changed to Monitor.PulseAll
            // statements in order to introduce some randomness into the order
            // in which threads are woken.
            while (_count <= 0) Monitor.Wait(_semLock, Timeout.Infinite);
            _count--;
        }
    }

    /// <summary>
    ///     V the semaphore (add 1 unit to it).
    /// </summary>
    public void V()
    {
        // Lock so we can work in peace.  This works because lock is actually
        // built around Monitor.
        lock (_semLock)
        {
            // Release our hold on the unit of control.  Then tell everyone
            // waiting on this object that there is a unit available.
            _count++;
            Monitor.Pulse(_semLock);
        }
    }

    /// <summary>
    ///     Resets the semaphore to the specified count.  Should be used cautiously.
    /// </summary>
    /// <param name="count">The count.</param>
    public void Reset(int count)
    {
        lock (_semLock)
        {
            _count = count;
        }
    }

    #endregion
}