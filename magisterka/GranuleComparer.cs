using magisterka.Enums;
using magisterka.Models;

namespace magisterka
{
    public static class GranuleComparer
    {
        public static GranuleComparerResult Compare(Granule gran1, Granule gran2)
        {
            if (gran1.Count() != gran2.Count())
            {
                return GranuleComparerResult.CanNotCompare;
            }

            var allIsLesser = true;
            var allIsGreater = true;
            for (var i = 0; i < gran1.Count(); i++)
            {
                if (gran1[i] > gran2[i])
                {
                    allIsLesser = false;
                }
                else if (gran1[i] < gran2[i])
                {
                    allIsGreater = false;
                }

                if (!allIsGreater && !allIsLesser)
                {
                    return GranuleComparerResult.CanNotCompare;
                }
            }

            if (allIsLesser && allIsGreater)
            {
                return GranuleComparerResult.Equal;
            }
            else if (allIsGreater)
            {
                return GranuleComparerResult.IsGreater;
            }

            return GranuleComparerResult.IsLesser;
        }
    }
}
