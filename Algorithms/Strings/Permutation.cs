namespace Algorithms.Strings;

public static class Permutation
{
    #region

    /// <summary>
    ///     Returns every anagram of a given word.
    /// </summary>
    /// <returns>List of anagrams.</returns>
    public static List<string> GetEveryUniquePermutation(string word)
    {
        if (word.Length < 2)
            return new List<string>
            {
                word
            };

        var result = new HashSet<string>();

        for (var i = 0; i < word.Length; i++)
        {
            var temp = GetEveryUniquePermutation(word.Remove(i, 1));

            result.UnionWith(temp.Select(subPerm => word[i] + subPerm));
        }

        return result.ToList();
    }

    #endregion
}