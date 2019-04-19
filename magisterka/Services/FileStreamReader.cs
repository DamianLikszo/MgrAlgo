using magisterka.Interfaces;
using System.IO;

namespace magisterka.Services
{
    public class FileStreamReader : IStreamReader
    {
        public StreamReader GetReader(string path)
        {
            return new StreamReader(path);
        }
    }
}
