using System.IO;

namespace App.Wrappers
{
    public class MyStreamReader : IMyStreamReader
    {
        public StreamReader GetReader(string path)
        {
            return new StreamReader(path);
        }
    }
}
