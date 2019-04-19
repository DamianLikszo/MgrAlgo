using System.Collections.Generic;

namespace magisterka.Models
{
    public class CoverageFile
    {
        public List<List<int>> Data { get; set; }
        public string Path { get; set; }

        public CoverageFile(string path, List<List<int>> data)
        {
            Path = path;
            Data = data;
        }
    }
}
