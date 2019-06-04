using System.Collections.Generic;
using App.Enums;
using App.Models;

namespace App.Compares
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
                if (allIsLesser && x[i] > y[i])
                {
                    allIsLesser = false;
                }
                else if (allIsGreater && x[i] < y[i])
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

            if (allIsGreater)
            {
                return (int)GranuleComparerResult.IsGreater;
            }

            return (int)GranuleComparerResult.IsLesser;
        }
    }
}
