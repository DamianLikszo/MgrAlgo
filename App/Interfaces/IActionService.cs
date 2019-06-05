using App.Models;

namespace App.Interfaces
{
    public interface IActionService
    {
        GranuleSetWithPath Load(out string error);
        bool SaveGranule(GranuleSet granuleSet, out string error);
    }
}
