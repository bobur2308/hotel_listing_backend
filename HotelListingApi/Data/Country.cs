using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListingApi.Data;

[Table("countries")]
public class Country
{
    [Column("id")]
    public int Id { get; set; }
    [Column("full_name")]
    public string FullName { get; set; }
    [Column("short_name")]
    public string ShortName { get; set; }
    public IList<Hotel> Hotels { get; set; } = [];
}