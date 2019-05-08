using magisterka.Models;

namespace magisterka.Interfaces
{
    public interface IFormData
    {
        GranuleSet GranuleSet { get; set; }
        string PathSource { get; set; }
    }
}
