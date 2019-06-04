namespace App.Wrappers
{
    public interface IMyJsonConvert
    {
        string SerializeObject(object value);
        T DeserializeObject<T>(string value);
    }
}
