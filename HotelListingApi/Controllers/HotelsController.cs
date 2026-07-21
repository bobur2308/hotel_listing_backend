using HotelListingApi.Data;
using HotelListingApi.DTOs.Hotel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelListingApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotelsController : ControllerBase
{
    private readonly HotelListingDbContext _context;

    public HotelsController(HotelListingDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetHotelsDto>>> GetHotels()
    {
        var hotels =  await _context.Hotels
            .Select(x => new GetHotelsDto(x.Id, x.FullName,x.Address,x.Rating,x.CountryId))
            .ToListAsync();
        
        return Ok(hotels);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetHotelDto>> GetHotel(int id)
    {
        var hotel = await _context.Hotels
            .Where(x => x.Id == id)
            .Include(h => h.Country)
            .Select(x => new GetHotelDto(x.Id,x.FullName,x.Address,x.Rating,x.Country!.FullName))
            .FirstOrDefaultAsync();

        if (hotel == null)
        {
            return NotFound();
        }
        return Ok(hotel);
    }

    [HttpPost]
    public async Task<ActionResult<Hotel>> PostHotel(CreateHotelDto hotelDto)
    {
        var hotel = new Hotel()
        {
            FullName = hotelDto.FullName,
            Address = hotelDto.Address,
            Rating = hotelDto.Rating,
            CountryId = hotelDto.CountryId
        };
        await _context.Hotels.AddAsync(hotel);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutHotel(int id, UpdateHotelDto hotelDto)
    {
        if (id != hotelDto.Id) return BadRequest();
        
        var hotel = await _context.Hotels.FirstOrDefaultAsync(x => x.Id == id);
        
        if (hotel == null) return NotFound();
        
        hotel.FullName = hotelDto.FullName;
        hotel.Address = hotelDto.Address;
        hotel.Rating = hotelDto.Rating;
        hotel.CountryId = hotelDto.CountryId;
        
        _context.Entry(hotel).State = EntityState.Modified;
        
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await HotelExists(id))
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
    public async Task<IActionResult> DeleteHotel(int id)
    {
        var hotel = await _context.Hotels.FirstOrDefaultAsync(x => x.Id == id);
        if (hotel == null) return NotFound();
        _context.Hotels.Remove(hotel);
        await _context.SaveChangesAsync();
        return NoContent();
    }
    
    private async Task<bool> HotelExists(int id)
    {
        return await _context.Hotels.AnyAsync(e => e.Id == id);
    }
}