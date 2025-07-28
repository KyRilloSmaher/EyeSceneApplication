using AutoMapper;
using EyeScenceApp.Application.DTOs.Crew.SoundDesigners;
using EyeScenceApp.Domain.Entities;


namespace EyeScenceApp.Application.Mapping.Crew
{
    public class SoundDesignerMappingProfile :Profile
    {
        public SoundDesignerMappingProfile() {
            configureMappingBetweenCreateSoundDesignerandSoundDesigner();
            configureMappingBetweenSoundDesignerandSoundDesignerDTO();
            configureMappingBetweenUpdateSoundDesignerandSoundDesigner();
        }


        void configureMappingBetweenSoundDesignerandSoundDesignerDTO()
        {

            CreateMap<SoundDesigner, SoundDesignerDTO>()
               .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(i => i.Image.Url)));
        }

        void configureMappingBetweenCreateSoundDesignerandSoundDesigner()
        {

            CreateMap<CreateSoundDesignerDTO, SoundDesigner>().ForMember(dest => dest.Images, opt => opt.Ignore()); ;

        }

        void configureMappingBetweenUpdateSoundDesignerandSoundDesigner()
        {
            CreateMap<UpdateSoundDesignerDTO, SoundDesigner>().ForMember(dest => dest.Images, opt => opt.Ignore()); ;
        }
    }
}
