using System.IO;

namespace magisterka.Wrappers
{
    public class MyStreamReader : IMyStreamReader
    {
        public StreamReader GetReader(string path)
        {
            return new StreamReader(path);
        }
    }
}
