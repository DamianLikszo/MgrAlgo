using magisterka.Models;

namespace magisterka.Interfaces
{
    public interface IGranuleSetDtoConverter
    {
        GranuleSet ConvertFromDto(GranuleDto[] granulesDto);
        GranuleDto[] ConvertToDto(GranuleSet granuleSet);
    }
}
