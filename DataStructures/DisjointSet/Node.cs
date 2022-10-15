namespace DataStructures.DisjointSet;

/// <summary>
///     node class to be used by disjoint set to represent nodes in Disjoint Set forest.
/// </summary>
/// <typeparam name="T">generic type for data to be stored.</typeparam>
public class Node<T>
{
    #region properties

    public int Rank { get; set; }

    public Node<T> Parent { get; set; }

    public T Data { get; set; }

    #endregion

    #region constructors

    public Node(T data)
    {
        Data = data;
        Parent = this;
    }

    #endregion
}