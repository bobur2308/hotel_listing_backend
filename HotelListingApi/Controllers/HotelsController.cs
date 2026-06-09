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
    public ActionResult<IEnumerable<Hotel> Get()
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
    public void Post([FromBody] string value)
    {
        
    }

    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
        
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        
    }
}