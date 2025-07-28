using EyeScenceApp.API.Helpers;
using EyeScenceApp.Application.DTOs.Actors;
using EyeScenceApp.Application.DTOs.EpisodeDTO;
using EyeScenceApp.Application.DTOs.Series;
using EyeScenceApp.Application.IServices;
using EyeScenceApp.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EyeScenceApp.API.Controllers
{
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly ISeriesService _seriesService;

        public SeriesController(ISeriesService seriesService)
        {
            _seriesService = seriesService;
        }

        // ********** GET **********

        [HttpGet(Router.Series.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _seriesService.GetAllSeriesAsync();
            return Router.FinalResponse<ICollection<SeriesDTO>>(response);
        }

        [HttpGet(Router.Series.GetById)]
        public async Task<IActionResult> GetById([FromRoute] Guid Id)
        {
            var response = await _seriesService.GetSeriesByIdAsync(Id);
            return Router.FinalResponse<SeriesDTO>(response);
        }

        [HttpGet(Router.Series.GetByTitle)]
        public async Task<IActionResult> GetByTitle([FromRoute] string title)
        {
            var response = await _seriesService.SearchSeriesByTiltle(title);
            return Router.FinalResponse<ICollection<SeriesDTO>>(response);
        }

        [HttpGet(Router.Series.GetSeriesEpisodes)]
        public async Task<IActionResult> GetEpisodes([FromRoute] Guid Id)
        {
            var response = await _seriesService.GetSeriesEpisodesAsync(Id);
            return Router.FinalResponse<ICollection<EpisodeDTO>>(response);
        }

        [HttpGet(Router.Series.GetSeriesEpisodesInSeason)]
        public async Task<IActionResult> GetEpisodesInSeason([FromRoute] Guid Id, [FromRoute] int seasonNum)
        {
            var response = await _seriesService.GetEpisodesInSeasonAsync(Id, seasonNum);
            return Router.FinalResponse<ICollection<EpisodeDTO>>(response);
        }

        [HttpGet(Router.Series.GetTopRated)]
        public async Task<IActionResult> GetTopRated()
        {
            var response = await _seriesService.GetTopRatedSeriesAsync();
            return Router.FinalResponse<ICollection<SeriesDTO>>(response);
        }

        [HttpGet(Router.Series.GetNewRealse)]
        public async Task<IActionResult> GetNewlyAdded()
        {
            var response = await _seriesService.GetNewRealseSeriesAsync();
            return Router.FinalResponse<ICollection<SeriesDTO>>(response);
        }
        [HttpGet(Router.Series.GetSeriesByGenre)]
        public async Task<IActionResult> GetSeriesInGenre([FromRoute] byte genreId)
        {
            var response = await _seriesService.GetAllSeriesByGenreAsync(genreId);
            return Router.FinalResponse<ICollection<SeriesDTO>>(response);
        }
        // ********** POST **********

        [HttpPost(Router.Series.CreateSeries)]
        public async Task<IActionResult> CreateSeries([FromForm] CreateSeriesDTO dto)
        {
            var response = await _seriesService.CreateSeriesAsync(dto);
            return Router.FinalResponse<SeriesDTO>(response);
        }

        [HttpPost(Router.Series.AddEpisode)]
        public async Task<IActionResult> CreateEpisode([FromForm] CreateEpisodeDTO dto)
        {
            var response = await _seriesService.CreateEpisodeAsync(dto);
            return Router.FinalResponse<EpisodeDTO>(response);
        }

        // ********** PUT **********

        [HttpPut(Router.Series.UpdateSeries)]
        public async Task<IActionResult> UpdateSeries([FromForm] UpdateSeriesDTO dto)
        {
            var response = await _seriesService.UpdateSeriesAsync(dto);
            return Router.FinalResponse<SeriesDTO>(response);
        }

        [HttpPut(Router.Series.UpdateEpisode)]
        public async Task<IActionResult> UpdateEpisode([FromForm] UpdateEpisodeDTO dto)
        {
            var response = await _seriesService.UpdateEpisodeAsync(dto);
            return Router.FinalResponse<EpisodeDTO>(response);
        }

        // ********** DELETE **********

        [HttpDelete(Router.Series.DeleteSeries)]
        public async Task<IActionResult> DeleteSeries([FromRoute] Guid Id)
        {
            var response = await _seriesService.DeleteSeriesAsync(Id);
            return Router.FinalResponse<bool>(response);
        }

        [HttpDelete(Router.Series.DeleteEpisode)]
        public async Task<IActionResult> DeleteEpisode([FromRoute] Guid Id)
        {
            var response = await _seriesService.DeleteEpisodeAsync(Id);
            return Router.FinalResponse<bool>(response);
        }
    }
}
