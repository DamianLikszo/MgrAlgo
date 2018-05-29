using System.Collections.Generic;

namespace magisterka.Models
{
    public class CoverageFile
    {
        public List<List<int>> Data { get; set; }
        public string Path { get; set; }

        public CoverageFile()
        {
            Data = new List<List<int>>();
        }

        public void Insert(List<int> row) => Data.Add(row);
    }
}
