using EyeScenceApp.Application.Bases;
using EyeScenceApp.Application.DTOs.DigitalContent;
using EyeScenceApp.Application.DTOs.Rates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.IServices
{
    public interface IRateService
    {
        public Task<Response<RateDTO>> GetRateByIdAsync(Guid rateId);
        public Task<Response<ICollection<RateDTO>>> GetAllRatesByUserAsync(string userId);
        public Task<Response<ICollection<RateDTO>>> GetAllRatesOfDigitalContentAsync(Guid DigitalContentId);
        public Task<Response<ICollection<DigitalContentDTO>>> GetTopRatedDigitalContentAsync();
        public Task<Response<ICollection<DigitalContentDTO>>> GetTopRatedDigitalContentByGenreNameAsync(string name);
        public Task<Response<RateDTO>> CreateRateAsync(CreateRateDTO rateDto);
        public Task<Response<RateDTO>> UpdateRateAsync(UpdateRateDTO rateDto);
        public Task<Response<bool>> DeleteRateAsync(Guid rateId);
        public Task<Response<bool>> IncreamentRateLikeAsync(Guid rateId);
        public Task<Response<bool>> DecreamentRateLikeAsync(Guid rateId);
        public Task<Response<bool>> IncreamentRateDisLikeAsync(Guid rateId);
        public Task<Response<bool>> DecreamentRateDisLikeAsync(Guid rateId);


    }
}
