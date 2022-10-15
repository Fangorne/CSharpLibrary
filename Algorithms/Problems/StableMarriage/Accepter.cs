namespace Algorithms.Problems.StableMarriage;

public class Accepter
{
    #region properties

    public Proposer? EngagedTo { get; set; }

    public List<Proposer> PreferenceOrder { get; set; } = new();

    #endregion

    #region

    public bool PrefersOverCurrent(Proposer newProposer)
    {
        return EngagedTo is null ||
               PreferenceOrder.IndexOf(newProposer) < PreferenceOrder.IndexOf(EngagedTo);
    }

    #endregion
}