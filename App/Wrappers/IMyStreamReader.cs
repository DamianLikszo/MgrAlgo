using System.IO;

namespace App.Wrappers
{
    public interface IMyStreamReader
    {
        StreamReader GetReader(string path);
    }
}
