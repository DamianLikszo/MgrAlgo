using System;
using System.Collections.Generic;
using magisterka.Models;

namespace Test.Helpers
{
    public class SortGranulesInSameOrderComparer : IComparer<Granule>
    {
        public int Compare(Granule x, Granule y)
        {
            var xValue = CalculateValue(x);
            var yValue = CalculateValue(y);

            if (xValue > yValue)
            {
                return 1;
            }

            if( xValue < yValue)
            {
                return -1;
            }

            return 0;
        }

        private static int CalculateValue(Granule granule)
        {
            var value = 0;
            for (var i = granule.Count() - 1; i >= 0; i--)
            {
                value += granule[i] * (int)Math.Pow(10, i);
            }

            return value;
        }
    }
}
