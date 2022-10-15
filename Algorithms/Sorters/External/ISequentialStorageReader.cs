namespace Algorithms.Sorters.External;

public interface ISequentialStorageReader<out T> : IDisposable
{
    #region

    T Read();

    #endregion
}