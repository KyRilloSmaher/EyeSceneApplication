using EyeScenceApp.Application.Bases;
using EyeScenceApp.Application.DTOs.Awards.CelebirtyAwards;
using EyeScenceApp.Application.DTOs.Crew.Base;
using EyeScenceApp.Application.DTOs.Crew.Producers;
using EyeScenceApp.Application.DTOs.DigitalContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.IServices
{
    public interface IProducerService
    {
        public Task<Response<ProducerDTO?>> GetProducerByIdAsync(Guid ProducerId);
        public Task<Response<ICollection<ProducerDTO>>> GetAllProducersAsync();
        public Task<Response<ICollection<ProducerDTO>>> FilterProducerAsync(FilterDTO dto);
        public Task<Response<ICollection<CelebirtyAwardDTO>>> GetProducerAwardsAsync(Guid ProducerId);
        public Task<Response<ICollection<DigitalContentDTO>>> GetProducerWorksAsync(Guid ProducerId);
        public Task<Response<bool>> UpdateProducerAsync(UpdateProducerDTO ProducerDTO);
        public Task<Response<bool>> CreateProducerAsync(CreateProducerDTO ProducerDTO);
        public Task<Response<bool>> DeleteProducerAsync(Guid ProducerId);
    }
}
