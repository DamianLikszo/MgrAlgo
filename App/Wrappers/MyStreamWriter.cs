using System.IO;

namespace App.Wrappers
{
    public class MyStreamWriter : IMyStreamWriter
    {
        public StreamWriter GetStreamWriter(string path)
        {
            return new StreamWriter(path);
        }
    }
}
