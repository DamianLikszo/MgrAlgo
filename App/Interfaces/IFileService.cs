using System.Collections.Generic;

namespace App.Interfaces
{
    public interface IFileService
    {
        string GetPathFromOpenFileDialog(string filter = null);
        List<string> ReadFile(string path, out string error);
        bool SaveFile(string path, List<string> content, out string error);
        string GetPathFromSaveFileDialog(string filter = null);
    }
}
