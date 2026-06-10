using HotelListingApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace HotelListingApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotelsController : ControllerBase
{
    private static List<Hotel> hotels = new List<Hotel>()
    {
        new  Hotel{ Id = 1, Name = "Hotel 1",Address = "Hotel 1" ,Rating = 5.5 },
        new  Hotel{ Id = 2, Name = "Hotel 2",Address = "Hotel 2" ,Rating = 4.2 },
    };
    
    [HttpGet]
    public ActionResult<IEnumerable<Hotel>> Get()
    {
        return Ok(hotels);
    }

    [HttpGet("{id}")]
    public ActionResult<Hotel> Get(int id)
    {
        var hotel = hotels.FirstOrDefault(h => h.Id == id);
        if (hotel == null) 
            return NotFound();
        return hotel;
    }

    [HttpPost]
    public ActionResult<Hotel> Post([FromBody] Hotel data)
    {
        if (hotels.Any(h => h.Id == data.Id))
            return BadRequest("Hotel with this Id already exists.");
        
        hotels.Add(data);
        return CreatedAtAction(nameof(Get), new { id = data.Id }, data);
    }

    [HttpPut("{id}")]
    public ActionResult Put(int id, [FromBody] Hotel data)
    {
        var existingHotel = hotels.FirstOrDefault(h => h.Id == id);
        if (existingHotel == null)
            return NotFound(); 
        existingHotel.Rating = data.Rating;
        existingHotel.Address = data.Address;
        existingHotel.Name = data.Name;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var hotel = hotels.FirstOrDefault(h => h.Id == id);
        if (hotels == null)
            return NotFound();
        hotels.Remove(hotel);
        return NoContent();
    }
}