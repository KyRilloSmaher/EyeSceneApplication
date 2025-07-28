using AutoMapper;
using EyeScenceApp.Application.Bases;
using EyeScenceApp.Application.DTOs.Awards.CelebirtyAwards;
using EyeScenceApp.Application.DTOs.Crew.Base;
using EyeScenceApp.Application.DTOs.Crew.Writers;
using EyeScenceApp.Application.DTOs.Crew.Writers;
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
    public class WriterService : IWriterService
    {
        #region Feilds 
        private readonly IWriterRepository _WriterRepository;
        private readonly IAwardRepository _awardRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IImageUploaderService _imageUploader;
        private readonly ResponseHandler _responseHandler;
        private IMapper _mapper;
        #endregion

        #region Constructor
        public WriterService(IWriterRepository WriterRepository, IAwardRepository awardRepository, IImageRepository imageRepository, IImageUploaderService imageUploader, ResponseHandler responseHandler, IMapper mapper)
        {
            _WriterRepository = WriterRepository;
            _awardRepository = awardRepository;
            _imageRepository = imageRepository;
            _imageUploader = imageUploader;
            _responseHandler = responseHandler;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task<Response<WriterDTO?>> GetWriterByIdAsync(Guid WriterId)
        {
            if (WriterId == Guid.Empty)
            {
                return _responseHandler.BadRequest<WriterDTO?>("Writer ID cannot be empty.");
            }

            var Writer = await _WriterRepository.GetByIdAsync(WriterId, false);
            if (Writer is null)
            {
                return _responseHandler.NotFound<WriterDTO?>($"Writer with ID {WriterId} not found.");
            }

            var WriterDto = _mapper.Map<WriterDTO>(Writer);

            return _responseHandler.Success<WriterDTO?>(WriterDto);
        }

        public async Task<Response<ICollection<WriterDTO>>> GetAllWritersAsync()
        {
            var Writers = await _WriterRepository.GetAllAsync();
            if (Writers is null)
            {
                return _responseHandler.NotFound<ICollection<WriterDTO>>($"No Writers found.");
            }

            var WritersDTOs = _mapper.Map<ICollection<WriterDTO>>(Writers);
            return _responseHandler.Success<ICollection<WriterDTO>>(WritersDTOs);
        }

        public async Task<Response<ICollection<WriterDTO>>> FilterWriterAsync(FilterDTO dto)
        {
            if (dto == null)
            {
                return _responseHandler.BadRequest<ICollection<WriterDTO>>("Filter DTO cannot be null.");
            }

            Expression<Func<Writer, bool>> searchPredicate = Writer =>
                (string.IsNullOrEmpty(dto.Name) ||
                 (Writer.FirstName != null && Writer.FirstName.Contains(dto.Name)) ||
                 (Writer.MiddleName != null && Writer.MiddleName.Contains(dto.Name)) ||
                 (Writer.LastName != null && Writer.LastName.Contains(dto.Name)))
                &&
                (dto.sex == null || Writer.Sex == dto.sex)
                &&
                (dto.nationality == null || Writer.Nationality == dto.nationality);

            Expression<Func<Writer, string>> orderBy = Writer => Writer.FirstName;

            bool ascending = dto.ascendenig;

            var WriterEntities = await _WriterRepository.FilterListAsync(orderBy, searchPredicate, ascending);

            if (WriterEntities == null)
            {
                return _responseHandler.NotFound<ICollection<WriterDTO>>("No Writers found matching the criteria.");
            }

            var WriterDTOs = _mapper.Map<ICollection<WriterDTO>>(WriterEntities);

            return WriterDTOs == null
                ? _responseHandler.UnprocessableEntity<ICollection<WriterDTO>>("Error mapping Writers.")
                : _responseHandler.Success(WriterDTOs);
        }

        public async Task<Response<ICollection<CelebirtyAwardDTO>>> GetWriterAwardsAsync(Guid WriterId)
        {
            if (WriterId == Guid.Empty)
            {
                return _responseHandler.BadRequest<ICollection<CelebirtyAwardDTO>>("Writer ID cannot be empty.");
            }

            var Writer = await _WriterRepository.GetByIdAsync(WriterId, false);
            if (Writer is null)
            {
                return _responseHandler.NotFound<ICollection<CelebirtyAwardDTO>>($"Writer with ID {WriterId} not found.");
            }

            var awards = await _awardRepository.GetAwardsByCelebrityAsync(Writer.Id);
            if (awards is null)
            {
                return _responseHandler.NotFound<ICollection<CelebirtyAwardDTO>>($"No awards found for Writer with ID {WriterId}.");
            }

            var awardDtos = _mapper.Map<ICollection<CelebirtyAwardDTO>>(awards);
            return _responseHandler.Success<ICollection<CelebirtyAwardDTO>>(awardDtos);
        }

        public async Task<Response<ICollection<DigitalContentDTO>>> GetWriterWorksAsync(Guid WriterId)
        {
            if (WriterId == Guid.Empty)
            {
                return _responseHandler.BadRequest<ICollection<DigitalContentDTO>>("Writer ID cannot be empty.");
            }
            var Writer = await _WriterRepository.GetByIdAsync(WriterId, false);
            if (Writer is null)
            {
                return _responseHandler.NotFound<ICollection<DigitalContentDTO>>($"Writer with ID {WriterId} not found.");
            }
            var works = await _WriterRepository.GetAllWorksAsync(Writer.Id);
            if (works is null)
            {
                return _responseHandler.NotFound<ICollection<DigitalContentDTO>>($"No works found for Writer with ID {WriterId}.");
            }
            var workDtos = _mapper.Map<ICollection<DigitalContentDTO>>(works);
            return _responseHandler.Success<ICollection<DigitalContentDTO>>(workDtos);
        }



        public async Task<Response<bool>> UpdateWriterAsync(UpdateWriterDTO WriterDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<bool>> CreateWriterAsync(CreateWriterDTO WriterDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<bool>> DeleteWriterAsync(Guid WriterId)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
