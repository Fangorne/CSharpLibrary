namespace Algorithms.Search.AStar;

/// <summary>
///     Contains Positional and other information about a single node.
/// </summary>
public class Node : IComparable<Node>, IEquatable<Node>
{
    #region properties

    /// <summary>
    ///     Gets the Total cost of the Node.
    ///     The Current Costs + the estimated costs.
    /// </summary>
    public double TotalCost => EstimatedCost + CurrentCost;

    /// <summary>
    ///     Gets or sets the Distance between this node and the target node.
    /// </summary>
    public double EstimatedCost { get; set; }

    /// <summary>
    ///     Gets a value indicating whether how costly it is to traverse over this node.
    /// </summary>
    public double TraversalCostMultiplier { get; }

    /// <summary>
    ///     Gets or sets a value indicating whether to go from the start node to this node.
    /// </summary>
    public double CurrentCost { get; set; }

    /// <summary>
    ///     Gets or sets the state of the Node
    ///     Can be Unconsidered(Default), Open and Closed.
    /// </summary>
    public NodeState State { get; set; }

    /// <summary>
    ///     Gets a value indicating whether the node is traversable.
    /// </summary>
    public bool Traversable { get; }

    /// <summary>
    ///     Gets or sets a list of all connected nodes.
    /// </summary>
    public Node[] ConnectedNodes { get; set; } = new Node[0];

    /// <summary>
    ///     Gets or sets he "previous" node that was processed before this node.
    /// </summary>
    public Node? Parent { get; set; }

    /// <summary>
    ///     Gets the positional information of the node.
    /// </summary>
    public VecN Position { get; }

    #endregion

    #region constructors

    public Node(VecN position, bool traversable, double traverseMultiplier)
    {
        Traversable = traversable;
        Position = position;
        TraversalCostMultiplier = traverseMultiplier;
    }

    #endregion

    #region Interface Implementations

    /// <summary>
    ///     Compares the Nodes based on their total costs.
    ///     Total Costs: A* Pathfinding.
    ///     Current: Djikstra Pathfinding.
    ///     Estimated: Greedy Pathfinding.
    /// </summary>
    /// <param name="other">The other node.</param>
    /// <returns>A comparison between the costs.</returns>
    public int CompareTo(Node? other)
    {
        return TotalCost.CompareTo(other?.TotalCost ?? 0);
    }

    public bool Equals(Node? other)
    {
        return CompareTo(other) == 0;
    }

    #endregion

    #region

    public static bool operator ==(Node left, Node right)
    {
        return left?.Equals(right) != false;
    }

    public static bool operator >(Node left, Node right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator <(Node left, Node right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator !=(Node left, Node right)
    {
        return !(left == right);
    }

    public static bool operator <=(Node left, Node right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >=(Node left, Node right)
    {
        return left.CompareTo(right) >= 0;
    }

    public override bool Equals(object? obj)
    {
        return obj is Node other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Position.GetHashCode()
               + Traversable.GetHashCode()
               + TraversalCostMultiplier.GetHashCode();
    }

    /// <summary>
    ///     Returns the distance to the other node.
    /// </summary>
    /// <param name="other">The other node.</param>
    /// <returns>Distance between this and other.</returns>
    public double DistanceTo(Node other)
    {
        return Math.Sqrt(Position.SqrDistance(other.Position));
    }

    #endregion
}