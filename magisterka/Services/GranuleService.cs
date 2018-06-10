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
                    foreach (var index in indexSelect[u])
                    {
                        if (result == 0)
                            break;

                        var test = data[i][index];
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
