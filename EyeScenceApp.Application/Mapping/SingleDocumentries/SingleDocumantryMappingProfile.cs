using AutoMapper;
using EyeScenceApp.Application.DTOs.SingleDocumentries;
using EyeScenceApp.Application.Mapping.DigitalContent;
using EyeScenceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.Mapping.SingleDocumentries
{
    public class MovieMappingProfile : Profile
    {
        public MovieMappingProfile() {
        
            configureMappingBetweenCreateDocumantryandDocumantry();
            configureMappingBetweenDocumantryandDocumantryDTO();
            configureMappingBetweenUpdateDocumantryandDocumantry();
        }


        void configureMappingBetweenDocumantryandDocumantryDTO() {
         
            CreateMap<SingleDocumentary,DocumantaryDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => src.ShortDescription))
                .ForMember(dest => dest.CountryOfOrigin, opt => opt.MapFrom(src => src.CountryOfOrigin))
                .ForMember(dest => dest.DurationByMinutes, opt => opt.MapFrom(src => src.DurationByMinutes))
                .ForMember(dest => dest.ReleaseYear, opt => opt.MapFrom(src => src.ReleaseYear))
                .ForMember(dest => dest.PosterUrl, opt => opt.MapFrom(src => src.Image.Url))
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(g => g.Genre.Name).ToList()))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => DigitalContentMappingProfile.GetContentType(src)))
                .ReverseMap();

        }

        void configureMappingBetweenCreateDocumantryandDocumantry()
        {

            CreateMap<CreateDocumantryDTO, SingleDocumentary>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PosterId, opt => opt.Ignore()) // Ignore Image as it will be set later
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => src.ShortDescription))
                .ForMember(dest => dest.CountryOfOrigin, opt => opt.MapFrom(src => src.CountryOfOrigin))
                .ForMember(dest => dest.DurationByMinutes, opt => opt.MapFrom(src => src.DurationByMinutes))
                .ForMember(dest => dest.ReleaseYear, opt => opt.MapFrom(src => src.ReleaseYear))
                .ForMember(dest => dest.UploadingDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => 0));

        }

        void configureMappingBetweenUpdateDocumantryandDocumantry()
        {
            CreateMap<UpdateDocumantryDTO, SingleDocumentary>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Rate, opt => opt.Ignore())
                .ForMember(dest => dest.UploadingDate, opt => opt.Ignore())
                .ForMember(dest => dest.PosterId, opt => opt.Ignore())
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => src.ShortDescription))
                .ForMember(dest => dest.CountryOfOrigin, opt => opt.MapFrom(src => src.CountryOfOrigin))
                .ForMember(dest => dest.DurationByMinutes, opt => opt.MapFrom(src => src.DurationByMinutes))
                .ForMember(dest => dest.ReleaseYear, opt => opt.MapFrom(src => src.ReleaseYear));
        }
    }
}
