﻿namespace Algorithms.Sorters.Comparison;

/// <summary>
///     Cocktail Sort is a variation of Bubble sort, where Cocktail
///     Sort traverses through a given array in both directions alternatively.
/// </summary>
/// <typeparam name="T">Array input type.</typeparam>
public class CocktailSorter<T> : IComparisonSorter<T>
{
    #region Interface Implementations

    /// <summary>
    ///     Sorts array using Cocktail sort algorithm.
    /// </summary>
    /// <param name="array">Input array.</param>
    /// <param name="comparer">Type of comparer for array elements.</param>
    public void Sort(T[] array, IComparer<T> comparer)
    {
        CocktailSort(array, comparer);
    }

    #endregion

    #region

    private static void CocktailSort(IList<T> array, IComparer<T> comparer)
    {
        var swapped = true;

        var startIndex = 0;
        var endIndex = array.Count - 1;

        while (swapped)
        {
            for (var i = startIndex; i < endIndex; i++)
            {
                if (comparer.Compare(array[i], array[i + 1]) != 1) continue;

                (array[i], array[i + 1]) = (array[i + 1], array[i]);
            }

            endIndex--;
            swapped = false;

            for (var i = endIndex; i > startIndex; i--)
            {
                if (comparer.Compare(array[i], array[i - 1]) != -1) continue;

                (array[i], array[i - 1]) = (array[i - 1], array[i]);

                swapped = true;
            }

            startIndex++;
        }
    }

    #endregion
}