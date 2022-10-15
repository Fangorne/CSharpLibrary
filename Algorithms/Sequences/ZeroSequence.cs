using System.Numerics;

namespace Algorithms.Sequences;

/// <summary>
///     <para>
///         The zero sequence.
///     </para>
///     <para>
///         OEIS: https://oeis.org/A000004.
///     </para>
/// </summary>
public class ZeroSequence : ISequence
{
    #region properties

    public IEnumerable<BigInteger> Sequence
    {
        get
        {
            while (true) yield return 0;
        }
    }

    #endregion
}