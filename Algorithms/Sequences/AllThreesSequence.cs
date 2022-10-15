using System.Numerics;

namespace Algorithms.Sequences;

/// <summary>
///     <para>
///         The all threes sequence.
///     </para>
///     <para>
///         OEIS: https://oeis.org/A010701.
///     </para>
/// </summary>
public class AllThreesSequence : ISequence
{
    #region properties

    public IEnumerable<BigInteger> Sequence
    {
        get
        {
            while (true) yield return 3;
        }
    }

    #endregion
}