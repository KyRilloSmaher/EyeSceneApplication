using AutoMapper;
using EyeScenceApp.Application.Bases;
using EyeScenceApp.Application.DTOs.Actors;
using EyeScenceApp.Application.DTOs.Genres;
using EyeScenceApp.Application.IServices;
using EyeScenceApp.Domain.Entities;
using EyeScenceApp.Domain.Enums;
using EyeScenceApp.Domain.RepositoriesInterfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.Services
{
    public class GenreService : IGenreService
    {
        #region Fields
        private readonly IGenreRepository _genreRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IImageUploaderService _imageUploader;
        private readonly ResponseHandler _responseHandler;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public GenreService(
            IGenreRepository genreRepository,
            IImageRepository imageRepository,
            IImageUploaderService imageUploader,
            ResponseHandler responseHandler,
            IMapper mapper)
        {
            _genreRepository = genreRepository;
            _imageRepository = imageRepository;
            _imageUploader = imageUploader;
            _responseHandler = responseHandler;
            _mapper = mapper;
        }
        #endregion

        #region Methods

        public async Task<Response<GenreDTO>> GetGenreByIdAsync(byte id)
        {
            if (id <= 0)
                return _responseHandler.BadRequest<GenreDTO>("Genre ID is invalid.");

            var genre = await _genreRepository.GetByIdAsync(id);
            if (genre == null)
                return _responseHandler.BadRequest<GenreDTO>("Genre does not exist.");

            var genreDto = _mapper.Map<GenreDTO>(genre);
            return _responseHandler.Success(genreDto, "Operation completed.");
        }

        public async Task<Response<GenreDTO>> GetGenreByNameAsync(string name)
        {
            if (name.IsNullOrEmpty())
                return _responseHandler.BadRequest<GenreDTO>("Genre name is invalid.");

            var genre = await _genreRepository.GetByNameAsync(name);
            if (genre == null)
                return _responseHandler.BadRequest<GenreDTO>("Genre does not exist.");

            var genreDto = _mapper.Map<GenreDTO>(genre);
            return _responseHandler.Success(genreDto, "Operation completed.");
        }

        public async Task<Response<IEnumerable<GenreDTO>>> GetAllAsync()
        {
            var genres = await _genreRepository.GetAllAsync();
            var genreDtos = _mapper.Map<IEnumerable<GenreDTO>>(genres);
            return _responseHandler.Success(genreDtos, "Operation completed.");
        }

        public async Task<Response<bool>> CreateGenreAsync(CreateGenreDTO dto)
        {
            if (dto == null)
                return _responseHandler.BadRequest<bool>("Genre data cannot be null.");

            await using var transaction = await _genreRepository.BeginTransactionAsync();

            try
            {
                var genre = _mapper.Map<Genre>(dto);
               

                var imageResult = await _imageUploader.UploadImageAsync(dto.Poster, ImageFolder.Genres);
                if (imageResult.Error != null)
                {
                    await transaction.RollbackAsync();
                    return _responseHandler.BadRequest<bool>(false, "Failed to upload image.");
                }

                var image = new Image
                {
                    IsPrimary = true,
                    Url = imageResult.Url.ToString()
                };

                var insertedImage =await _imageRepository.AddAsync(image);
                genre.PosterId = insertedImage.Id;
                await _genreRepository.AddAsync(genre);
                await transaction.CommitAsync();

                return _responseHandler.Success(true, "Genre created successfully.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                // TODO: Log the error (ex)
                return _responseHandler.BadRequest(false, "An error occurred while creating the genre.");
            }
        }

        public async Task<Response<bool>> UpdateGenreAsync(UpdateGenreDTO dto)
        {
            if (dto == null || dto.Id <= 0)
                return _responseHandler.BadRequest<bool>("Invalid genre data.");

            var existingGenre = await _genreRepository.GetByIdAsync(dto.Id, true);
            if (existingGenre == null)
                return _responseHandler.BadRequest<bool>("Genre not found.");

            await using var transaction = await _genreRepository.BeginTransactionAsync();

            try
            {
               
                _mapper.Map(dto, existingGenre);
                await _genreRepository.UpdateAsync(existingGenre);

                
                if (dto.Poster != null)
                {
                    var imageResult = await _imageUploader.UploadImageAsync(dto.Poster, ImageFolder.Genres);
                    if (imageResult.Error != null)
                    {
                        await transaction.RollbackAsync();
                        return _responseHandler.BadRequest<bool>(false, "Failed to upload new image.");
                    }
                    // Delete the iamge From Server
                    await _imageUploader.DeleteImageByUrlAsync(existingGenre.Image.Url);
                    // Delete existing  image for this genre from DB
                    await _imageRepository.DeleteAsync(existingGenre.Image);

                   

                    // Add the new image
                    var newImage = new Image
                    {
                        IsPrimary = true,
                        Url = imageResult.Url.ToString()
                    };
                    await _imageRepository.AddAsync(newImage);
                }

                await transaction.CommitAsync();
                return _responseHandler.Success(true, "Genre updated successfully.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return _responseHandler.BadRequest(false, "An error occurred while updating the genre.");
            }
        }


        public async Task<Response<bool>> DeleteAsync(byte id)
        {
            if (id <= 0)
                return _responseHandler.BadRequest<bool>("Genre ID is invalid.");

            var existingGenre = await _genreRepository.GetByIdAsync(id, true);
            if (existingGenre == null)
                return _responseHandler.BadRequest<bool>("Genre does not exist.");

            await _genreRepository.DeleteAsync(existingGenre);
            return _responseHandler.Success(true, "Genre deleted successfully.");
        }

        #endregion
    }
}
