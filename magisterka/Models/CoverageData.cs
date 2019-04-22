using System.Collections.Generic;

namespace magisterka.Models
{
    public class CoverageData
    {
        public List<List<int>> Data { get; set; }

        public CoverageData(List<List<int>> data)
        {
            Data = data;
        }
    }
}
