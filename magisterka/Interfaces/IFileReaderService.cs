using System.Collections.Generic;
using magisterka.Models;

namespace magisterka.Interfaces
{
    public interface IFileReaderService
    {
        CoverageFile OpenAndReadFile(out string error);
        List<string> PreparePrint(GranuleSet granuleSet);
        bool SaveFile(GranuleSet granuleSet, out string error);
    }
}
