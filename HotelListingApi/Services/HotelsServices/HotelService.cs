using HotelListingApi.Data;
using HotelListingApi.DTOs.Hotel;
using Microsoft.EntityFrameworkCore;

namespace HotelListingApi.Services.HotelsServices;

public class HotelService(HotelListingDbContext context) : IHotelService
{
    public async Task<IEnumerable<GetHotelsDto>> GetHotels()
    {
        var hotels =  await context.Hotels
            .Select(x => new GetHotelsDto(
                x.Id, 
                x.FullName,
                x.Address,
                x.Rating,
                x.CountryId
            ))
            .ToListAsync();
        
        return hotels;
    }

    public async Task<GetHotelDto?> GetHotelAsync(int id)
    {
        var hotel =  await context.Hotels
            .Where(x => x.Id == id)
            .Include(h => h.Country)
            .Select(x => new GetHotelDto(
                x.Id,
                x.FullName,
                x.Address,
                x.Rating,
                x.Country!.FullName))
            .FirstOrDefaultAsync(); 
        
        return hotel;
    }

    public async Task<GetHotelDto?> CreateHotelAsync(CreateHotelDto hotelDto)
    {
        var country = await context.Countries.FindAsync(hotelDto.CountryId);
        if (country == null) return null;
        
        var hotel = new Hotel()
        {
            FullName = hotelDto.FullName,
            Address = hotelDto.Address,
            Rating = hotelDto.Rating,
            CountryId = hotelDto.CountryId
        };
        await context.Hotels.AddAsync(hotel);
        await context.SaveChangesAsync();
        return new GetHotelDto(hotel.Id,hotel.FullName,hotel.Address,hotel.Rating,country.FullName);
    }

    public async Task UpdateHotelAsync(int id, UpdateHotelDto hotelDto)
    {
        var hotel = await context.Hotels.FindAsync(id) ??  throw new KeyNotFoundException("Hotel not found");
        hotel.FullName = hotelDto.FullName;
        hotel.Address = hotelDto.Address;
        hotel.Rating = hotelDto.Rating;
        hotel.CountryId = hotelDto.CountryId;
        await context.SaveChangesAsync();
    }

    public async Task DeleteHotelAsync(int id)
    {
        var hotel = await context.Hotels.FindAsync(id) ??   throw new KeyNotFoundException("Hotel not found");
        context.Hotels.Remove(hotel);
        await context.SaveChangesAsync();
    }
}