using System.Collections.Generic;
using magisterka.Models;

namespace magisterka.Interfaces
{
    public interface IGranuleService
    {
        GranuleSet GenerateGran(CoverageData coverageData);
        List<Granule> GenerateGranules(CoverageData coverageData);
        GranuleSet BuildGranuleSet(List<Granule> granules);
    }
}
