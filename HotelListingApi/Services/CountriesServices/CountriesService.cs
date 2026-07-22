using HotelListingApi.Data;
using HotelListingApi.DTOs.Country;
using HotelListingApi.DTOs.Hotel;
using Microsoft.EntityFrameworkCore;

namespace HotelListingApi.Services;

public class CountriesService(HotelListingDbContext context) : ICountriesService
{
    public async Task<IEnumerable<GetCountriesDto>> GetCountries()
    {
        var countries = await context.Countries
            .Select( x => new GetCountriesDto(
                    x.Id,
                    x.FullName,
                    x.ShortName
                )
            )
            .ToListAsync();
        
        return countries;
    }

    public async Task<GetCountryDto?> GetCountryAsync(int id)
    {
        var country = await context.Countries
            .Where(x => x.Id == id)
            .Select(x => new GetCountryDto(
                x.Id,
                x.FullName,
                x.ShortName,
                x.Hotels.Select(h => new GetHotelSlimDto(
                    h.Id,
                    h.FullName,
                    h.Address,
                    h.Rating
                )).ToList()
            ))
            .FirstOrDefaultAsync();
        
        return country ?? null;
    }

    public async Task<GetCountryDto> CreateCountryAsync(CreateCountryDto createDto)
    {
        var country = new Country()
        {
            FullName = createDto.FullName,
            ShortName = createDto.ShortName,
        };

        context.Countries.Add(country);
        await context.SaveChangesAsync();
        return new GetCountryDto(country.Id, country.FullName, country.ShortName,[]);
    }

    public async Task UpdateCountryAsync(int id, UpdateCountryDto updateDto)
    {
        var country = await context.Countries.FindAsync(id) ?? throw new KeyNotFoundException("Country not found");
        country.FullName = updateDto.FullName;
        country.ShortName = updateDto.ShortName;
        context.Countries.Update(country);
        await context.SaveChangesAsync();
    }
    
    public async Task DeleteCountryAsync(int id)
    {
        var country = await context.Countries.FindAsync(id) ?? throw new KeyNotFoundException("Country not found");
        context.Countries.Remove(country);
        await context.SaveChangesAsync();
    }
}