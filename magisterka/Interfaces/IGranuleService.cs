using System.Collections.Generic;
using magisterka.Models;

namespace magisterka.Interfaces
{
    public interface IGranuleService
    {
        ZbGran GenerateGran(List<List<int>> data);
        Granula SearchMin(ZbGran zbGran);
        ZbGran BuildSortedTree(ZbGran zbGran);
    }
}
