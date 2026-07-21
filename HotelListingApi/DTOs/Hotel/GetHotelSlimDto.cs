namespace HotelListingApi.DTOs.Hotel;

public record GetHotelSlimDto(
    int Id,
    string FullName,
    string Address,
    double Rating
);