using App.Models;

namespace App.Interfaces
{
    public interface IGranuleSetDtoConverter
    {
        GranuleSet ConvertFromDto(GranuleDto[] granulesDto);
        GranuleDto[] ConvertToDto(GranuleSet granuleSet);
    }
}
