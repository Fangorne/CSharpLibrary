namespace Algorithms.Sorters.External;

public interface ISequentialStorageWriter<in T> : IDisposable
{
    #region

    void Write(T value);

    #endregion
}