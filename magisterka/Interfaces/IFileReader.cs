using magisterka.Models;

namespace magisterka.Interfaces
{
    public interface IFileReader
    {
        CoverageFile OpenAndReadFile();
    }
}
