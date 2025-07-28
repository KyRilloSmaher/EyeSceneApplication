using AutoMapper;
using EyeScenceApp.Application.Bases;
using EyeScenceApp.Application.DTOs.Awards.CelebirtyAwards;
using EyeScenceApp.Application.DTOs.Crew.Base;
using EyeScenceApp.Application.DTOs.Crew.Producers;
using EyeScenceApp.Application.DTOs.Crew.Producers;
using EyeScenceApp.Application.DTOs.DigitalContent;
using EyeScenceApp.Application.IServices;
using EyeScenceApp.Domain.Entities;
using EyeScenceApp.Domain.RepositoriesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.Services
{
    public class ProducerService : IProducerService
    {
        #region Feilds 
        private readonly IProducerRepository _producerRepository;
        private readonly IAwardRepository _awardRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IImageUploaderService _imageUploader;
        private readonly ResponseHandler _responseHandler;
        private IMapper _mapper;
        #endregion

        #region Constructor
        public ProducerService(IProducerRepository producerRepository, IAwardRepository awardRepository, IImageRepository imageRepository, IImageUploaderService imageUploader, ResponseHandler responseHandler, IMapper mapper)
        {
            _producerRepository = producerRepository;
            _awardRepository = awardRepository;
            _imageRepository = imageRepository;
            _imageUploader = imageUploader;
            _responseHandler = responseHandler;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task<Response<ProducerDTO?>> GetProducerByIdAsync(Guid ProducerId)
        {
            if (ProducerId == Guid.Empty)
            {
                return _responseHandler.BadRequest<ProducerDTO?>("Producer ID cannot be empty.");
            }

            var Producer = await _producerRepository.GetByIdAsync(ProducerId, false);
            if (Producer is null)
            {
                return _responseHandler.NotFound<ProducerDTO?>($"Producer with ID {ProducerId} not found.");
            }

            var ProducerDto = _mapper.Map<ProducerDTO>(Producer);

            return _responseHandler.Success<ProducerDTO?>(ProducerDto);
        }

        public async Task<Response<ICollection<ProducerDTO>>> GetAllProducersAsync()
        {
            var Producers = await _producerRepository.GetAllAsync();
            if (Producers is null)
            {
                return _responseHandler.NotFound<ICollection<ProducerDTO>>($"No Producers found.");
            }

            var ProducersDTOs = _mapper.Map<ICollection<ProducerDTO>>(Producers);
            return _responseHandler.Success<ICollection<ProducerDTO>>(ProducersDTOs);
        }

        public async Task<Response<ICollection<ProducerDTO>>> FilterProducerAsync(FilterDTO dto)
        {
            if (dto == null)
            {
                return _responseHandler.BadRequest<ICollection<ProducerDTO>>("Filter DTO cannot be null.");
            }

            Expression<Func<Producer, bool>> searchPredicate = Producer =>
                (string.IsNullOrEmpty(dto.Name) ||
                 (Producer.FirstName != null && Producer.FirstName.Contains(dto.Name)) ||
                 (Producer.MiddleName != null && Producer.MiddleName.Contains(dto.Name)) ||
                 (Producer.LastName != null && Producer.LastName.Contains(dto.Name)))
                &&
                (dto.sex == null || Producer.Sex == dto.sex)
                &&
                (dto.nationality == null || Producer.Nationality == dto.nationality);

            Expression<Func<Producer, string>> orderBy = Producer => Producer.FirstName;

            bool ascending = dto.ascendenig;

            var ProducerEntities = await _producerRepository.FilterListAsync(orderBy, searchPredicate, ascending);

            if (ProducerEntities == null)
            {
                return _responseHandler.NotFound<ICollection<ProducerDTO>>("No Producers found matching the criteria.");
            }

            var ProducerDTOs = _mapper.Map<ICollection<ProducerDTO>>(ProducerEntities);

            return ProducerDTOs == null
                ? _responseHandler.UnprocessableEntity<ICollection<ProducerDTO>>("Error mapping Producers.")
                : _responseHandler.Success(ProducerDTOs);
        }

        public async Task<Response<ICollection<CelebirtyAwardDTO>>> GetProducerAwardsAsync(Guid ProducerId)
        {
            if (ProducerId == Guid.Empty)
            {
                return _responseHandler.BadRequest<ICollection<CelebirtyAwardDTO>>("Producer ID cannot be empty.");
            }

            var Producer = await _producerRepository.GetByIdAsync(ProducerId, false);
            if (Producer is null)
            {
                return _responseHandler.NotFound<ICollection<CelebirtyAwardDTO>>($"Producer with ID {ProducerId} not found.");
            }

            var awards = await _awardRepository.GetAwardsByCelebrityAsync(Producer.Id);
            if (awards is null)
            {
                return _responseHandler.NotFound<ICollection<CelebirtyAwardDTO>>($"No awards found for Producer with ID {ProducerId}.");
            }

            var awardDtos = _mapper.Map<ICollection<CelebirtyAwardDTO>>(awards);
            return _responseHandler.Success<ICollection<CelebirtyAwardDTO>>(awardDtos);
        }

        public async Task<Response<ICollection<DigitalContentDTO>>> GetProducerWorksAsync(Guid ProducerId)
        {
            if (ProducerId == Guid.Empty)
            {
                return _responseHandler.BadRequest<ICollection<DigitalContentDTO>>("Producer ID cannot be empty.");
            }
            var Producer = await _producerRepository.GetByIdAsync(ProducerId, false);
            if (Producer is null)
            {
                return _responseHandler.NotFound<ICollection<DigitalContentDTO>>($"Producer with ID {ProducerId} not found.");
            }
            var works = await _producerRepository.GetAllWorksAsync(Producer.Id);
            if (works is null)
            {
                return _responseHandler.NotFound<ICollection<DigitalContentDTO>>($"No works found for Producer with ID {ProducerId}.");
            }
            var workDtos = _mapper.Map<ICollection<DigitalContentDTO>>(works);
            return _responseHandler.Success<ICollection<DigitalContentDTO>>(workDtos);
        }



        public async Task<Response<bool>> UpdateProducerAsync(UpdateProducerDTO ProducerDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<bool>> CreateProducerAsync(CreateProducerDTO ProducerDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<bool>> DeleteProducerAsync(Guid ProducerId)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
