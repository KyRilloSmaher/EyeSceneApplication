using AutoMapper;
using EyeScenceApp.Application.Bases;
using EyeScenceApp.Application.DTOs.Awards.CelebirtyAwards;
using EyeScenceApp.Application.DTOs.Crew.Base;
using EyeScenceApp.Application.DTOs.Crew.SoundDesigners;
using EyeScenceApp.Application.DTOs.Crew.SoundDesigners;
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
    public class SoundDesignerService : ISoundDesignerService
    {
        #region Feilds 
        private readonly ISoundDeignerRepository _SoundDesignerRepository;
        private readonly IAwardRepository _awardRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IImageUploaderService _imageUploader;
        private readonly ResponseHandler _responseHandler;
        private IMapper _mapper;
        #endregion

        #region Constructor
        public SoundDesignerService(ISoundDeignerRepository SoundDesignerRepository, IAwardRepository awardRepository, IImageRepository imageRepository, IImageUploaderService imageUploader, ResponseHandler responseHandler, IMapper mapper)
        {
            _SoundDesignerRepository = SoundDesignerRepository;
            _awardRepository = awardRepository;
            _imageRepository = imageRepository;
            _imageUploader = imageUploader;
            _responseHandler = responseHandler;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task<Response<SoundDesignerDTO?>> GetSoundDesignerByIdAsync(Guid SoundDesignerId)
        {
            if (SoundDesignerId == Guid.Empty)
            {
                return _responseHandler.BadRequest<SoundDesignerDTO?>("SoundDesigner ID cannot be empty.");
            }

            var SoundDesigner = await _SoundDesignerRepository.GetByIdAsync(SoundDesignerId, false);
            if (SoundDesigner is null)
            {
                return _responseHandler.NotFound<SoundDesignerDTO?>($"SoundDesigner with ID {SoundDesignerId} not found.");
            }

            var SoundDesignerDto = _mapper.Map<SoundDesignerDTO>(SoundDesigner);

            return _responseHandler.Success<SoundDesignerDTO?>(SoundDesignerDto);
        }

        public async Task<Response<ICollection<SoundDesignerDTO>>> GetAllSoundDesignersAsync()
        {
            var SoundDesigners = await _SoundDesignerRepository.GetAllAsync();
            if (SoundDesigners is null)
            {
                return _responseHandler.NotFound<ICollection<SoundDesignerDTO>>($"No SoundDesigners found.");
            }

            var SoundDesignersDTOs = _mapper.Map<ICollection<SoundDesignerDTO>>(SoundDesigners);
            return _responseHandler.Success<ICollection<SoundDesignerDTO>>(SoundDesignersDTOs);
        }

        public async Task<Response<ICollection<SoundDesignerDTO>>> FilterSoundDesignerAsync(FilterDTO dto)
        {
            if (dto == null)
            {
                return _responseHandler.BadRequest<ICollection<SoundDesignerDTO>>("Filter DTO cannot be null.");
            }

            Expression<Func<SoundDesigner, bool>> searchPredicate = SoundDesigner =>
                (string.IsNullOrEmpty(dto.Name) ||
                 (SoundDesigner.FirstName != null && SoundDesigner.FirstName.Contains(dto.Name)) ||
                 (SoundDesigner.MiddleName != null && SoundDesigner.MiddleName.Contains(dto.Name)) ||
                 (SoundDesigner.LastName != null && SoundDesigner.LastName.Contains(dto.Name)))
                &&
                (dto.sex == null || SoundDesigner.Sex == dto.sex)
                &&
                (dto.nationality == null || SoundDesigner.Nationality == dto.nationality);

            Expression<Func<SoundDesigner, string>> orderBy = SoundDesigner => SoundDesigner.FirstName;

            bool ascending = dto.ascendenig;

            var SoundDesignerEntities = await _SoundDesignerRepository.FilterListAsync(orderBy, searchPredicate, ascending);

            if (SoundDesignerEntities == null)
            {
                return _responseHandler.NotFound<ICollection<SoundDesignerDTO>>("No SoundDesigners found matching the criteria.");
            }

            var SoundDesignerDTOs = _mapper.Map<ICollection<SoundDesignerDTO>>(SoundDesignerEntities);

            return SoundDesignerDTOs == null
                ? _responseHandler.UnprocessableEntity<ICollection<SoundDesignerDTO>>("Error mapping SoundDesigners.")
                : _responseHandler.Success(SoundDesignerDTOs);
        }

        public async Task<Response<ICollection<CelebirtyAwardDTO>>> GetSoundDesignerAwardsAsync(Guid SoundDesignerId)
        {
            if (SoundDesignerId == Guid.Empty)
            {
                return _responseHandler.BadRequest<ICollection<CelebirtyAwardDTO>>("SoundDesigner ID cannot be empty.");
            }

            var SoundDesigner = await _SoundDesignerRepository.GetByIdAsync(SoundDesignerId, false);
            if (SoundDesigner is null)
            {
                return _responseHandler.NotFound<ICollection<CelebirtyAwardDTO>>($"SoundDesigner with ID {SoundDesignerId} not found.");
            }

            var awards = await _awardRepository.GetAwardsByCelebrityAsync(SoundDesigner.Id);
            if (awards is null)
            {
                return _responseHandler.NotFound<ICollection<CelebirtyAwardDTO>>($"No awards found for SoundDesigner with ID {SoundDesignerId}.");
            }

            var awardDtos = _mapper.Map<ICollection<CelebirtyAwardDTO>>(awards);
            return _responseHandler.Success<ICollection<CelebirtyAwardDTO>>(awardDtos);
        }

        public async Task<Response<ICollection<DigitalContentDTO>>> GetSoundDesignerWorksAsync(Guid SoundDesignerId)
        {
            if (SoundDesignerId == Guid.Empty)
            {
                return _responseHandler.BadRequest<ICollection<DigitalContentDTO>>("SoundDesigner ID cannot be empty.");
            }
            var SoundDesigner = await _SoundDesignerRepository.GetByIdAsync(SoundDesignerId, false);
            if (SoundDesigner is null)
            {
                return _responseHandler.NotFound<ICollection<DigitalContentDTO>>($"SoundDesigner with ID {SoundDesignerId} not found.");
            }
            var works = await _SoundDesignerRepository.GetAllWorksAsync(SoundDesigner.Id);
            if (works is null)
            {
                return _responseHandler.NotFound<ICollection<DigitalContentDTO>>($"No works found for SoundDesigner with ID {SoundDesignerId}.");
            }
            var workDtos = _mapper.Map<ICollection<DigitalContentDTO>>(works);
            return _responseHandler.Success<ICollection<DigitalContentDTO>>(workDtos);
        }



        public async Task<Response<bool>> UpdateSoundDesignerAsync(UpdateSoundDesignerDTO SoundDesignerDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<bool>> CreateSoundDesignerAsync(CreateSoundDesignerDTO SoundDesignerDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<bool>> DeleteSoundDesignerAsync(Guid SoundDesignerId)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
