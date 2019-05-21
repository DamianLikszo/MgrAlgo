using System.Collections.Generic;
using magisterka.Enums;
using magisterka.Models;

namespace magisterka.Compares
{
    public class GranuleComparer : IComparer<Granule>
    {
        public int Compare(Granule x, Granule y)
        {
            if (x == null || y == null || x.Count() != y.Count())
            {
                return (int)GranuleComparerResult.CanNotCompare;
            }

            var allIsLesser = true;
            var allIsGreater = true;
            for (var i = 0; i < x.Count(); i++)
            {
                if (x[i] > y[i])
                {
                    allIsLesser = false;
                }
                else if (x[i] < y[i])
                {
                    allIsGreater = false;
                }

                if (!allIsGreater && !allIsLesser)
                {
                    return (int)GranuleComparerResult.CanNotCompare;
                }
            }

            if (allIsLesser && allIsGreater)
            {
                return (int)GranuleComparerResult.Equal;
            }
            else if (allIsGreater)
            {
                return (int)GranuleComparerResult.IsGreater;
            }

            return (int)GranuleComparerResult.IsLesser;
        }
    }
}
