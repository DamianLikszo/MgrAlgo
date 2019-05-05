using System.Linq;
using magisterka.Interfaces;
using magisterka.Models;

namespace magisterka
{
    public class NumberOfOnesForGranuleComparer : IGranuleComparerForBuildTree
    {
        public int Compare(Granule x, Granule y)
        {
            if (x == null || y == null)
                return int.MinValue;

            return x.Count(p => p == 1).CompareTo(y.Count(p => p == 1));
        }
    }
}
