using AutoMapper;
using EyeScenceApp.Application.Bases;
using EyeScenceApp.Application.DTOs.Actors;
using EyeScenceApp.Application.DTOs.Movies;
using EyeScenceApp.Application.DTOs.SingleDocumentries;
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
    public class MovieService : IMovieService
    {
        #region Field(s)
        private readonly IMapper _mapper;
        private readonly IImageUploaderService _imageUploaderService;
        private readonly IImageRepository _imageRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly ICastRepository _castRepository;
        private readonly ResponseHandler _responseHandler;
        #endregion

        #region Constructor(s)
        public MovieService(IMapper mapper, IImageUploaderService imageUploaderService, IImageRepository imageRepository, IMovieRepository movieRepository, ResponseHandler responseHandler, ICastRepository castRepository)
        {
            _mapper = mapper;
            _imageUploaderService = imageUploaderService;
            _imageRepository = imageRepository;
            _movieRepository = movieRepository;
            _responseHandler = responseHandler;
            _castRepository = castRepository;
        }


        #endregion

        #region Method(s)



        public async Task<Response<ICollection<MovieDTO>>> GetAllMoviesAsync()
        {
            var movies = await _movieRepository.GetAllAsync();
            if (movies == null || !movies.Any())
            {
                return _responseHandler.NotFound<ICollection<MovieDTO>>("No movies found.");
            }

            var movieDTOs = _mapper.Map<ICollection<MovieDTO>>(movies);
            return _responseHandler.Success(movieDTOs);

        }
        public async Task<Response<ICollection<MovieDTO>>> GetAllMoviesByCountryAsync(Nationality countryName)
        {
            Expression<Func<Movie, bool>> searchPredicate = sd => sd.CountryOfOrigin == countryName;

            Expression<Func<Movie, double>> orderBy = sd => sd.Rate; // default ordering

            bool ascending = false;
            var movies = await _movieRepository.FilterListAsync(orderBy, searchPredicate, ascending);
            if (movies == null || !movies.Any())
            {
                return _responseHandler.NotFound<ICollection<MovieDTO>>($"No Movies found for country: {countryName}.");
            }
            var movieDTOs = _mapper.Map<ICollection<MovieDTO>>(movies);
            return _responseHandler.Success(movieDTOs, $"Movies from {countryName} retrieved successfully.");
        }
        public async Task<Response<ICollection<MovieDTO>>> GetAllMoviesByRealseyearAsync(int year)
        {
            Expression<Func<Movie, bool>> searchPredicate = sd => sd.ReleaseYear == year;

            Expression<Func<Movie, double>> orderBy = sd => sd.Rate; // default ordering

            bool ascending = false;
            var movies = await _movieRepository.FilterListAsync(orderBy, searchPredicate, ascending);
            if (movies == null || !movies.Any())
            {
                return _responseHandler.NotFound<ICollection<MovieDTO>>($"No Movies found In year : {year}.");
            }
            var movieDTOs = _mapper.Map<ICollection<MovieDTO>>(movies);
            return _responseHandler.Success(movieDTOs, $"Movies from {year} retrieved successfully.");
        }
        public async Task<Response<ICollection<MovieDTO>>> GetNewRealseMoviesAsync()
        {
            var movies = await _movieRepository.GetNewAddedMoviesAsync();
            if (movies == null || !movies.Any())
            {
                return _responseHandler.NotFound<ICollection<MovieDTO>>("No movies found.");
            }

            var movieDTOs = _mapper.Map<ICollection<MovieDTO>>(movies);
            return _responseHandler.Success(movieDTOs);
        }
        public async Task<Response<MovieDTO>> GetMovieAsync(Guid movieId)
        {
            if (movieId == Guid.Empty)
            {
                return _responseHandler.BadRequest<MovieDTO>("Movie ID cannot be empty.");
            }
            var movie = await _movieRepository.GetByIdAsync(movieId,false);
            if (movie == null) {
                return _responseHandler.NotFound<MovieDTO>($"Movie with ID {movieId} not found.");
            }
            var movieDTO = _mapper.Map<MovieDTO>(movie);
            return _responseHandler.Success(movieDTO);
        }
        public async Task<Response<ICollection<MovieDTO>>> GetTopRatedMoviesAsync()
        {
            var movies = await _movieRepository.GetTopRatedMoviesAsync();
            if (movies == null || !movies.Any())
            {
                return _responseHandler.NotFound<ICollection<MovieDTO>>("No movies found.");
            }
            var movieDTOs = _mapper.Map<ICollection<MovieDTO>>(movies);
            return _responseHandler.Success(movieDTOs);

        }
        public async Task<Response<ICollection<MovieDTO>>> SearchMoviesByTiltle(string title)
        {
            Expression<Func<Movie, bool>> searchPredicate = sd => sd.Title.Contains(title) || sd.ShortDescription.Contains(title);

            Expression<Func<Movie, double>> orderBy = sd => sd.Rate; // default ordering

            bool ascending = false;
            var movies = await _movieRepository.FilterListAsync(orderBy, searchPredicate, ascending);
            if (movies == null || !movies.Any())
            {
                return _responseHandler.NotFound<ICollection<MovieDTO>>($"No Movies found contains title : {title}.");
            }
            var movieDTOs = _mapper.Map<ICollection<MovieDTO>>(movies);
            return _responseHandler.Success(movieDTOs, $"Movies contains {title} retrieved successfully.");
        }
        public async Task<Response<MovieDTO>> CreateMovieAsync(CreateMovieDTO createMovieDTO)
        {
            if (createMovieDTO is null)
            {
                return _responseHandler.BadRequest<MovieDTO>("Create Movie DTO can not be null");

            }
            try
            {
                var movie = _mapper.Map<Movie>(createMovieDTO);
                // Begin transaction
                await using var transaction = await _movieRepository.BeginTransactionAsync();

                try
                {
                    // Upload primary image
                    var PosterResult = await _imageUploaderService.UploadImageAsync(
                    createMovieDTO.Poster,
                    ImageFolder.DigitalContentsImages);

                    if (PosterResult.Error is not null)
                    {
                        await transaction.RollbackAsync();
                        return _responseHandler.BadRequest<MovieDTO>(null, "Failed to upload primary Poster.");
                    }
                    // Create image record
                    var Poster = new Image
                    {
                        IsPrimary = true,
                        Url = PosterResult.Url.ToString()
                    };
                    var createdPoster = await _imageRepository.AddAsync(Poster);

                    if (createdPoster == null)
                    {
                        return _responseHandler.Failed<MovieDTO>("Failed to Save Movie poster.");
                    }
                    movie.PosterId = createdPoster.Id;
                    var createdMovie = await _movieRepository.AddAsync(movie);

                    if (createdMovie == null)
                    {
                        return _responseHandler.Failed<MovieDTO>("Failed to create Movie.");
                    }

                    await _movieRepository.CommitAsync();
                    return _responseHandler.Created(_mapper.Map<MovieDTO>(createdMovie), "Movie created successfully.");

                }
                catch (Exception ex)
                {
                    // Handle exceptions and rollback transaction
                    await _movieRepository.RollBackAsync();
                    return _responseHandler.Failed<MovieDTO>($"An error occurred while creating the Movie: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                return _responseHandler.BadRequest<MovieDTO>(null, "An error occurred while starting the transaction.");
            }
        }
        public async Task<Response<bool>> DeleteMovieAsync(Guid movieId)
        {
            if(movieId == Guid.Empty)
            {
                return _responseHandler.BadRequest<bool>("Movie ID cannot be empty.");
            }
            var movie = await _movieRepository.GetByIdAsync(movieId, false);
            if (movie == null)
            {
                return _responseHandler.NotFound<bool>($"Movie with ID {movieId} not found.");
            }
            var image = await _imageRepository.GetByIdAsync(movie.PosterId, false);
            if (image != null)
            {
                var deleteResult = await _imageUploaderService.DeleteImageByUrlAsync(image.Url);
                if (!deleteResult)
                {
                    return _responseHandler.Failed<bool>("Failed to delete movie poster image.");
                }
                else
                {
                    await _imageRepository.DeleteAsync(image);
                    await _movieRepository.DeleteAsync(movie);
                    return _responseHandler.Success(true, "Movie deleted successfully.");
                }
            }
            else
            {
                await _movieRepository.DeleteAsync(movie);
                return _responseHandler.Success(true, "Movie deleted successfully ,No poster Found to deleted.");
            }

        }
        public async Task<Response<MovieDTO>> UpdateMovieAsync(UpdateMovieDTO updateMovieDTO)
        {
            if (updateMovieDTO is null)
            {
                return _responseHandler.BadRequest<MovieDTO>(" Updated Movie DTO can not be null");

            }

            var movie = await _movieRepository.GetByIdAsync(updateMovieDTO.Id, true);
            if (movie == null)
            {
                return _responseHandler.NotFound<MovieDTO>("movie not found.");
            }
            _mapper.Map(updateMovieDTO, movie);
            try
            {
                // Begin transaction
                await using var transaction = await _movieRepository.BeginTransactionAsync();
                if (updateMovieDTO.Poster is not null)
                {
                    // If a new poster is provided, delete the old one if it exists
                    var deleteImage = await _imageRepository.GetByIdAsync(movie.Id, true);
                    if (deleteImage != null)
                    {
                        var result = await _imageUploaderService.DeleteImageByUrlAsync(deleteImage.Url);
                        if (!result)
                        {
                            await transaction.RollbackAsync();
                            return _responseHandler.BadRequest<MovieDTO>(null, "Failed to delete old Poster.");
                        }
                        await _imageRepository.DeleteAsync(deleteImage);
                    }
                    // Upload primary image
                    var PosterResult = await _imageUploaderService.UploadImageAsync(
                        updateMovieDTO.Poster,
                        ImageFolder.DigitalContentsImages);

                    if (PosterResult.Error is not null)
                    {
                        await transaction.RollbackAsync();
                        return _responseHandler.BadRequest<MovieDTO>(null, "Failed to upload primary Poster.");
                    }
                    // Create image record
                    var Poster = new Image
                    {
                        IsPrimary = true,
                        Url = PosterResult.Url.ToString()
                    };
                    var createdPoster = await _imageRepository.AddAsync(Poster);

                    if (createdPoster == null)
                    {
                        return _responseHandler.Failed<MovieDTO>("Failed to Save movie poster.");
                    }
                    movie.PosterId = createdPoster.Id;

                }


                await _movieRepository.UpdateAsync(movie);

                await _movieRepository.CommitAsync();
                var createdMovie = await _movieRepository.GetByIdAsync(movie.Id, false);
                return _responseHandler.Success(_mapper.Map<MovieDTO>(createdMovie), "Movie created successfully.");

            }
            catch (Exception ex)
            {
                // Handle exceptions and rollback transaction
                await _movieRepository.RollBackAsync();
                return _responseHandler.Failed<MovieDTO>($"An error occurred while creating the Movie: {ex.Message}");
            }
        }
        public async Task<Response<ICollection<MovieDTO>>> GetTopMoviesByRevenuesAsync()
        {
            var movies = await _movieRepository.GetMoviesOrderedByReveuesAsync();
            if (movies == null || !movies.Any())
            {
                return _responseHandler.NotFound<ICollection<MovieDTO>>("No Movies found ");
            }
            var movieDTOs = _mapper.Map<ICollection<MovieDTO>>(movies);
            return _responseHandler.Success(movieDTOs, "Movies retrieved successfully.");
        }
        public async Task<Response<ICollection<ActorDTO>>> GetMovieCastAsync(Guid movieId)
        {
            if (movieId == Guid.Empty)
            {
                return _responseHandler.BadRequest<ICollection<ActorDTO>>("Movie Id Not Valid");
            }
            var ExistMovie = await _movieRepository.GetByIdAsync(movieId, false);
            if (ExistMovie == null)
            {
                return _responseHandler.NotFound<ICollection<ActorDTO>>($"Movie with ID {movieId} not found.");
            }
            var actors = await _castRepository.GetActorsByMovieIdAsync(movieId);
            if (actors == null || !actors.Any())
            {
                return _responseHandler.NotFound<ICollection<ActorDTO>>($"No cast found for movie with ID {movieId}.");
            }
            var actorDTOs = _mapper.Map<ICollection<ActorDTO>>(actors);
            return _responseHandler.Success(actorDTOs, $"Cast for movie with ID {movieId} retrieved successfully.");
        }
        #endregion

    }
}
