using AutoMapper;
using EyeScenceApp.Application.Bases;
using EyeScenceApp.Application.DTOs.DigitalContent;
using EyeScenceApp.Application.DTOs.Favorites;
using EyeScenceApp.Application.DTOs.WatchList;
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
    public class WatchListService : IWatchListService
    {
        #region Field(s)
        private readonly IWatchListRepository _watchListRepository;
        private readonly IApplicationUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ResponseHandler _responseHandler;


        #endregion

        #region Constructor(s)
        public WatchListService(IWatchListRepository watchListRepository, IApplicationUserRepository userRepository, IMapper mapper, ResponseHandler responseHandler)
        {
            _watchListRepository = watchListRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _responseHandler = responseHandler;
        }
        #endregion

        #region Method(s)
        public async Task<Response<bool>> AddToWatchListAsync(AddDigitalContentToUserWatchListDTO dto)
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
            var isInWatchList = await _watchListRepository.IsInWatchListAsync(dto.UserId, dto.DigitalContentId);
            if (isInWatchList)
            {
                return _responseHandler.BadRequest<bool>("This digital content is already in watch list.");
            }
            var watchList = _mapper.Map<WatchList>(dto);
            await _watchListRepository.AddAsync(watchList);
            return _responseHandler.Success(true, "Digital content added to watch list successfully.");
        }

        public async Task<Response<bool>> ClearWatchListAsync(string userId)
        {
            if (userId == null) {
                return _responseHandler.BadRequest<bool>("User ID is invalid.");
            }
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return _responseHandler.BadRequest<bool>("User does not exist.");
            }
            var isCleared = await _watchListRepository.ClearWatchListAsync(userId);
            if (!isCleared)
            {
                return _responseHandler.BadRequest<bool>("Failed to clear watch list.");
            }
            return _responseHandler.Success(true, "Watch list cleared successfully.");
        }

        public async Task<Response<bool>> DeleteFromWatchListAsync(DeleteDigitalContentFromUserWatchListDTO dto)
        {
            if (dto == null) {
                return _responseHandler.BadRequest<bool>("DTO cannot be null.");
            }
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
            var isInWatchList = await _watchListRepository.IsInWatchListAsync(dto.UserId, dto.DigitalContentId);

            if (!isInWatchList)
            {
                return _responseHandler.BadRequest<bool>("This digital content is not in watch list.");
            }
            var watchList = _mapper.Map<WatchList>(dto);
            await _watchListRepository.DeleteAsync(watchList);
            return _responseHandler.Success(true, "Digital content removed from watch list successfully.");
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
            var watchList = await _watchListRepository.GetWatchListByUserIdAsync(userId);
            if (watchList == null)
            {
                return _responseHandler.NotFound<ICollection<DigitalContentDTO>>("No digital content found in watch list.");
            }
            var digitalContentDtos = _mapper.Map<ICollection<DigitalContentDTO>>(watchList.Select(w=>w.DigitalContent));
            return _responseHandler.Success(digitalContentDtos, "Watch list retrieved successfully.");


        }

        public async Task<Response<bool>> IsInWatchListAsync(IsDigitalContentInUserWatchListDTO dto)
        {
            if (dto == null) {
                return _responseHandler.BadRequest<bool>("DTO cannot be null.");
            }
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
            var isInWatchList = await _watchListRepository.IsInWatchListAsync(dto.UserId, dto.DigitalContentId);
            if (!isInWatchList)
            {
                return _responseHandler.Success(false, "Digital content is not in watch list.");
            }
            return _responseHandler.Success(true, "Digital content is in watch list.");
        }

        #endregion

    }
}
