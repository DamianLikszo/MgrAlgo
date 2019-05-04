using magisterka.Models;

namespace magisterka.Interfaces
{
    public interface IGranuleService
    {
        bool IsGreaterOrEqual(Granule gran1, Granule gran2);
        bool IsLesser(Granule gran1, Granule gran2);
        GranuleSet GenerateGran(CoverageData coverageData);
    }
}
