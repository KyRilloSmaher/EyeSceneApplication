using EyeScenceApp.Application.Bases;
using EyeScenceApp.Application.DTOs.Actors;
using EyeScenceApp.Application.DTOs.EpisodeDTO;
using EyeScenceApp.Application.DTOs.Series;
using EyeScenceApp.Application.DTOs.Series;
using EyeScenceApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.IServices
{
    public interface ISeriesService
    {
        public Task<Response<SeriesDTO>> GetSeriesByIdAsync(Guid seriesId);
        public Task<Response<ICollection<SeriesDTO>>> SearchSeriesByTiltle(string title);
        public Task<Response<ICollection<SeriesDTO>>> GetAllSeriesAsync();
        public Task<Response<ICollection<SeriesDTO>>> GetAllSeriesByCountryAsync(Nationality countryName);
        public Task<Response<ICollection<SeriesDTO>>> GetAllSeriesByRealseyearAsync(int year);
        public Task<Response<ICollection<SeriesDTO>>> GetAllSeriesByGenreAsync(byte genreId);
        public Task<Response<ICollection<SeriesDTO>>> GetTopRatedSeriesAsync();
        public Task<Response<ICollection<SeriesDTO>>> GetNewRealseSeriesAsync();
        public Task<Response<ICollection<EpisodeDTO>>> GetSeriesEpisodesAsync(Guid seriesId);
        public Task<Response<ICollection<EpisodeDTO>>> GetEpisodesInSeasonAsync(Guid seriesId, int SeasonNumber);
        public Task<Response<SeriesDTO>> CreateSeriesAsync(CreateSeriesDTO createSeriesDTO);
        public Task<Response<EpisodeDTO>> CreateEpisodeAsync(CreateEpisodeDTO createEpisodeDTO);
        public Task<Response<SeriesDTO>> UpdateSeriesAsync(UpdateSeriesDTO updateSeriesDTO);
        public Task<Response<EpisodeDTO>> UpdateEpisodeAsync(UpdateEpisodeDTO updateEpisodeDTO);
        public Task<Response<bool>> DeleteSeriesAsync(Guid seriesId);
        public Task<Response<bool>> DeleteEpisodeAsync(Guid episodeId);


    }
}
