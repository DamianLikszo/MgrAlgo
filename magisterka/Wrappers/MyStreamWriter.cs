using System.IO;

namespace magisterka.Wrappers
{
    public class MyStreamWriter : IMyStreamWriter
    {
        public StreamWriter GetStreamWriter(string path)
        {
            return new StreamWriter(path);
        }
    }
}
