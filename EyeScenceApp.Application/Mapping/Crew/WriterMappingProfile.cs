using AutoMapper;
using EyeScenceApp.Application.DTOs.Crew.Writers;
using EyeScenceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.Mapping.Crew
{
    public class WriterMappingProfile :Profile
    {
        public WriterMappingProfile() { 
            configureMappingBetweenCreateWriterandWriter();
            configureMappingBetweenWriterandWriterDTO();
            configureMappingBetweenUpdateWriterandWriter();
        }


        void configureMappingBetweenWriterandWriterDTO()
        {

            CreateMap<Writer, WriterDTO>()
               .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(i => i.Image.Url)));
        }

        void configureMappingBetweenCreateWriterandWriter()
        {

            CreateMap<CreateWriterDTO, Writer>().ForMember(dest => dest.Images, opt => opt.Ignore()); ;

        }

        void configureMappingBetweenUpdateWriterandWriter()
        {
            CreateMap<UpdateWriterDTO, Writer>().ForMember(dest => dest.Images, opt => opt.Ignore()); ;
        }
    }
}
