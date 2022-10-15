namespace Algorithms.Sorters.External.Storages;

public class IntFileStorage : ISequentialStorage<int>
{
    #region fields

    private readonly string filename;

    #endregion

    #region properties

    public int Length { get; }

    #endregion

    #region constructors

    public IntFileStorage(string filename, int length)
    {
        Length = length;
        this.filename = filename;
    }

    #endregion

    #region Interface Implementations

    public ISequentialStorageReader<int> GetReader()
    {
        return new FileReader(filename);
    }

    public ISequentialStorageWriter<int> GetWriter()
    {
        return new FileWriter(filename);
    }

    #endregion

    private class FileReader : ISequentialStorageReader<int>
    {
        #region fields

        private readonly BinaryReader reader;

        #endregion

        #region constructors

        public FileReader(string filename)
        {
            reader = new BinaryReader(File.OpenRead(filename));
        }

        #endregion

        #region Interface Implementations

        public void Dispose()
        {
            reader.Dispose();
        }

        public int Read()
        {
            return reader.ReadInt32();
        }

        #endregion
    }

    private class FileWriter : ISequentialStorageWriter<int>
    {
        #region fields

        private readonly BinaryWriter writer;

        #endregion

        #region constructors

        public FileWriter(string filename)
        {
            writer = new BinaryWriter(File.OpenWrite(filename));
        }

        #endregion

        #region Interface Implementations

        public void Write(int value)
        {
            writer.Write(value);
        }

        public void Dispose()
        {
            writer.Dispose();
        }

        #endregion
    }
}