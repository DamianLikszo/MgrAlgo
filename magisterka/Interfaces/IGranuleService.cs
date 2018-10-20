using System.Collections.Generic;
using magisterka.Models;

namespace magisterka.Interfaces
{
    public interface IGranuleService
    {
        bool IsGreaterOrEqual(Granula gran1, Granula gran2);
        bool IsLesser(Granula gran1, Granula gran2);
        ZbGran GenerateGran(List<List<int>> data);
    }
}
