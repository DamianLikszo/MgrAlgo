using System.IO;

namespace App.Wrappers
{
    public interface IMyStreamWriter
    {
        StreamWriter GetStreamWriter(string path);
    }
}
