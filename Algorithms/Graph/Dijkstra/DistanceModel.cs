using DataStructures.Graph;

namespace Algorithms.Graph.Dijkstra;

/// <summary>
///     Entity which represents the Dijkstra shortest distance.
///     Contains: Vertex, Previous Vertex and minimal distance from start vertex.
/// </summary>
/// <typeparam name="T">Generic parameter.</typeparam>
public class DistanceModel<T>
{
    #region properties

    public Vertex<T>? Vertex { get; }

    public Vertex<T>? PreviousVertex { get; set; }

    public double Distance { get; set; }

    #endregion

    #region constructors

    public DistanceModel(Vertex<T>? vertex, Vertex<T>? previousVertex, double distance)
    {
        Vertex = vertex;
        PreviousVertex = previousVertex;
        Distance = distance;
    }

    #endregion

    #region

    public override string ToString()
    {
        return $"Vertex: {Vertex} - Distance: {Distance} - Previous: {PreviousVertex}";
    }

    #endregion
}