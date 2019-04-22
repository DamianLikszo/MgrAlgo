using System.Collections.Generic;
using magisterka.Interfaces;
using magisterka.Models;
using magisterka.Wrappers;

namespace magisterka.Services
{
    public class CoverageDataConverter :  ICoverageDataConverter
    {
        private readonly IMyMessageBox _myMessageBox;
        private readonly char _separator = ';';

        public CoverageDataConverter(IMyMessageBox messageBox)
        {
            _myMessageBox = messageBox;
        }

        public CoverageData Convert(List<string> content)
        {
            var data = new List<List<int>>();

            foreach (var line in content)
            {
                var columns = line.Split(_separator);
                var row = new List<int>();

                foreach (var item in columns)
                {
                    if (!int.TryParse(item, out var column))
                    {
                        _myMessageBox.Show("Nieprawidłowy zestaw danych. Wiersze zawierają inne dane niż liczby.");
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
