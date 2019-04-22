using System;
using System.Collections.Generic;
using magisterka.Models;
using magisterka.Interfaces;

namespace magisterka.Services
{
    public class GranuleService : IGranuleService
    {
        //TODO: rename t i t1
        public ZbGran GenerateGran(CoverageData coverageData)
        {
            var zbGran = new ZbGran();
            
            foreach (var t1 in coverageData)
            {
                var granule = new Granula();
                var indexSelect = new List<int>();

                for (var j = 0; j < t1.Count; j++)
                {
                    if (t1[j] == 1)
                        indexSelect.Add(j);
                }
                
                foreach (var t in coverageData)
                {
                    var result = 1;
                    foreach (var index in indexSelect)
                    {
                        if (result == 0)
                            break;

                        result = Math.Min(result, t[index]);
                    }
                    granule.AddToInside(result);
                }
                zbGran.Add(granule);
            }

            return zbGran;
        }

        public bool IsGreaterOrEqual(Granula gran1, Granula gran2)
        {
            if (gran1.Inside.Count != gran2.Inside.Count)
                return false;

            for (int i = 0; i < gran1.Inside.Count; i++)
            {
                if (gran1.Inside[i] < gran2.Inside[i])
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsLesser(Granula gran1, Granula gran2)
        {
            if (gran1.Inside.Count != gran2.Inside.Count)
                return false;

            for (int i = 0; i < gran1.Inside.Count; i++)
            {
                if (gran2.Inside[i] > gran1.Inside[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
