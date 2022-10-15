namespace Algorithms.Sorters.Integer;

/// <summary>
///     Sorts array of integers without comparing them.
/// </summary>
public interface IIntegerSorter
{
    #region

    /// <summary>
    ///     Sorts array in ascending order.
    /// </summary>
    /// <param name="array">Array to sort.</param>
    void Sort(int[] array);

    #endregion
}