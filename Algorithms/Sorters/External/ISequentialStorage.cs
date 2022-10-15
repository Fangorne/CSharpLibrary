namespace Algorithms.Sorters.External;

public interface ISequentialStorage<T>
{
    #region properties

    public int Length { get; }

    #endregion

    #region

    ISequentialStorageReader<T> GetReader();

    ISequentialStorageWriter<T> GetWriter();

    #endregion
}