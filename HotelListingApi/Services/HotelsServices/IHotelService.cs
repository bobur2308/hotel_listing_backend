using HotelListingApi.DTOs.Hotel;

namespace HotelListingApi.Services.HotelsServices;

public interface IHotelService
{
    Task<IEnumerable<GetHotelsDto>> GetHotels();
    Task<GetHotelDto?> GetHotelAsync(int id);
    Task<GetHotelDto?> CreateHotelAsync(CreateHotelDto hotelDto);
    Task UpdateHotelAsync(int id, UpdateHotelDto hotelDto);
    Task DeleteHotelAsync(int id);
}