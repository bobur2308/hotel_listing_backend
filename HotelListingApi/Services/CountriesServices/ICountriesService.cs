using HotelListingApi.DTOs.Country;

namespace HotelListingApi.Services;

public interface ICountriesService
{
    Task<IEnumerable<GetCountriesDto>> GetCountries();
    Task<GetCountryDto?> GetCountryAsync(int id);
    Task<GetCountryDto> CreateCountryAsync(CreateCountryDto createDto);
    Task UpdateCountryAsync(int id, UpdateCountryDto updateDto);
    Task DeleteCountryAsync(int id);
}