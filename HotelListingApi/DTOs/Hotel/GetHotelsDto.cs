namespace HotelListingApi.DTOs.Hotel;

public record GetHotelsDto(
    int Id,
    string FullName,
    string Address,
    double Rating,
    int CountryId
);