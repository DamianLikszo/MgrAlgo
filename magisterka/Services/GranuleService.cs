﻿using System.Collections.Generic;
using System.Linq;
using magisterka.Models;
using magisterka.Interfaces;

namespace magisterka.Services
{
    public class GranuleService : IGranuleService
    {
        private readonly IGranuleComparerForBuildTree _comparerForBuildTree;

        public GranuleService(IGranuleComparerForBuildTree comparerForBuildTree)
        {
            _comparerForBuildTree = comparerForBuildTree;
        }

        //TODO: move to GranSetService
        public GranuleSet GenerateGran(CoverageData coverageData)
        {
            var granuleSet = new GranuleSet();
            granuleSet.Granules = GenerateGranules(coverageData);
            granuleSet.Sort(_comparerForBuildTree);

            return granuleSet;
        }

        public List<Granule> GenerateGranules(CoverageData coverageData)
        {
            var granules = new List<Granule>();

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

                granules.Add(granule);
            }

            return granules;
        }
    }
}
