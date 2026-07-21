using HotelListingApi.Data;
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
    public async Task<ActionResult<IEnumerable<Hotel>>> GetHotels()
    {
        return await _context.Hotels.Include(h => h.Country).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Hotel>> GetHotel(int id)
    {
        var hotel = await _context.Hotels.Include(h => h.Country).FirstOrDefaultAsync(x => x.Id == id);

        if (hotel == null)
        {
            return NotFound();
        }
        return hotel;
    }

    [HttpPost]
    public async Task<ActionResult<Hotel>> PostHotel(Hotel hotel)
    {
        await _context.Hotels.AddAsync(hotel);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutHotel(int id, Hotel hotel)
    {
        if (id != hotel.Id) return BadRequest();
        
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