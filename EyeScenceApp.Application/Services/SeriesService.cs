using AutoMapper;
using EyeScenceApp.Application.Bases;
using EyeScenceApp.Application.DTOs.Actors;
using EyeScenceApp.Application.DTOs.EpisodeDTO;
using EyeScenceApp.Application.DTOs.Series;
using EyeScenceApp.Application.IServices;
using EyeScenceApp.Domain.Entities;
using EyeScenceApp.Domain.Enums;
using EyeScenceApp.Domain.RepositoriesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.Services
{
    public class SeriesService : ISeriesService
    {

        #region Feild(s)
        private readonly ISeriesRepository _seriesRepository;
        private readonly IEpisodeRepository _episodeRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IImageUploaderService _imageUploaderService;
        private readonly IMapper _mapper;
        private readonly ResponseHandler _responseHandler; 
        #endregion


       #region MyRegion
	     public SeriesService(ISeriesRepository seriesRepository, IEpisodeRepository episodeRepository, IMapper mapper, ResponseHandler responseHandler, IImageRepository imageRepository, IImageUploaderService imageUploaderService)
                {
                    _seriesRepository = seriesRepository;
                    _episodeRepository = episodeRepository;
                    _mapper = mapper;
                    _responseHandler = responseHandler;
                    _imageRepository = imageRepository;
                    _imageUploaderService = imageUploaderService;
                }
        #endregion

        #region Method(s)


        public async Task<Response<ICollection<SeriesDTO>>> GetAllSeriesAsync()
        {
            var seriesList = await _seriesRepository.GetAllAsync();
            if (seriesList == null)
            {
                return _responseHandler.NotFound<ICollection<SeriesDTO>>("No series found.");
            }
            var seriesDTOList = _mapper.Map<ICollection<SeriesDTO>>(seriesList);
            return _responseHandler.Success(seriesDTOList);
        }

        public async Task<Response<ICollection<SeriesDTO>>> GetAllSeriesByCountryAsync(Nationality countryName)
        {

            Expression<Func<Series, bool>> searchPredicate = sd => sd.CountryOfOrigin == countryName;

            Expression<Func<Series, double>> orderBy = sd => sd.Rate; // default ordering

            bool ascending = false;
            var seriesList = await _seriesRepository.FilterListAsync(orderBy, searchPredicate, ascending);
            if (seriesList == null)
            {
                return _responseHandler.NotFound<ICollection<SeriesDTO>>("No series found for the given country.");
            }
            var seriesDTOList = _mapper.Map<ICollection<SeriesDTO>>(seriesList);
            return _responseHandler.Success(seriesDTOList);

        }

        public async Task<Response<ICollection<SeriesDTO>>> GetAllSeriesByRealseyearAsync(int year)
        {
            if (year <=0)
            {
                return _responseHandler.BadRequest<ICollection<SeriesDTO>>("Invalid release year provided.");

            }
            Expression<Func<Series, bool>> searchPredicate = sd => sd.ReleaseYear == year;

            Expression<Func<Series, double>> orderBy = sd => sd.Rate; // default ordering

            bool ascending = false;
            var seriesList = await _seriesRepository.FilterListAsync( orderBy, searchPredicate, ascending);
            if (seriesList == null)
            {
                return _responseHandler.NotFound<ICollection<SeriesDTO>>("No series found for the given release year.");
            }
            var seriesDTOList = _mapper.Map<ICollection<SeriesDTO>>(seriesList);
            return _responseHandler.Success(seriesDTOList);

        }

        public async Task<Response<ICollection<SeriesDTO>>> GetNewRealseSeriesAsync()
        {
            var seriesList = await _seriesRepository.GetNewAddedSeriesAsync();
            if (seriesList == null)
            {
                return _responseHandler.NotFound<ICollection<SeriesDTO>>("No series found with the given title.");
            }
            var seriesDTOList = _mapper.Map<ICollection<SeriesDTO>>(seriesList);
            return _responseHandler.Success(seriesDTOList);
        }

        public async Task<Response<SeriesDTO>> GetSeriesByIdAsync(Guid seriesId)
        {
            if (seriesId == Guid.Empty)
            {
                return _responseHandler.BadRequest<SeriesDTO>("Invalid series ID.");
            }
            var series = await _seriesRepository.GetByIdAsync(seriesId,false);
            if (series == null)
            {
                return _responseHandler.NotFound<SeriesDTO>("Series not found.");
            }
            var seriesDTO = _mapper.Map<SeriesDTO>(series);
            return _responseHandler.Success(seriesDTO);
        }


        public async Task<Response<ICollection<EpisodeDTO>>> GetSeriesEpisodesAsync(Guid seriesId)
        {
            if (seriesId == Guid.Empty)
            {
                return _responseHandler.BadRequest<ICollection<EpisodeDTO>>("Invalid series ID.");
            }
            var Existingseries = await _seriesRepository.GetByIdAsync(seriesId, false);
            if (Existingseries == null)
            {
                return _responseHandler.NotFound<ICollection<EpisodeDTO>>("Series not found.");
            }
            // Fetching episodes of the series
            var episodes = await _episodeRepository.GetEpsidoesOfSeriesAsync(seriesId);
            if (episodes == null )
            {
                return _responseHandler.NotFound<ICollection<EpisodeDTO>>("No episodes found for this series.");
            }
            var episodeDTOs = _mapper.Map<ICollection<EpisodeDTO>>(episodes);
            return _responseHandler.Success(episodeDTOs);
        }

        public async Task<Response<ICollection<SeriesDTO>>> GetTopRatedSeriesAsync()
        {
            var seriesList = await _seriesRepository.GetTopRatedSeriesAsync();
            if (seriesList == null )
            {
                return _responseHandler.NotFound<ICollection<SeriesDTO>>("No top-rated series found.");
            }
            var seriesDTOList = _mapper.Map<ICollection<SeriesDTO>>(seriesList);
            return _responseHandler.Success(seriesDTOList);
        }

        public async Task<Response<ICollection<SeriesDTO>>> SearchSeriesByTiltle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return _responseHandler.BadRequest<ICollection<SeriesDTO>>("Title cannot be empty.");
            }
            var seriesList = await _seriesRepository.GetSeriesByTitleAsync(title);
            if (seriesList == null)
            {
                return _responseHandler.NotFound<ICollection<SeriesDTO>>("No series found with the given title.");
            }
            var seriesDTOList = _mapper.Map<ICollection<SeriesDTO>>(seriesList);
            return _responseHandler.Success(seriesDTOList);
        }

        public async Task<Response<ICollection<EpisodeDTO>>> GetEpisodesInSeasonAsync(Guid seriesId , int SeasonNumber)
        {
            if (seriesId == Guid.Empty)
            {
                return _responseHandler.BadRequest<ICollection<EpisodeDTO>>("Invalid series ID.");
            }
            if (SeasonNumber <= 0)
            {
                return _responseHandler.BadRequest<ICollection<EpisodeDTO>>("Invalid season number provided.");
            }
            var existingSeries = await _seriesRepository.GetByIdAsync(seriesId, false);
            if (existingSeries == null)
            {
                return _responseHandler.NotFound<ICollection<EpisodeDTO>>("Series not found.");
            }
            // Fetching episodes of the series for the specified season
            var episodes = await _episodeRepository.GetEpsidoesInSeasonOfSeriesAsync(seriesId, SeasonNumber);
            if (episodes == null)
            {
                return _responseHandler.NotFound<ICollection<EpisodeDTO>>("No episodes found for this series in the specified season.");
            }
            var episodeDTOs = _mapper.Map<ICollection<EpisodeDTO>>(episodes);
            return _responseHandler.Success(episodeDTOs);
        }

        public async Task<Response<EpisodeDTO>> CreateEpisodeAsync(CreateEpisodeDTO createEpisodeDTO)
        {
            if (createEpisodeDTO is null)
            {
                return _responseHandler.BadRequest<EpisodeDTO>("Create Episode DTO cannot be null.");
            }
            var series = await _seriesRepository.GetByIdAsync(createEpisodeDTO.SeriesId, false);
            if (series == null)
            {
                return _responseHandler.NotFound<EpisodeDTO>("Series not found.");
            }
            try
            {
                var episode = _mapper.Map<Episode>(createEpisodeDTO);

                await using var transaction = await _episodeRepository.BeginTransactionAsync();

                try
                {
                    var posterResult = await _imageUploaderService.UploadImageAsync(
                        createEpisodeDTO.Poster,
                        ImageFolder.DigitalContentsImages);

                    if (posterResult.Error is not null)
                    {
                        await transaction.RollbackAsync();
                        return _responseHandler.BadRequest<EpisodeDTO>(null, "Failed to upload poster.");
                    }

                    var poster = new Image
                    {
                        IsPrimary = true,
                        Url = posterResult.Url.ToString()
                    };

                    var createdPoster = await _imageRepository.AddAsync(poster);
                    if (createdPoster == null)
                    {
                        return _responseHandler.Failed<EpisodeDTO>("Failed to save episode poster.");
                    }

                    episode.posterId = createdPoster.Id;

                    var createdEpisode = await _episodeRepository.AddAsync(episode);
                    if (createdEpisode == null)
                    {
                        return _responseHandler.Failed<EpisodeDTO>("Failed to create episode.");
                    }

                    await _episodeRepository.CommitAsync();
                    return _responseHandler.Created(_mapper.Map<EpisodeDTO>(createdEpisode), "Episode created successfully.");
                }
                catch (Exception ex)
                {
                    await _episodeRepository.RollBackAsync();
                    return _responseHandler.Failed<EpisodeDTO>($"An error occurred while creating the episode: {ex.Message}");
                }
            }
            catch
            {
                return _responseHandler.BadRequest<EpisodeDTO>(null, "An error occurred while starting the transaction.");
            }
        }


        public async Task<Response<SeriesDTO>> CreateSeriesAsync(CreateSeriesDTO createSeriesDTO)
        {
            if (createSeriesDTO is null)
            {
                return _responseHandler.BadRequest<SeriesDTO>("Create Series DTO cannot be null.");
            }

            try
            {
               

                await using var transaction = await _seriesRepository.BeginTransactionAsync();

                try
                {
                    var posterResult = await _imageUploaderService.UploadImageAsync(
                        createSeriesDTO.Poster,
                        ImageFolder.DigitalContentsImages);

                    if (posterResult.Error is not null)
                    {
                        await transaction.RollbackAsync();
                        return _responseHandler.BadRequest<SeriesDTO>(null, "Failed to upload primary Poster.");
                    }

                    var poster = new Image
                    {
                        IsPrimary = true,
                        Url = posterResult.Url.ToString()
                    };

                    var createdPoster = await _imageRepository.AddAsync(poster);
                    if (createdPoster == null)
                    {
                        return _responseHandler.Failed<SeriesDTO>("Failed to save series poster.");
                    }
                    var series = _mapper.Map<Series>(createSeriesDTO);
                    series.PosterId = createdPoster.Id;

                    var createdSeries = await _seriesRepository.AddAsync(series);
                    if (createdSeries == null)
                    {
                        return _responseHandler.Failed<SeriesDTO>("Failed to create series.");
                    }

                    await _seriesRepository.CommitAsync();
                    return _responseHandler.Created(_mapper.Map<SeriesDTO>(createdSeries), "Series created successfully.");
                }
                catch (Exception ex)
                {
                    await _seriesRepository.RollBackAsync();
                    return _responseHandler.Failed<SeriesDTO>($"An error occurred while creating the Series: {ex.Message}");
                }
            }
            catch
            {
                return _responseHandler.BadRequest<SeriesDTO>(null, "An error occurred while starting the transaction.");
            }
        }

        public async Task<Response<EpisodeDTO>> UpdateEpisodeAsync(UpdateEpisodeDTO updateEpisodeDTO)
        {
            if (updateEpisodeDTO is null)
            {
                return _responseHandler.BadRequest<EpisodeDTO>("Updated Episode DTO cannot be null.");
            }

            var episode = await _episodeRepository.GetByIdAsync(updateEpisodeDTO.Id, true);
            if (episode == null)
            {
                return _responseHandler.NotFound<EpisodeDTO>("Episode not found.");
            }
            // check if  new series Id Exist In Case it changed
            var series = await _seriesRepository.GetByIdAsync(updateEpisodeDTO.SeriesId, false);
            if (series == null)
            {
                return _responseHandler.NotFound<EpisodeDTO>("Series not found.");
            }
            _mapper.Map(updateEpisodeDTO, episode);

            try
            {
                await using var transaction = await _episodeRepository.BeginTransactionAsync();

                if (updateEpisodeDTO.Poster != null)
                {
                    var oldImage = await _imageRepository.GetByIdAsync(episode.posterId, true);
                    if (oldImage != null)
                    {
                        var result = await _imageUploaderService.DeleteImageByUrlAsync(oldImage.Url);
                        if (!result)
                        {
                            await transaction.RollbackAsync();
                            return _responseHandler.BadRequest<EpisodeDTO>(null, "Failed to delete old poster.");
                        }

                        await _imageRepository.DeleteAsync(oldImage);
                    }

                    var posterResult = await _imageUploaderService.UploadImageAsync(
                        updateEpisodeDTO.Poster,
                        ImageFolder.DigitalContentsImages);

                    if (posterResult.Error is not null)
                    {
                        await transaction.RollbackAsync();
                        return _responseHandler.BadRequest<EpisodeDTO>(null, "Failed to upload new poster.");
                    }

                    var newPoster = new Image
                    {
                        IsPrimary = true,
                        Url = posterResult.Url.ToString()
                    };

                    var createdPoster = await _imageRepository.AddAsync(newPoster);
                    if (createdPoster == null)
                    {
                        return _responseHandler.Failed<EpisodeDTO>("Failed to save new poster.");
                    }

                    episode.posterId = createdPoster.Id;
                }

                await _episodeRepository.UpdateAsync(episode);
                await _episodeRepository.CommitAsync();

                var updatedEpisode = await _episodeRepository.GetByIdAsync(episode.Id, false);
                return _responseHandler.Success(_mapper.Map<EpisodeDTO>(updatedEpisode), "Episode updated successfully.");
            }
            catch (Exception ex)
            {
                await _episodeRepository.RollBackAsync();
                return _responseHandler.Failed<EpisodeDTO>($"An error occurred while updating the episode: {ex.Message}");
            }
        }


        public async Task<Response<SeriesDTO>> UpdateSeriesAsync(UpdateSeriesDTO updateSeriesDTO)
        {
            if (updateSeriesDTO is null)
            {
                return _responseHandler.BadRequest<SeriesDTO>("Updated Series DTO cannot be null.");
            }

            var series = await _seriesRepository.GetByIdAsync(updateSeriesDTO.Id, true);
            if (series == null)
            {
                return _responseHandler.NotFound<SeriesDTO>("Series not found.");
            }

            _mapper.Map(updateSeriesDTO, series);

            try
            {
                await using var transaction = await _seriesRepository.BeginTransactionAsync();

                if (updateSeriesDTO.Poster != null)
                {
                    var oldImage = await _imageRepository.GetByIdAsync(series.PosterId, true);
                    if (oldImage != null)
                    {
                        var result = await _imageUploaderService.DeleteImageByUrlAsync(oldImage.Url);
                        if (!result)
                        {
                            await transaction.RollbackAsync();
                            return _responseHandler.BadRequest<SeriesDTO>(null, "Failed to delete old poster.");
                        }

                        await _imageRepository.DeleteAsync(oldImage);
                    }

                    var posterResult = await _imageUploaderService.UploadImageAsync(
                        updateSeriesDTO.Poster,
                        ImageFolder.DigitalContentsImages);

                    if (posterResult.Error is not null)
                    {
                        await transaction.RollbackAsync();
                        return _responseHandler.BadRequest<SeriesDTO>(null, "Failed to upload new poster.");
                    }

                    var newPoster = new Image
                    {
                        IsPrimary = true,
                        Url = posterResult.Url.ToString()
                    };

                    var createdPoster = await _imageRepository.AddAsync(newPoster);
                    if (createdPoster == null)
                    {
                        return _responseHandler.Failed<SeriesDTO>("Failed to save new poster.");
                    }

                    series.PosterId = createdPoster.Id;
                }

                await _seriesRepository.UpdateAsync(series);
                await _seriesRepository.CommitAsync();

                var updatedSeries = await _seriesRepository.GetByIdAsync(series.Id, false);
                return _responseHandler.Success(_mapper.Map<SeriesDTO>(updatedSeries), "Series updated successfully.");
            }
            catch (Exception ex)
            {
                await _seriesRepository.RollBackAsync();
                return _responseHandler.Failed<SeriesDTO>($"An error occurred while updating the Series: {ex.Message}");
            }
        }


        public async Task<Response<bool>> DeleteEpisodeAsync(Guid episodeId)
        {
            if (episodeId == Guid.Empty)
            {
                return _responseHandler.BadRequest<bool>("Invalid episode ID.");
            }
            var existingEpisode = await _episodeRepository.GetByIdAsync(episodeId, false);
            if (existingEpisode == null)
            {
                return _responseHandler.NotFound<bool>("Episode not found.");
            }
            // Delete the episode
            var image = await _imageRepository.GetByIdAsync(existingEpisode.posterId, false);
            if (image != null)
            {
                var deleteResult = await _imageUploaderService.DeleteImageByUrlAsync(image.Url);
                if (!deleteResult)
                {
                    return _responseHandler.Failed<bool>("Failed to delete Episode poster image.");
                }
                else
                {
                    await _episodeRepository.DeleteAsync(existingEpisode);
                    await _imageRepository.DeleteAsync(image);
                    return _responseHandler.Success(true, "Episode deleted successfully.");
                }
            }
            else
            {
                await _episodeRepository.DeleteAsync(existingEpisode);
                return _responseHandler.Success(true, "Episode deleted successfully ,No poster Found to deleted.");
            }
        }

        public async Task<Response<bool>> DeleteSeriesAsync(Guid seriesId)
        {
            if (seriesId == Guid.Empty)
            {
                return _responseHandler.BadRequest<bool>("Invalid episode ID.");
            }
            var existingSeries = await _seriesRepository.GetByIdAsync(seriesId, false);
            if (existingSeries == null)
            {
                return _responseHandler.NotFound<bool>("Series not found.");
            }
            // Delete the episode
            var image = await _imageRepository.GetByIdAsync(existingSeries.PosterId, false);
            if (image != null)
            {
                var deleteResult = await _imageUploaderService.DeleteImageByUrlAsync(image.Url);
                if (!deleteResult)
                {
                    return _responseHandler.Failed<bool>("Failed to delete Series poster image.");
                }
                else
                {
                    await _imageRepository.DeleteAsync(image);
                    await _seriesRepository.DeleteAsync(existingSeries);
                    return _responseHandler.Success(true, "Series deleted successfully.");
                }
            }
            else
            {
                await _seriesRepository.DeleteAsync(existingSeries);
                return _responseHandler.Success(true, "Series deleted successfully ,No poster Found to deleted.");
            }
        }

        public async Task<Response<ICollection<SeriesDTO>>> GetAllSeriesByGenreAsync(byte genreId)
        {
            if (genreId <= 0)
            {
                return _responseHandler.BadRequest<ICollection<SeriesDTO>>("Invalid genre ID provided.");
            }
            var seriesList = await _seriesRepository.GetSeriesByGenreIdAsync(genreId);
            if (seriesList == null)
            {
                return _responseHandler.NotFound<ICollection<SeriesDTO>>("No series found for the given genre.");
            }
            var seriesDTOList = _mapper.Map<ICollection<SeriesDTO>>(seriesList);
            return _responseHandler.Success(seriesDTOList);
        }


        #endregion


    }
}
