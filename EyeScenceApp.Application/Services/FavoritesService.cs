using AutoMapper;
using EyeScenceApp.Application.Bases;
using EyeScenceApp.Application.DTOs.DigitalContent;
using EyeScenceApp.Application.DTOs.Favorites;
using EyeScenceApp.Application.IServices;
using EyeScenceApp.Domain.Entities;
using EyeScenceApp.Domain.RepositoriesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.Services
{
    public class FavoritesService : IFavoritesService
    {

        #region Field(s)
        private readonly IFavoriteRepository _favoritesRepository;
        private readonly IApplicationUserRepository _userRepository;
        private readonly ResponseHandler _responseHandler;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor(s)
        public FavoritesService(IFavoriteRepository favoritesRepository, IApplicationUserRepository userRepository, IMapper mapper, ResponseHandler responseHandler)
        {
            _favoritesRepository = favoritesRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _responseHandler = responseHandler;
        }
        #endregion

        #region Method(s)
        public async Task<Response<bool>> AddFavoriteAsync(AddDigitalContentToUserFavoriteListDTO dto)
        {
            if (Guid.Empty == dto.DigitalContentId)
            {
                return _responseHandler.BadRequest<bool>("Digital Content ID is invalid.");

            }
            if (string.IsNullOrEmpty(dto.UserId))
            {
                return _responseHandler.BadRequest<bool>("User ID is invalid.");
            }
            var user = await _userRepository.GetByIdAsync(dto.UserId);
            if (user == null)
            {
                return _responseHandler.BadRequest<bool>("User does not exist.");
            }
            var isFavorite = await _favoritesRepository.IsFavoriteAsync(dto.UserId, dto.DigitalContentId);
            if (isFavorite)
            {
                return _responseHandler.BadRequest<bool>("This digital content is already in favorites.");
            }
            var favorite = _mapper.Map<Favorite>(dto);

            var result = await _favoritesRepository.AddAsync(favorite);
            if (result == null)
            {
                return _responseHandler.BadRequest<bool>("Failed to add favorite.");
            }
            return _responseHandler.Created(true, "Digital content added to favorites successfully.");

        }

        public async Task<Response<bool>> ClearFavoritesAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return _responseHandler.BadRequest<bool>("User ID is invalid.");

            }
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return _responseHandler.BadRequest<bool>("User does not exist.");
            }
            var result = await _favoritesRepository.ClearFavoritesAsync(userId);
            if (!result)
            {
                return _responseHandler.BadRequest<bool>("Failed to clear favorites.");
            }
            return _responseHandler.Deleted(true, "Favorites cleared successfully.");
        }

        public async Task<Response<bool>> DeleteFavoriteAsync(DeleteDigitalContentToUserFavoriteListDTO dto)
        {
            if (Guid.Empty == dto.DigitalContentId)
            {
                return _responseHandler.BadRequest<bool>("Digital Content ID is invalid.");

            }
            if (string.IsNullOrEmpty(dto.UserId))
            {
                return _responseHandler.BadRequest<bool>("User ID is invalid.");
            }
            var user = await _userRepository.GetByIdAsync(dto.UserId);
            if (user == null)
            {
                return _responseHandler.BadRequest<bool>("User does not exist.");
            }
            var isFavorite = await _favoritesRepository.IsFavoriteAsync(dto.UserId, dto.DigitalContentId);
            if (!isFavorite)
            {
                return _responseHandler.BadRequest<bool>("This digital content is already Not in favorites.");
            }
            var favorite = _mapper.Map<Favorite>(dto);

             await _favoritesRepository.DeleteAsync(favorite);
             return _responseHandler.Deleted<bool>(true,"Digital content Deleted from favorites successfully.");
        }

            public async Task<Response<ICollection<DigitalContentDTO>>> GetFavoritesOfUserAsync(string userId)
            {
                if (string.IsNullOrEmpty(userId))
                {
                    return _responseHandler.BadRequest<ICollection<DigitalContentDTO>>("User ID is invalid.");
                }
                var user = await _userRepository.GetByIdAsync(userId);
                if (user == null)
                {
                    return _responseHandler.BadRequest<ICollection<DigitalContentDTO>>("User does not exist.");
                }
                var favorites = await _favoritesRepository.GetFavoritesByUserIdAsync(userId);
                if (favorites == null)
                {
                    return _responseHandler.BadRequest<ICollection<DigitalContentDTO>>("No favorites found for this user.");
                }
                var digitalContentDtos = _mapper.Map<ICollection<DigitalContentDTO>>(favorites.Select(f => f.DigitalContent));
                 return _responseHandler.Success(digitalContentDtos, "Favorites retrieved successfully.");

            }

        public async Task<Response<bool>> IsFavoriteAsync(IsDigitalContentToUserFavoriteListDTO dto)
        {
            if (Guid.Empty == dto.DigitalContentId)
            {
                return _responseHandler.BadRequest<bool>("Digital Content ID is invalid.");
            }
            if (string.IsNullOrEmpty(dto.UserId))
            {
                return _responseHandler.BadRequest<bool>("User ID is invalid.");
            }
            var user = await _userRepository.GetByIdAsync(dto.UserId);
            if (user == null)
            {
                return _responseHandler.BadRequest<bool>("User does not exist.");
            }
            var isFavorite = await _favoritesRepository.IsFavoriteAsync(dto.UserId, dto.DigitalContentId);
            if (isFavorite)
            {
                return _responseHandler.Success(true, "This digital content is in favorites.");
            }
            return _responseHandler.Success(false, "This digital content is not in favorites.");
        }


            #endregion
        }
}
