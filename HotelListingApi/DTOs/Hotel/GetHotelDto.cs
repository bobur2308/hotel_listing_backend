namespace HotelListingApi.DTOs.Hotel;

public record GetHotelDto(
    int Id,
    string FullName,
    string Address,
    double Rating,
    string Country
);