using AutoMapper;
using EyeScenceApp.Application.DTOs.EpisodeDTO;
using EyeScenceApp.Domain.Entities;

namespace EyeScenceApp.Application.Mapping.Episode
{
    public class EpisodeMappingProfile : Profile
    {
        public EpisodeMappingProfile()
        {
            ConfigureMappingBetweenEpisodeAndEpisodeDTO();
            ConfigureMappingBetweenCreateEpisodeDTOAndEpisode();
            ConfigureMappingBetweenUpdateEpisodeDTOAndEpisode();
        }

        private void ConfigureMappingBetweenEpisodeAndEpisodeDTO()
        {
            CreateMap<Domain.Entities.Episode, EpisodeDTO>()
                .ForMember(dest => dest.PosterUrl, opt => opt.MapFrom(src => src.poster != null ? src.poster.Url : string.Empty));
        }

        private void ConfigureMappingBetweenCreateEpisodeDTOAndEpisode()
        {
            CreateMap<CreateEpisodeDTO, Domain.Entities.Episode>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => src.ShortDescription))
                .ForMember(dest => dest.DurationByMinutes, opt => opt.MapFrom(src => src.DurationByMinutes))
                .ForMember(dest => dest.Season, opt => opt.MapFrom(src => src.Season))
                .ForMember(dest => dest.EpisodeNumber, opt => opt.MapFrom(src => src.EpisodeNumber))
                .ForMember(dest => dest.SeriesId, opt => opt.MapFrom(src => src.SeriesId))
                .ForMember(dest => dest.posterId, opt => opt.Ignore()) 
                .ForMember(dest => dest.poster, opt => opt.Ignore())   
                .ForMember(dest => dest.Series, opt => opt.Ignore());
        }

        private void ConfigureMappingBetweenUpdateEpisodeDTOAndEpisode()
        {
            CreateMap<UpdateEpisodeDTO, Domain.Entities.Episode>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => src.ShortDescription))
                .ForMember(dest => dest.DurationByMinutes, opt => opt.MapFrom(src => src.DurationByMinutes))
                .ForMember(dest => dest.Season, opt => opt.MapFrom(src => src.Season))
                .ForMember(dest => dest.EpisodeNumber, opt => opt.MapFrom(src => src.EpisodeNumber))
                .ForMember(dest => dest.SeriesId, opt => opt.MapFrom(src => src.SeriesId))
                .ForMember(dest => dest.posterId, opt => opt.Ignore())
                .ForMember(dest => dest.poster, opt => opt.Ignore())
                .ForMember(dest => dest.Series, opt => opt.Ignore());
        }
    }
}
