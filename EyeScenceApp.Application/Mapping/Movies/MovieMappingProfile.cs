using AutoMapper;
using EyeScenceApp.Application.DTOs.Movies;
using EyeScenceApp.Application.DTOs.SingleDocumentries;
using EyeScenceApp.Application.Mapping.DigitalContent;
using EyeScenceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.Mapping.Movies

{
    public class MovieMappingProfile : Profile
    {
        public MovieMappingProfile() {
        
            configureMappingBetweenCreateMovieandMovie();
            configureMappingBetweenMovieandMovieDTO();
            configureMappingBetweenUpdateMovieandMovie();
        }


        void configureMappingBetweenMovieandMovieDTO() {
         
            CreateMap<Movie,MovieDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => src.ShortDescription))
                .ForMember(dest => dest.CountryOfOrigin, opt => opt.MapFrom(src => src.CountryOfOrigin))
                .ForMember(dest => dest.DurationByMinutes, opt => opt.MapFrom(src => src.DurationByMinutes))
                .ForMember(dest => dest.ReleaseYear, opt => opt.MapFrom(src => src.ReleaseYear))
                .ForMember(dest => dest.PosterUrl, opt => opt.MapFrom(src => src.Image.Url))
                .ForMember(dest => dest.Revenues, opt => opt.MapFrom(src => src.Revenues))
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(g => g.Genre.Name).ToList()))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => DigitalContentMappingProfile.GetContentType(src)))
                .ReverseMap();

        }

        void configureMappingBetweenCreateMovieandMovie()
        {

            CreateMap<CreateMovieDTO, Movie>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PosterId, opt => opt.Ignore()) // Ignore Image as it will be set later
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => src.ShortDescription))
                .ForMember(dest => dest.CountryOfOrigin, opt => opt.MapFrom(src => src.CountryOfOrigin))
                .ForMember(dest => dest.DurationByMinutes, opt => opt.MapFrom(src => src.DurationByMinutes))
                .ForMember(dest => dest.ReleaseYear, opt => opt.MapFrom(src => src.ReleaseYear))
                .ForMember(dest => dest.UploadingDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.Revenues, opt => opt.MapFrom(src => src.Revenues));


        }

        void configureMappingBetweenUpdateMovieandMovie()
        {
            CreateMap<UpdateMovieDTO, Movie>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Rate, opt => opt.Ignore())
                .ForMember(dest => dest.UploadingDate, opt => opt.Ignore())
                .ForMember(dest => dest.PosterId, opt => opt.Ignore())
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => src.ShortDescription))
                .ForMember(dest => dest.CountryOfOrigin, opt => opt.MapFrom(src => src.CountryOfOrigin))
                .ForMember(dest => dest.DurationByMinutes, opt => opt.MapFrom(src => src.DurationByMinutes))
                .ForMember(dest => dest.ReleaseYear, opt => opt.MapFrom(src => src.ReleaseYear))
                .ForMember(dest => dest.Revenues, opt => opt.MapFrom(src => src.Revenues));
        }
    }
}
