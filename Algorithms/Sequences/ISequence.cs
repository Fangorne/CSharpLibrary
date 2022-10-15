using System.Numerics;

namespace Algorithms.Sequences;

/// <summary>
///     Common interface for all integer sequences.
/// </summary>
public interface ISequence
{
    #region properties

    /// <summary>
    ///     Gets sequence as enumerable.
    /// </summary>
    IEnumerable<BigInteger> Sequence { get; }

    #endregion
}