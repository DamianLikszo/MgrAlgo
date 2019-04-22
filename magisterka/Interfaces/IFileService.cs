using System.Collections.Generic;

namespace magisterka.Interfaces
{
    public interface IFileService
    {
        string SelectFile();
        List<string> ReadFile(string path);
    }
}
