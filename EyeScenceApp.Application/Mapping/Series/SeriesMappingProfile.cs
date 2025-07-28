using AutoMapper;
using EyeScenceApp.Application.DTOs.Series;
using EyeScenceApp.Application.Mapping.DigitalContent;
using EyeScenceApp.Domain.Entities;
using System.Linq;

namespace EyeScenceApp.Application.Mapping.Series
{
    public class SeriesMappingProfile : Profile
    {
        public SeriesMappingProfile()
        {
            ConfigureMappingBetweenSeriesAndSeriesDTO();
            ConfigureMappingBetweenCreateSeriesDTOAndSeries();
            ConfigureMappingBetweenUpdateSeriesDTOAndSeries();
        }

        void ConfigureMappingBetweenSeriesAndSeriesDTO()
        {
            CreateMap<Domain.Entities.Series, SeriesDTO>()
                .ForMember(dest => dest.PosterUrl, opt => opt.MapFrom(src => src.Image != null ? src.Image.Url : string.Empty))
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(g => g.Genre.Name).ToList()))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => DigitalContentMappingProfile.GetContentType(src)));
        }

        void ConfigureMappingBetweenCreateSeriesDTOAndSeries()
        {
            CreateMap<CreateSeriesDTO, Domain.Entities.Series>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => src.ShortDescription))
                .ForMember(dest => dest.CountryOfOrigin, opt => opt.MapFrom(src => src.CountryOfOrigin))
                .ForMember(dest => dest.DurationByMinutes, opt => opt.Ignore())
                .ForMember(dest => dest.ReleaseYear, opt => opt.MapFrom(src => src.ReleaseYear))
                .ForMember(dest => dest.SeasonsCount, opt => opt.MapFrom(src => src.SeasonsCount))
                .ForMember(dest => dest.EpisodesCount, opt => opt.MapFrom(src => src.EpisodesCount))
                .ForMember(dest => dest.UploadingDate, opt => opt.Ignore())
                .ForMember(dest => dest.PosterId, opt => opt.Ignore())
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.Genres, opt => opt.Ignore())
                .ForMember(dest => dest.Ratings, opt => opt.Ignore())
                .ForMember(dest => dest.FavoriteMovies, opt => opt.Ignore())
                .ForMember(dest => dest.WatchListMovies, opt => opt.Ignore())
                .ForMember(dest => dest.MovieCasts, opt => opt.Ignore())
                .ForMember(dest => dest.WorksOn, opt => opt.Ignore())
                .ForMember(dest => dest.Awards, opt => opt.Ignore());
        }

        void ConfigureMappingBetweenUpdateSeriesDTOAndSeries()
        {
            CreateMap<UpdateSeriesDTO, Domain.Entities.Series>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => src.ShortDescription))
                .ForMember(dest => dest.CountryOfOrigin, opt => opt.MapFrom(src => src.CountryOfOrigin))
                .ForMember(dest => dest.DurationByMinutes, opt => opt.MapFrom(src => src.DurationByMinutes))
                .ForMember(dest => dest.ReleaseYear, opt => opt.MapFrom(src => src.ReleaseYear))
                .ForMember(dest => dest.SeasonsCount, opt => opt.MapFrom(src => src.SeasonsCount))
                .ForMember(dest => dest.EpisodesCount, opt => opt.MapFrom(src => src.EpisodesCount))
                .ForMember(dest => dest.PosterId, opt => opt.Ignore()) 
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.Genres, opt => opt.Ignore())
                .ForMember(dest => dest.Ratings, opt => opt.Ignore())
                .ForMember(dest => dest.FavoriteMovies, opt => opt.Ignore())
                .ForMember(dest => dest.WatchListMovies, opt => opt.Ignore())
                .ForMember(dest => dest.MovieCasts, opt => opt.Ignore())
                .ForMember(dest => dest.WorksOn, opt => opt.Ignore())
                .ForMember(dest => dest.Awards, opt => opt.Ignore());
        }
    }
}
