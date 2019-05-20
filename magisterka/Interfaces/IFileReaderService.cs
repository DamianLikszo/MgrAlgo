using System.Collections.Generic;
using magisterka.Models;

namespace magisterka.Interfaces
{
    public interface IFileReaderService
    {
        List<string> PreparePrint(GranuleSet granuleSet);
        bool SaveFile(GranuleSet granuleSet, out string error);
    }
}
