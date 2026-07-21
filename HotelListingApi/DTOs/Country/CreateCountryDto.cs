using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace HotelListingApi.DTOs.Country;

public class CreateCountryDto
{
    [Required]
    [MaxLength(50)]
    public required string FullName { get; set; }

    [Required] [MaxLength(50)] 
    public required string ShortName { get; set; }
}