using AutoMapper;
using CodebridgeTestTask.Common.DTO;
using CodebridgeTestTask.Dal.Entities;

namespace CodebridgeTestTask.Infrastructure.MappingProfiles
{
    public class DogProfile : Profile
    {
        public DogProfile()
        {
            CreateMap<DogDto, Dog>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ReverseMap();

        }
    }
}
