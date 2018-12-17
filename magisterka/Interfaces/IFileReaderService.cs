using magisterka.Models;
using System.Collections.Generic;

namespace magisterka.Interfaces
{
    public interface IFileReaderService
    {
        CoverageFile OpenAndReadFile();
        bool SaveFile(List<Granula> data);
    }
}
