namespace DataStructures.Graph;

public interface IDirectedWeightedGraph<T>
{
    #region properties

    int Count { get; }

    Vertex<T>?[] Vertices { get; }

    #endregion

    #region members

    void AddEdge(Vertex<T> startVertex, Vertex<T> endVertex, double weight);

    Vertex<T> AddVertex(T data);

    bool AreAdjacent(Vertex<T> startVertex, Vertex<T> endVertex);

    double AdjacentDistance(Vertex<T> startVertex, Vertex<T> endVertex);

    IEnumerable<Vertex<T>?> GetNeighbors(Vertex<T> vertex);

    void RemoveEdge(Vertex<T> startVertex, Vertex<T> endVertex);

    void RemoveVertex(Vertex<T> vertex);

    #endregion
}