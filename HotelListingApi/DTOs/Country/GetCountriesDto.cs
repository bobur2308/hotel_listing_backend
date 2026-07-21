namespace HotelListingApi.DTOs.Country;

public record GetCountriesDto(
    int Id,
    string FullName,
    string ShortName
);