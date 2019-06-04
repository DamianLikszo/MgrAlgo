using System.Collections.Generic;
using App.Models;

namespace App.Interfaces
{
    public interface IPrintGranuleService
    {
        List<string> Print(GranuleSet granuleSet);
    }
}
