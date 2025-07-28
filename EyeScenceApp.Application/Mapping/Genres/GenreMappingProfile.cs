using AutoMapper;
using EyeScenceApp.Application.DTOs.Genres;
using EyeScenceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.Mapping.Genres
{
    public class GenreMappingProfile : Profile
    {
        public GenreMappingProfile() {
            MapFromCreateGenreDTOToGenre();
            MapFromGenreToGenreDTO();
            MapFromUpdateGenreDTOToGenre();
        }


        void MapFromGenreToGenreDTO()
        {
            CreateMap<Genre, GenreDTO>()
                  .ForMember(dest=>dest.PosterUrl ,opt=>opt.MapFrom(src=>src.Image.Url));
        }
        void MapFromCreateGenreDTOToGenre()
        {
            CreateMap<CreateGenreDTO, Genre>()
                 .ForMember(dest => dest.PosterId, opt => opt.Ignore());
        }
    
        void MapFromUpdateGenreDTOToGenre()
        {
            CreateMap<UpdateGenreDTO, Genre>()
             .ForMember(dest => dest.PosterId, opt => opt.Ignore());
        }
    }
}
