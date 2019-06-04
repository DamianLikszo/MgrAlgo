using System.Collections.Generic;
using App.Models;

namespace App.Interfaces
{
    public interface IGranuleService
    {
        GranuleSet GenerateGran(CoverageData coverageData);
        List<Granule> GenerateGranules(CoverageData coverageData);
        GranuleSet BuildGranuleSet(List<Granule> granules);
    }
}
