using System.Collections.Generic;
using App.Models;

namespace App.Interfaces
{
    public interface ICoverageDataConverter
    {
        CoverageData Convert(List<string> content, out string error);
    }
}
