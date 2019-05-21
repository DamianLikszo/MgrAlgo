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

            foreach (var row in coverageData)
            {
                var insideList = new List<int>();
                var indexes = new List<int>();

                for (var j = 0; j < row.Count; j++)
                {
                    if (row[j] == 1)
                        indexes.Add(j);
                }

                foreach (var checkRow in coverageData)
                {
                    var result = indexes.All(x => checkRow[x] == 1) ? 1 : 0;
                    insideList.Add(result);
                }

                var granule = new Granule(insideList);
                granules.Add(new Granule(granule));
            }

            return granules;
        }
        
        public GranuleSet BuildGranuleSet(List<Granule> granules)
        {
            var sortedGranules = granules.OrderBy(x => x.Count(y => y == 1)).ToList();
            var result = new GranuleSet();

            foreach (var addGranule in sortedGranules)
            {
                if (result.Count > 0)
                {
                    foreach (var granule in result.GetMax())
                    {
                        SetRelations(addGranule, granule);
                    }
                }

                var resultReverse = result.Reverse();
                foreach (var granule in resultReverse)
                {
                    SetRelations(addGranule, granule, true);
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
                    return true;
            }

            return false;
        }

        private void SetRelations(Granule granule1, Granule granule2, bool checkParent = false)
        {
            var compare = (GranuleComparerResult) granule1.CompareTo(granule2);
            if (compare == GranuleComparerResult.IsGreater && (!checkParent || !CheckContainInParent(granule2, granule1)))
            {
                granule1.Child.Add(granule2);
                granule2.Parent.Add(granule1);
            }
        }
    }
}
