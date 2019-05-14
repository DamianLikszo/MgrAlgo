namespace magisterka.Interfaces
{
    public interface IActionService
    {
        bool Load(out string error);
        bool SerializeGranuleSetAndSaveFile(out string error);
        bool OpenFileAndDeserializeGranuleSet(out string error);
    }
}
