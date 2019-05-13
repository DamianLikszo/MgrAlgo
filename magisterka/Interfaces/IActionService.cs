namespace magisterka.Interfaces
{
    public interface IActionService
    {
        bool Load();
        bool SerializeGranuleSetAndSaveFile();
        bool OpenFileAndDeserializeGranuleSet();
    }
}
