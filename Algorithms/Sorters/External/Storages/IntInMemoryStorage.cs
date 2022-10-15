namespace Algorithms.Sorters.External.Storages;

public class IntInMemoryStorage : ISequentialStorage<int>
{
    #region fields

    private readonly int[] storage;

    #endregion

    #region properties

    public int Length => storage.Length;

    #endregion

    #region constructors

    public IntInMemoryStorage(int[] array)
    {
        storage = array;
    }

    #endregion

    #region Interface Implementations

    public ISequentialStorageReader<int> GetReader()
    {
        return new InMemoryReader(storage);
    }

    public ISequentialStorageWriter<int> GetWriter()
    {
        return new InMemoryWriter(storage);
    }

    #endregion

    private class InMemoryReader : ISequentialStorageReader<int>
    {
        #region fields

        private readonly int[] storage;
        private int offset;

        #endregion

        #region constructors

        public InMemoryReader(int[] storage)
        {
            this.storage = storage;
        }

        #endregion

        #region Interface Implementations

        public void Dispose()
        {
            // Nothing to dispose here
        }

        public int Read()
        {
            return storage[offset++];
        }

        #endregion
    }

    private class InMemoryWriter : ISequentialStorageWriter<int>
    {
        #region fields

        private readonly int[] storage;
        private int offset;

        #endregion

        #region constructors

        public InMemoryWriter(int[] storage)
        {
            this.storage = storage;
        }

        #endregion

        #region Interface Implementations

        public void Write(int value)
        {
            storage[offset++] = value;
        }

        public void Dispose()
        {
            // Nothing to dispose here
        }

        #endregion
    }
}