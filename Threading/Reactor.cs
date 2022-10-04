using System.Globalization;

namespace Threading
{
	/// <summary>
	/// List the type of thread pool
	/// </summary>
	public enum ThreadPoolType
	{
		/// <summary>
		/// the native .net thread pool
		/// </summary>
		DotNet,
		/// <summary>
		/// the toob thread pool
		/// </summary>
		Toub
	}

	/// <summary>
	/// Dispatch commands in a thread pool, the type of thread pool can be one of the <see cref="ThreadPoolType"/>
	/// </summary>
	/// <threadsafety instance="true"/>
	public class Reactor : Dispatcher
	{
		#region fields

		private readonly ThreadPoolType _threadPoolType;
		private bool _disposed;

		#endregion

		#region properties

		#endregion

		#region constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="Reactor"/> class.
		/// </summary>
		/// <param name="threadPoolType">Type of the thread pool to use.</param>
		public Reactor(ThreadPoolType threadPoolType)
		{
			this._threadPoolType = threadPoolType;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Reactor"/> class which uses the .NET ThreadPool
		/// </summary>
		public Reactor()
			: this(ThreadPoolType.Toub)
		{
		}

		/// <summary>
		/// Releases unmanaged resources and performs other cleanup operations before the
		/// <see cref="Reactor"/> is reclaimed by garbage collection.
		/// </summary>
		~Reactor()
		{
			Dispose(false);
		}

		#endregion

		#region methods

		private static void DoProcess(object? obj)
		{
			try
			{
				// Sets the correct culture info (for further decimal parsing)
				Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US", false);
				ICommand? command = obj as ICommand;
                command?.Execute();
            }
			catch (Exception ex)
			{
				throw Dispatcher.EncapsulateExceptionToProtectStackTrace(ex);
			}
		}

		#endregion

		#region Implementation of Dispatcher

		/// <summary>
		/// Posts the specified <see cref="System.Threading.Tasks.Task"></see>.
		/// </summary>
		/// <param name="task">The <see cref="System.Threading.Tasks.Task"></see>.</param>
        public override void Post(ITask task)
		{
			// Delegate the processing of the event to the Thread pool
			switch (_threadPoolType)
			{
				case ThreadPoolType.DotNet:
					ThreadPool.QueueUserWorkItem(DoProcess, task);
					break;

				case ThreadPoolType.Toub:
					ManagedThreadPool.QueueUserWorkItem(DoProcess, task);
					break;
			}
		}

		#endregion

		#region Implementation of IDisposable

		/// <summary>
		/// Disposes the specified disposing.
		/// </summary>
		/// <param name="disposing">if set to <c>true</c> [disposing].</param>
		protected override void Dispose(bool disposing)
		{

			if (disposing)
			{
			}
			else
			{
			}

			if (!_disposed)
			{
				_disposed = true;
            }
		}

		#endregion
	}
}
