using HotelListingApi.Data;
using HotelListingApi.DTOs.Country;
using HotelListingApi.DTOs.Hotel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelListingApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CountriesController:ControllerBase
{   
    private readonly HotelListingDbContext _context;

    public CountriesController(HotelListingDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetCountriesDto>>> GetCountries()
    {
        var countries = await _context.Countries
            .Select( x => new GetCountriesDto(
                x.Id,
                x.FullName,
                x.ShortName
                )
            )
            .ToListAsync();
        
        return Ok(countries);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetCountryDto>> GetCountry(int id)
    {
        var country = await _context.Countries
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

        if (country == null)
        {
            return NotFound();
        }
        return Ok(country);
    }

    [HttpPost]
    public async Task<ActionResult<Country>> PostCountry(CreateCountryDto countryDto)
    {
        var country = new Country()
        {
            FullName = countryDto.FullName,
            ShortName = countryDto.ShortName,
        };
        await _context.Countries.AddAsync(country);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetCountry", new { id = country.Id }, country);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCountry(int id, UpdateCountryDto countryDto)
    {
        if(id != countryDto.Id) return BadRequest();
        
        var country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);
        
        if(country == null) return NotFound();
        country.FullName = countryDto.FullName;
        country.ShortName = countryDto.ShortName;
        
        _context.Entry(country).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await CountryExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return NoContent(); 
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCountry(int id)
    {
        var country = await _context.Countries.FindAsync(id);
        if (country == null)
        {
            return NotFound();
        }
        _context.Countries.Remove(country);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private async Task<bool> CountryExists(int id)
    {
        return await _context.Countries.AnyAsync(e => e.Id == id);
    }
}