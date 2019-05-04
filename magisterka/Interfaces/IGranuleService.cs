using magisterka.Models;

namespace magisterka.Interfaces
{
    public interface IGranuleService
    {
        GranuleSet GenerateGran(CoverageData coverageData);
    }
}
