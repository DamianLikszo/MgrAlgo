using System.Collections.Generic;
using System.Linq;
using magisterka.Models;
using magisterka.Interfaces;

namespace magisterka.Services
{
    public class GranuleService : IGranuleService
    {
        //TODO: move to GranSetService
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
    }
}
