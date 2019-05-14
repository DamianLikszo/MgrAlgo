using System.Collections.Generic;

namespace magisterka.Interfaces
{
    public interface IFileService
    {
        string GetPathFromOpenFileDialog(string filter);
        List<string> ReadFile(string path, out string error);
        bool SaveFile(string path, List<string> content, out string error);
        string GetPathFromSaveFileDialog(string filter);
    }
}
