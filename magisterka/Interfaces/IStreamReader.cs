using System.IO;

namespace magisterka.Interfaces
{
    public interface IStreamReader
    {
        StreamReader GetReader(string path);
    }
}
