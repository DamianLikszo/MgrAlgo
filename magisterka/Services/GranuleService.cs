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

            // sprawdzenie po kórych indeksach sprawdzamy
            List<List<int>> indexSelect = new List<List<int>>();
            foreach (var row in data)
            {
                var rowIndexSelect = new List<int>();
                for (int i = 0; i < row.Count; i++)
                {
                    if (row[i] == 1)
                        rowIndexSelect.Add(i);
                }
                indexSelect.Add(rowIndexSelect);
            }

            // porównywanie min
            // ? czy indexSelect moze byc 0 ?
            for (int u = 0; u < data.Count; u++)
            {
                int result;
                var granule = new Granula();
                for (int i = 0; i < data.Count; i++)
                {
                    result = 1;
                    foreach (var index in indexSelect[i])
                    {
                        result = Math.Min(result, data[u][index]);
                    }
                    granule.AddToInside(result);
                }
                zbGran.Add(granule);
            }

            return zbGran;
        }
        
        public Granula SearchMin(ZbGran zbGran)
        {
            Granula result = zbGran.Granules[0];

            for (int i = 0; i < zbGran.Granules.Count; i++)
            {
                var gran = zbGran.Granules[i];

                if (gran.IsLesser(result))
                {
                    result = gran;
                    i = 0;
                }
            }

            return result;
        }
        
        public ZbGran BuildSortedTree(ZbGran zbGran)
        {
            var result = new ZbGran();
            
            while (zbGran.Granules.Count > 0)
            {
                var gran = SearchMin(zbGran);
                zbGran.Remove(gran);

                if(result.Granules.Count != 0)
                {
                    foreach (var granMax in result.GetMax())
                    {
                        if(gran.IsGreaterOrEqual(granMax))
                        {
                            granMax.Parent.Add(gran);
                            gran.Child.Add(granMax);
                        }
                    }
                }
                
                result.Add(gran);
            }

            return result;
        }
    }
}
