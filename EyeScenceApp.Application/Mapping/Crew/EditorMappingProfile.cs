using AutoMapper;
using EyeScenceApp.Application.DTOs.Crew.Editiors;
using EyeScenceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.Mapping.Crew
{
    public class EditorMappingProfile : Profile
    {
        public EditorMappingProfile() {
            configureMappingBetweenCreateEditorandEditor();
            configureMappingBetweenEditorandEditorDTO();
            configureMappingBetweenUpdateEditorandEditor();
        }


        void configureMappingBetweenEditorandEditorDTO()
        {

            CreateMap<Editor, EditorDTO>()
               .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(i => i.Image.Url)));
        }

        void configureMappingBetweenCreateEditorandEditor()
        {

            CreateMap<CreateEditorDTO, Editor>().ForMember(dest => dest.Images, opt => opt.Ignore()); ;

        }

        void configureMappingBetweenUpdateEditorandEditor()
        {
            CreateMap<UpdateEditorDTO, Editor>().ForMember(dest => dest.Images, opt => opt.Ignore()); ;
        }
    }
}
