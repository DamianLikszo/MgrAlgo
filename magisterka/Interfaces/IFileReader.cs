using magisterka.Models;
using System.Collections.Generic;

namespace magisterka.Interfaces
{
    public interface IFileReader
    {
        CoverageFile OpenAndReadFile();
        bool SaveFile(List<Granula> data);
    }
}
