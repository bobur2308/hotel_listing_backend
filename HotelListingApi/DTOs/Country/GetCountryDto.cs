using HotelListingApi.DTOs.Hotel;

namespace HotelListingApi.DTOs.Country;

public record GetCountryDto(
    int Id,
    string FullName,
    string ShortName,
    List<GetHotelSlimDto> Hotels
);