using AutoMapper;
using PortalToWork.Models.H4G;
using PortalToWork.Models.Algolia;
using PortalToWork.Models;
using System.Linq;

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
                )
                .ForMember(
                    dest => dest.geodata,
                    opt => opt.MapFrom(
                        src => src.locations.data
                        .Select(l => new GeoData
                        {
                            lat = l.lat,
                            lng = l.lng,
                        })
                    )
                );
        }
    }
}