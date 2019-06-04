using System.IO;

namespace magisterka.Wrappers
{
    public interface IMyStreamReader
    {
        StreamReader GetReader(string path);
    }
}
