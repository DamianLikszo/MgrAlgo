using System.Collections.Generic;
using magisterka.Models;

namespace magisterka.Interfaces
{
    public interface ICoverageDataConverter
    {
        CoverageData Convert(List<string> content);
    }
}
