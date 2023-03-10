using AutoMapper;
using SehirRehberi.Api.Dtos;
using SehirRehberi.Entity.Concrete;

namespace SehirRehberi.Api.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<City, CityForListDto>()
                .ForMember(dest => dest.PhotoUrl,
                opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url));
            CreateMap<City, CityForDetailDto>();
            CreateMap<PhotoForCreationDto,Photo>();
            CreateMap<PhotoForReturnDto,Photo>();
        }
    }
}
