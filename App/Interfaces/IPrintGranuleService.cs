using System.Collections.Generic;
using magisterka.Models;

namespace magisterka.Interfaces
{
    public interface IPrintGranuleService
    {
        List<string> Print(GranuleSet granuleSet);
    }
}
