using System.IO;

namespace magisterka.Wrappers
{
    public interface IMyStreamWriter
    {
        StreamWriter GetStreamWriter(string path);
    }
}
