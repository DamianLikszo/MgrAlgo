using System.Collections.Generic;
using System.Linq;
using magisterka.Models;
using magisterka.Interfaces;

namespace magisterka.Services
{
    public class GranuleService : IGranuleService
    {
        public GranuleSet GenerateGran(CoverageData coverageData)
        {
            var granuleSet = new GranuleSet();
            
            foreach (var row in coverageData)
            {
                var granule = new Granule();
                var indexes = new List<int>();
                
                for (var j = 0; j < row.Count; j++)
                {
                    if (row[j] == 1)
                        indexes.Add(j);
                }
                
                foreach (var checkRow in coverageData)
                {
                    var result = indexes.All(x => checkRow[x] == 1) ? 1 : 0;
                    granule.AddToInside(result);
                }
                granuleSet.Add(granule);
            }

            return granuleSet;
        }

        public bool IsGreaterOrEqual(Granule gran1, Granule gran2)
        {
            if (gran1.Count() != gran2.Count())
                return false;

            for (var i = 0; i < gran1.Count(); i++)
            {
                if (gran1[i] < gran2[i])
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsLesser(Granule gran1, Granule gran2)
        {
            if (gran1.Count() != gran2.Count())
                return false;

            for (var i = 0; i < gran1.Count(); i++)
            {
                if (gran2[i] > gran1[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
