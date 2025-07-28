using AutoMapper;
using EyeScenceApp.Application.DTOs.Crew.Directors;
using EyeScenceApp.Application.DTOs.SingleDocumentries;
using EyeScenceApp.Application.Mapping.DigitalContent;
using EyeScenceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.Mapping.Crew
{
    public class DirectorMappingProfile : Profile
    {
        public DirectorMappingProfile() {
            configureMappingBetweenCreateDirectorandDirector();
            configureMappingBetweenDirectorandDirectorDTO();
            configureMappingBetweenUpdateDirectorandDirector();
        }


        void configureMappingBetweenDirectorandDirectorDTO()
        {

            CreateMap<Director, DirectorDTO>()
               .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(i => i.Image.Url)));
        }

        void configureMappingBetweenCreateDirectorandDirector()
        {

            CreateMap<CreateDirectorDTO, Director>().ForMember(dest => dest.Images, opt => opt.Ignore()); ;

        }

        void configureMappingBetweenUpdateDirectorandDirector()
        {
            CreateMap<UpdateDirectorDTO, Director>().ForMember(dest => dest.Images, opt => opt.Ignore()); ;
        }
    }
}
