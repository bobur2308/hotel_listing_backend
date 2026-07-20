using HotelListingApi.Data;
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
    public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
    {
        return await _context.Countries.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Country>> GetCountry(int id)
    {
        var country = await _context.Countries.FirstOrDefaultAsync(x => x.CountryId == id);

        if (country == null)
        {
            return NotFound();
        }
        return country;
    }

    [HttpPost]
    public async Task<ActionResult<Country>> PostCountry(Country country)
    {
        _context.Countries.Add(country);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetCountry", new { id = country.CountryId }, country);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCountry(int id, Country country)
    {
        if(id != country.CountryId) return BadRequest();
        
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
        return _context.Countries.Any(e => e.CountryId == id);
    }
}