namespace App.Models
{
    public class CoverageFile
    {
        public CoverageData CoverageData { get; set; }
        public string Path { get; set; }

        public CoverageFile(string path, CoverageData coverageData)
        {
            Path = path;
            CoverageData = coverageData;
        }
    }
}
