using System.Collections.Generic;
using magisterka.Interfaces;
using magisterka.Models;

namespace magisterka.Services
{
    public class CoverageDataConverter :  ICoverageDataConverter
    {
        private readonly char _separator = ';';

        public CoverageData Convert(List<string> content, out string error)
        {
            error = null;
            var data = new List<List<int>>();

            foreach (var line in content)
            {
                var columns = line.Split(_separator);
                var row = new List<int>();

                foreach (var item in columns)
                {
                    if (!int.TryParse(item, out var column))
                    {
                        error = "Nieprawidłowy zestaw danych. Wiersze zawierają inne dane niż liczby.";
                        return null;
                    }

                    row.Add(column);
                }

                data.Add(row);
            }

            return new CoverageData(data);
        }
    }
}
