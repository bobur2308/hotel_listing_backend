using System.ComponentModel.DataAnnotations;

namespace HotelListingApi.DTOs.Country;

public class UpdateCountryDto : CreateCountryDto
{
    [Required]
    public long Id { get; set; }
}