using HotelListingApi.Data;
using HotelListingApi.DTOs.Country;
using HotelListingApi.DTOs.Hotel;
using HotelListingApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelListingApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CountriesController:ControllerBase
{   
    private readonly ICountriesService  _service;

    public CountriesController(ICountriesService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult> GetCountries()
    {
        var countries = await _service.GetCountries();
        
        return Ok(countries);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetCountry(int id)
    {
        var country = await _service.GetCountryAsync(id);

        if (country == null)
        {
            return NotFound();
        }
        return Ok(country);
    }

    [HttpPost]
    public async Task<ActionResult> PostCountry(CreateCountryDto countryDto)
    {
        var country = await _service.CreateCountryAsync(countryDto);
        
        return CreatedAtAction("GetCountry", new { id = country.Id }, country);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCountry(int id, UpdateCountryDto countryDto)
    {
        if(id != countryDto.Id) return BadRequest();
        
        await _service.UpdateCountryAsync(id,countryDto);
        
        return NoContent(); 
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCountry(int id)
    {
        await _service.DeleteCountryAsync(id);
        
        return NoContent();
    }

}