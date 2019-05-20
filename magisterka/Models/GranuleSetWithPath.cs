namespace magisterka.Models
{
    public class GranuleSetWithPath
    {
        public GranuleSetWithPath(GranuleSet granuleSet, string path)
        {
            GranuleSet = granuleSet;
            Path = path;
        }

        public GranuleSet GranuleSet { get;}
        public string Path { get; }
    }
}
