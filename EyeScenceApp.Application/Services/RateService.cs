using AutoMapper;
using EyeScenceApp.Application.Bases;
using EyeScenceApp.Application.DTOs.DigitalContent;
using EyeScenceApp.Application.DTOs.Rates;
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
    public class RateService :IRateService
    {
        #region Field(s)
        private readonly IRatingRepository _ratingRepository;
        private readonly IApplicationUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ResponseHandler _responseHandler;
        #endregion

        #region Constructor(s)
        public RateService(IRatingRepository ratingRepository, IApplicationUserRepository userRepository, IMapper mapper, ResponseHandler responseHandler)
        {
            _ratingRepository = ratingRepository ?? throw new ArgumentNullException(nameof(ratingRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _responseHandler = responseHandler ?? throw new ArgumentNullException(nameof(responseHandler));
        }
        #endregion
        #region Method(s)
        public async Task<Response<RateDTO>> GetRateByIdAsync(Guid rateId)
        {
            if (rateId == Guid.Empty)
            {
                return _responseHandler.BadRequest<RateDTO>("Rate ID is invalid.");
            }

            var rate = await _ratingRepository.GetByIdAsync(rateId);
            if (rate == null)
            {
                return _responseHandler.NotFound<RateDTO>("Rate not found.");
            }

            var rateDto = _mapper.Map<RateDTO>(rate);
            return _responseHandler.Success(rateDto);
        }

        public async Task<Response<ICollection<RateDTO>>> GetAllRatesByUserAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return _responseHandler.BadRequest<ICollection<RateDTO>>("User ID is invalid.");
            }

            var rates = await _ratingRepository.GetAllRatesByUserAsync(userId);
            if (rates == null)
            {
                return _responseHandler.NotFound<ICollection<RateDTO>>("No rates found for this user.");
            }

            var rateDtos = _mapper.Map<ICollection<RateDTO>>(rates);
            return _responseHandler.Success(rateDtos);
        }

        public async Task<Response<ICollection<RateDTO>>> GetAllRatesOfDigitalContentAsync(Guid DigitalContentId)
        {
            if (DigitalContentId == Guid.Empty)
            {
                return _responseHandler.BadRequest<ICollection<RateDTO>>("Digital Content ID is invalid.");
            }

            var rates = await _ratingRepository.GetAllRatesOfDigitalContentAsync(DigitalContentId);
            if (rates == null )
            {
                return _responseHandler.NotFound<ICollection<RateDTO>>("No rates found for this digital content.");
            }

            var rateDtos = _mapper.Map<ICollection<RateDTO>>(rates);
            return _responseHandler.Success(rateDtos);
        }

        public async Task<Response<ICollection<DigitalContentDTO>>> GetTopRatedDigitalContentAsync()
        {
            var topRatedDigitalContents = await _ratingRepository.GetTopRatedDigitalContentsAsync();
            if (topRatedDigitalContents == null || !topRatedDigitalContents.Any())
            {
                return _responseHandler.NotFound<ICollection<DigitalContentDTO>>("No top-rated digital contents found.");
            }

            var rateDtos = _mapper.Map<ICollection<DigitalContentDTO>>(topRatedDigitalContents);
            return _responseHandler.Success(rateDtos);
        }
        public async Task<Response<ICollection<DigitalContentDTO>>> GetTopRatedDigitalContentByGenreNameAsync(string name)
        {
            var topRatedDigitalContents = await _ratingRepository.GetTopRatedDigitalContentsByGenreAsync(name);
            if (topRatedDigitalContents == null || !topRatedDigitalContents.Any())
            {
                return _responseHandler.NotFound<ICollection<DigitalContentDTO>>("No top-rated digital contents found.");
            }

            var rateDtos = _mapper.Map<ICollection<DigitalContentDTO>>(topRatedDigitalContents);
            return _responseHandler.Success(rateDtos);
        }

        public async Task<Response<RateDTO>> CreateRateAsync(CreateRateDTO rateDto)
        {
            if (rateDto == null)
            {
                return _responseHandler.BadRequest<RateDTO>("Rate DTO cannot be null.");
            }

            if (string.IsNullOrEmpty(rateDto.UserId))
            {
                return _responseHandler.BadRequest<RateDTO>("User ID is invalid.");
            }

            var user = await _userRepository.GetByIdAsync(rateDto.UserId);
            if (user == null)
            {
                return _responseHandler.BadRequest<RateDTO>("User does not exist.");
            }

            var rate = _mapper.Map<Rate>(rateDto);
            await _ratingRepository.AddAsync(rate);

            var createdRateDto = _mapper.Map<RateDTO>(rate);
            createdRateDto.UserName = user.UserName;
            return _responseHandler.Created(createdRateDto, "Rate created successfully.");
        }

        public async Task<Response<RateDTO>> UpdateRateAsync(UpdateRateDTO rateDto)
        {
            if (rateDto == null)
            {
                return _responseHandler.BadRequest<RateDTO>("Rate DTO cannot be null.");
            }

            if (rateDto.Id == Guid.Empty)
            {
                return _responseHandler.BadRequest<RateDTO>("Rate ID is invalid.");
            }

            var existingRate = await _ratingRepository.GetByIdAsync(rateDto.Id);
            if (existingRate == null)
            {
                return _responseHandler.NotFound<RateDTO>("Rate not found.");
            }

            var updatedRate = _mapper.Map(rateDto, existingRate);
            await _ratingRepository.UpdateAsync(updatedRate);

            var updatedRateDto = _mapper.Map<RateDTO>(updatedRate);
            return _responseHandler.Success(updatedRateDto, "Rate updated successfully.");
        }

        public async Task<Response<bool>> DeleteRateAsync(Guid rateId)
        {
            if (rateId == Guid.Empty) {
                return _responseHandler.BadRequest<bool>("Rate ID is invalid.");
            }
            var rate = await _ratingRepository.GetByIdAsync(rateId);
            if (rate == null)
            {
                return _responseHandler.NotFound<bool>("Rate not found.");
            }
                await _ratingRepository.DeleteAsync(rate);
            
                return _responseHandler.Success<bool>(true,"Rate deleted Successfully.");
           
        }

        public async Task<Response<bool>> IncreamentRateLikeAsync(Guid rateId)
        {
            if (rateId == Guid.Empty) {
                return _responseHandler.BadRequest<bool>("Rate ID is invalid.");
            }
            var rate = await _ratingRepository.GetByIdAsync(rateId);
            if (rate == null)
            {
                return _responseHandler.NotFound<bool>("Rate not found.");
            }
             var result = await _ratingRepository.IncreamentRateLikeAsync(rateId);
            if (!result)
            {
                return _responseHandler.Failed<bool>("Failed to increment rate like.");
            }
            return _responseHandler.Success(true, "Rate like incremented successfully.");
        }

        public async Task<Response<bool>> DecreamentRateLikeAsync(Guid rateId)
        {
            if (rateId == Guid.Empty)
            {
                return _responseHandler.BadRequest<bool>("Rate ID is invalid.");
            }
            var rate = await _ratingRepository.GetByIdAsync(rateId);
            if (rate == null)
            {
                return _responseHandler.NotFound<bool>("Rate not found.");
            }
            var result = await _ratingRepository.DecreamentRateLikeAsync(rateId);
            if (!result)
            {
                return _responseHandler.Failed<bool>("Failed to decrement rate like.");
            }
            return _responseHandler.Success(true, "Rate like decremented successfully.");
        }

        public async Task<Response<bool>> IncreamentRateDisLikeAsync(Guid rateId)
        {
            if (rateId == Guid.Empty)
            {
                return _responseHandler.BadRequest<bool>("Rate ID is invalid.");
            }
            var rate = await _ratingRepository.GetByIdAsync(rateId);
            if (rate == null)
            {
                return _responseHandler.NotFound<bool>("Rate not found.");
            }
            var result = await _ratingRepository.IncreamentRateDisLikeAsync(rateId);
            if (!result)
            {
                return _responseHandler.Failed<bool>("Failed to increment rate dislike.");
            }
            return _responseHandler.Success(true, "Rate dislike incremented successfully.");
        }

        public async Task<Response<bool>> DecreamentRateDisLikeAsync(Guid rateId)
        {
            if (rateId == Guid.Empty) {
                return _responseHandler.BadRequest<bool>("Rate ID is invalid.");
            }
            var rate = await _ratingRepository.GetByIdAsync(rateId);
            if (rate == null)
            {
                return _responseHandler.NotFound<bool>("Rate not found.");
            }
            var result = await _ratingRepository.DecreamentRateDisLikeAsync(rateId);
            if (!result)
            {
                return _responseHandler.Failed<bool>("Failed to decrement rate dislike.");
            }
            return _responseHandler.Success(true, "Rate dislike decremented successfully.");
        }
        #endregion
    }
}
