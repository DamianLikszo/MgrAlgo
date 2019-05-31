using System.Collections.Generic;
using System.Linq;
using magisterka.Enums;
using magisterka.Models;
using magisterka.Interfaces;

namespace magisterka.Services
{
    public class GranuleService : IGranuleService
    {
        public GranuleSet GenerateGran(CoverageData coverageData)
        {
            var granules = GenerateGranules(coverageData);
            var granuleSet = BuildGranuleSet(granules);

            return granuleSet;
        }

        public List<Granule> GenerateGranules(CoverageData coverageData)
        {
            var granules = new List<Granule>();

            for (var i = 0; i < coverageData.Count; i++)
            {
                var insideList = new List<int>();
                var indexes = new List<int>();

                for (var j = 0; j < coverageData[i].Count; j++)
                {
                    if (coverageData[i][j] == 1)
                    {
                        indexes.Add(j);
                    }
                }

                foreach (var checkRow in coverageData)
                {
                    var result = indexes.All(x => checkRow[x] == 1) ? 1 : 0;
                    insideList.Add(result);
                }

                var granule = new Granule(insideList, i+1);
                granules.Add(granule);
            }

            return granules;
        }
        
        public GranuleSet BuildGranuleSet(List<Granule> granules)
        {
            var sortedGranules = granules.OrderBy(x => x.Count(y => y == 1)).ToList();
            var result = new GranuleSet();

            foreach (var addGranule in sortedGranules)
            {
                var resultReverse = result.Reverse().ToList();
                foreach (var granule in resultReverse)
                {
                    var compare = (GranuleComparerResult)addGranule.CompareTo(granule);
                    if (compare != GranuleComparerResult.IsGreater || CheckContainInParent(granule, addGranule))
                    {
                        continue;
                    }

                    addGranule.Child.Add(granule);
                    granule.Parent.Add(addGranule);
                }

                result.Add(addGranule);
            }

            return result;
        }

        private bool CheckContainInParent(Granule granule, Granule searchGranule)
        {
            if (granule == searchGranule)
            {
                return true;
            }

            foreach (var parent in granule.Parent)
            {
                if (CheckContainInParent(parent, searchGranule))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
