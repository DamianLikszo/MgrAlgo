using System.Collections.Generic;

namespace magisterka.Interfaces
{
    public interface IFileService
    {
        string GetPathFromOpenFileDialog();
        List<string> ReadFile(string path);
        bool SaveFile(string path, List<string> content);
        string GetPathFromSaveFileDialog();
    }
}
