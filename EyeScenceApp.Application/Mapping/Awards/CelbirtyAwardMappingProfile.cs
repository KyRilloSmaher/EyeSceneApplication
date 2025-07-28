using AutoMapper;
using EyeScenceApp.Application.DTOs.Awards;
using EyeScenceApp.Application.DTOs.Awards.CelebirtyAwards;
using EyeScenceApp.Domain.Entities;

namespace EyeScenceApp.Application.Mapping.Awards
{
    public class CelbirtyAwardMappingProfile : Profile
    {
        public CelbirtyAwardMappingProfile()
        {
            CreateMap<Award, AwardDTO>()
                .ForMember(dest => dest.PosterUrl, opt => opt.MapFrom(src => src.Image.Url));


            ConfigureCelebirtyAwardToDTO();
            ConfigureCreateCelebirtyAwardDTOToEntity();
            ConfigureUpdateCelebirtyAwardDTOToEntity();
        }

        private void ConfigureCelebirtyAwardToDTO()
        {
            CreateMap<CelebirtyAward, CelebirtyAwardDTO>()
                .ForMember(dest => dest.PosterUrl, opt => opt.MapFrom(src => src.Image.Url));
              
        }

        private void ConfigureCreateCelebirtyAwardDTOToEntity()
        {
            CreateMap<CreateCelebirtyAwardDTO, CelebirtyAward>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.PosterId, opt => opt.Ignore());
        }

        private void ConfigureUpdateCelebirtyAwardDTOToEntity()
        {
            CreateMap<UpdateCelebirtyAwardDTO, CelebirtyAward>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.PosterId, opt => opt.Ignore());
        }
    }
}
