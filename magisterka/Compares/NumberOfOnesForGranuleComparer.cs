using System.Linq;
using magisterka.Models;

namespace magisterka.Compares
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
