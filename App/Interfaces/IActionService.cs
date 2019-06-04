using magisterka.Models;

namespace magisterka.Interfaces
{
    public interface IActionService
    {
        GranuleSetWithPath Load(out string error);
        bool SaveGranule(GranuleSet granuleSet, out string error);
        bool SerializeGranuleSetAndSaveFile(GranuleSet granuleSet, out string error);
        GranuleSetWithPath OpenFileAndDeserializeGranuleSet(out string error);
    }
}
