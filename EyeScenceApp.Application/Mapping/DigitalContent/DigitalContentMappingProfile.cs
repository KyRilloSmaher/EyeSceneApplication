using AutoMapper;
using EyeScenceApp.Application.DTOs.DigitalContent;
using EyeScenceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.Mapping.DigitalContent
{
    public class DigitalContentMappingProfile : Profile
    {
        public DigitalContentMappingProfile()
        {
            CreateMapBetweenDigitalContentAndDTO();


        }

        void CreateMapBetweenDigitalContentAndDTO()
        {
            CreateMap<EyeScenceApp.Domain.Entities.DigitalContent, DigitalContentDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => src.ShortDescription))
                .ForMember(dest => dest.PosterUrl, opt => opt.MapFrom(src => src.Image.Url))
                .ForMember(dest => dest.UploadingDate, opt => opt.MapFrom(src => src.UploadingDate))
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(g => g.Genre.Name).ToList()))
                //.ForMember(dest => dest.Type, opt => opt.MapFrom(src => src switch
                //{
                //    Movie => "Movie",
                //    Series => "Series",
                //    SingleDocumentary => "Documentary",
                //    _ => "Unknown"
                //}));
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => GetContentType(src)));



        }
        public static string GetContentType(EyeScenceApp.Domain.Entities.DigitalContent src) => src switch
        {
            Movie => "Movie",
            Domain.Entities.Series => "Series",
            SingleDocumentary => "Documentary",
            _ => "Unknown"
        };

    }
 }
