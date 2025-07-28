using AutoMapper;
using EyeScenceApp.Application.DTOs.Awards.DigitalContentAwards;
using EyeScenceApp.Domain.Entities;

namespace EyeScenceApp.Application.Mapping.Awards
{
    public class DigitalContentAwardMappingProfile : Profile
    {
        public DigitalContentAwardMappingProfile()
        {
            ConfigureDigitalContentAwardToDTO();
            ConfigureCreateDigitalContentAwardDTOToEntity();
            ConfigureUpdateDigitalContentAwardDTOToEntity();
        }

        private void ConfigureDigitalContentAwardToDTO()
        {
            CreateMap<DigitalContentAward, DigitalContentAwardDTO>()
                .ForMember(dest => dest.PosterUrl, opt => opt.MapFrom(src => src.Image.Url));
        }

        private void ConfigureCreateDigitalContentAwardDTOToEntity()
        {
            CreateMap<CreateDigitalContentAwardDTO, DigitalContentAward>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.PosterId, opt => opt.Ignore());
        }

        private void ConfigureUpdateDigitalContentAwardDTOToEntity()
        {
            CreateMap<UpdateDigitalContentAwardDTO, DigitalContentAward>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.PosterId, opt => opt.Ignore());
        }
    }
}
