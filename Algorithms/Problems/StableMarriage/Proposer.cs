namespace Algorithms.Problems.StableMarriage;

public class Proposer
{
    #region properties

    public Accepter? EngagedTo { get; set; }

    public LinkedList<Accepter> PreferenceOrder { get; set; } = new();

    #endregion
}