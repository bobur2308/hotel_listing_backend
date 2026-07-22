using AutoMapper;
using HotelListingApi.Data;
using HotelListingApi.DTOs.Country;
using HotelListingApi.DTOs.Hotel;

namespace HotelListingApi.MappingProfile;

public class HotelMappingProfile:Profile
{
    public HotelMappingProfile()
    {
        CreateMap<Hotel,GetHotelDto>()
            .ForMember(x => x.Country, opt => opt.MapFrom(s => s.Country!.FullName));
        CreateMap<CreateHotelDto, Hotel>();
    }
}

public class CountryMappingProfile:Profile
{
    public CountryMappingProfile()
    {
        CreateMap<Country,GetCountryDto>();
        CreateMap<Country,GetCountriesDto>();
        CreateMap<CreateCountryDto, Country>();
    }
}