namespace Threading
{
    /// <summary>
    /// Base interface for all dispatcher
    /// </summary>
    public interface IDispatcher : IDisposable
    {
        /// <summary>
        /// Posts the specified <see cref="TaskHandler"/>.
        /// </summary>
        /// <param name="taskHandler">The <see cref="TaskHandler"/>.</param>
        void Post(TaskHandler taskHandler);

        /// <summary>
        /// Posts the specified task handler.
        /// </summary>
        /// <param name="parameterizedTaskHandler">The task handler.</param>
        void Post(ParameterizedTaskHandler parameterizedTaskHandler);

        /// <summary>
        /// Aborts this instance.
        /// </summary>
        /// <returns></returns>
        bool Abort();

        /// <summary>
        /// Posts the specified <see cref="Task"/>.
        /// </summary>
        /// <param name="task">The <see cref="Task"/>.</param>
        void Post(ITask task);
    }
}