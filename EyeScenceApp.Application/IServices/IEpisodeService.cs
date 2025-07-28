using EyeScenceApp.Application.Bases;
using EyeScenceApp.Application.DTOs.EpisodeDTO;
using EyeScenceApp.Application.DTOs.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.IServices
{
    public interface IEpisodeService
    {
        public Task<Response<ICollection<EpisodeDTO>>> GetSeriesEpisodesAsync(Guid seriesId);
        public Task<Response<ICollection<EpisodeDTO>>> GetSeriesEpisodesInSeasonAsync(Guid seriesId, int seasonNum = 1);
        public Task<Response<EpisodeDTO>> CreateEpisodeAsync(CreateEpisodeDTO createEpisodeDTO);
        public Task<Response<EpisodeDTO>> UpdateSeriesAsync(UpdateSeriesDTO updateEpisodeDTO);
        public Task<Response<bool>> DeleteSeiesAsync(Guid episodeId);
    }
}
