using AutoMapper;
using PortalToWork.Models.Algolia;
using PortalToWork.Models.H4G;

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
        }
    }
}