using System.Numerics;

namespace Algorithms.Sequences;

/// <summary>
///     <para>
///         The all twos sequence.
///     </para>
///     <para>
///         OEIS: https://oeis.org/A007395.
///     </para>
/// </summary>
public class AllTwosSequence : ISequence
{
    #region properties

    public IEnumerable<BigInteger> Sequence
    {
        get
        {
            while (true) yield return 2;
        }
    }

    #endregion
}