using AutoMapper;
using PortalToWork.Models.H4G;
using PortalToWork.Models.Algolia;
using PortalToWork.Models;

namespace PortalToWork.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<H4GJob, AlgoliaJob>()
                .ForMember(
                    dest => dest.ObjectId,
                    opt => opt.MapFrom(src => src.id)
                );

            CreateMap<H4GLocationList, AlgoliaLocationList>();

            CreateMap<H4GLocation, AlgoliaLocation>()
                .ForMember(
                    dest => dest.geodata,
                    opt => opt.MapFrom(src => new GeoData { lat = src.lat, lng = src.lng })
                );
        }
    }
}