using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListingApi.Data;

[Table("hotels")]
public class Hotel
{
    [Column("id")]
    public int Id { get; set; }
    [Column("full_name")]
    public string FullName { get; set; }
    [Column("address")]
    public string Address { get; set; }
    [Column("rating")]
    public double Rating { get; set; }
    [Column("country_id")]
    public int CountryId { get; set; }
    public Country? Country { get; set; }
}