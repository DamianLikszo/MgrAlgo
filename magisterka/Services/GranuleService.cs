using System;
using System.Collections.Generic;
using magisterka.Models;
using magisterka.Interfaces;

namespace magisterka.Services
{
    public class GranuleService : IGranuleService
    {
        public ZbGran GenerateGran(List<List<int>> data)
        {
            var zbGran = new ZbGran();
            
            for (int u = 0; u < data.Count; u++)
            {
                int result;
                var granule = new Granula();
                var indexSelect = new List<int>();

                for (int j = 0; j < data[u].Count; j++)
                {
                    if (data[u][j] == 1)
                        indexSelect.Add(j);
                }
                
                for (int i = 0; i < data.Count; i++)
                {
                    result = 1;
                    foreach (var index in indexSelect)
                    {
                        if (result == 0)
                            break;

                        result = Math.Min(result, data[i][index]);
                    }
                    granule.AddToInside(result);
                }
                zbGran.Add(granule);
            }

            return zbGran;
        }
    }
}
