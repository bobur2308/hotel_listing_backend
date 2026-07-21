using System.ComponentModel.DataAnnotations;

namespace HotelListingApi.DTOs.Hotel;

public class CreateHotelDto
{
    [Required]
    public required string FullName { get; set; }
    
    [MaxLength(150)]
    public required string Address { get; set; }
    
    [Range(0, 5)]
    public double Rating { get; set; }
    
    [Required]
    public int CountryId { get; set; }
}