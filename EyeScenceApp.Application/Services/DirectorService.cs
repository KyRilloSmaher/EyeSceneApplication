using AutoMapper;
using EyeScenceApp.Application.Bases;
using EyeScenceApp.Application.DTOs.Awards.CelebirtyAwards;
using EyeScenceApp.Application.DTOs.Crew.Base;
using EyeScenceApp.Application.DTOs.Crew.Directors;
using EyeScenceApp.Application.DTOs.DigitalContent;
using EyeScenceApp.Application.IServices;
using EyeScenceApp.Domain.Entities;
using EyeScenceApp.Domain.RepositoriesInterfaces;
using System.Linq.Expressions;


namespace EyeScenceApp.Application.Services
{
    public class DirectorService : IDirectorService
    {
        #region Feilds 
        private readonly IDirectorRepository _directorRepository;
        private readonly IAwardRepository _awardRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IImageUploaderService _imageUploader;
        private readonly ResponseHandler _responseHandler;
        private IMapper _mapper;
        #endregion

        #region Constructor
        public DirectorService(IDirectorRepository directorRepository, IAwardRepository awardRepository, IImageRepository imageRepository, IImageUploaderService imageUploader, ResponseHandler responseHandler, IMapper mapper)
        {
            _directorRepository = directorRepository;
            _awardRepository = awardRepository;
            _imageRepository = imageRepository;
            _imageUploader = imageUploader;
            _responseHandler = responseHandler;
            _mapper = mapper;
        }
        #endregion

        #region Methods

        public async Task<Response<DirectorDTO?>> GetDirectorByIdAsync(Guid DirectorId)
        {
            if (DirectorId == Guid.Empty)
            {
                return _responseHandler.BadRequest<DirectorDTO?>("Director ID cannot be empty.");
            }

            var director = await _directorRepository.GetByIdAsync(DirectorId, false);
            if (director is null)
            {
                return _responseHandler.NotFound<DirectorDTO?>($"director with ID {DirectorId} not found.");
            }

            var directorDto = _mapper.Map<DirectorDTO>(director);

            return _responseHandler.Success<DirectorDTO?>(directorDto);
        }

        public async Task<Response<ICollection<DirectorDTO>>> GetAllDirectorsAsync()
        {
            var directors = await _directorRepository.GetAllAsync();
            if (directors is null)
            {
                return _responseHandler.NotFound<ICollection<DirectorDTO>>($"No directors found.");
            }

            var DirectorsDTOs = _mapper.Map<ICollection<DirectorDTO>>(directors);
            return _responseHandler.Success<ICollection<DirectorDTO>>(DirectorsDTOs);
        }

        public async Task<Response<ICollection<DirectorDTO>>> FilterDirectorAsync(FilterDTO dto)
        {
            if (dto == null)
            {
                return _responseHandler.BadRequest<ICollection<DirectorDTO>>("Filter DTO cannot be null.");
            }

            Expression<Func<Director, bool>> searchPredicate = director =>
                (string.IsNullOrEmpty(dto.Name) ||
                 (director.FirstName != null && director.FirstName.Contains(dto.Name)) ||
                 (director.MiddleName != null && director.MiddleName.Contains(dto.Name)) ||
                 (director.LastName != null && director.LastName.Contains(dto.Name)))
                &&
                (dto.sex == null || director.Sex == dto.sex)
                &&
                (dto.nationality == null || director.Nationality == dto.nationality);

            Expression<Func<Director, string>> orderBy = director => director.FirstName; 

            bool ascending = dto.ascendenig;

            var directorEntities = await _directorRepository.FilterListAsync(orderBy, searchPredicate, ascending);

            if (directorEntities == null)
            {
                return _responseHandler.NotFound<ICollection<DirectorDTO>>("No directors found matching the criteria.");
            }

            var directorDTOs = _mapper.Map<ICollection<DirectorDTO>>(directorEntities);

            return directorDTOs == null
                ? _responseHandler.UnprocessableEntity<ICollection<DirectorDTO>>("Error mapping directors.")
                : _responseHandler.Success(directorDTOs);
        }

        public async Task<Response<ICollection<CelebirtyAwardDTO>>> GetDirectorAwardsAsync(Guid directorId)
        {
            if (directorId == Guid.Empty)
            {
                return _responseHandler.BadRequest<ICollection<CelebirtyAwardDTO>>("Director ID cannot be empty.");
            }

            var Director = await _directorRepository.GetByIdAsync(directorId, false);
            if (Director is null)
            {
                return _responseHandler.NotFound<ICollection<CelebirtyAwardDTO>>($"Director with ID {directorId} not found.");
            }

            var awards = await _awardRepository.GetAwardsByCelebrityAsync(Director.Id);
            if (awards is null)
            {
                return _responseHandler.NotFound<ICollection<CelebirtyAwardDTO>>($"No awards found for Director with ID {directorId}.");
            }

            var awardDtos = _mapper.Map<ICollection<CelebirtyAwardDTO>>(awards);
            return _responseHandler.Success<ICollection<CelebirtyAwardDTO>>(awardDtos);
        }

        public async Task<Response<ICollection<DigitalContentDTO>>> GetDirectorWorksAsync(Guid directorId)
        {
            if (directorId == Guid.Empty)
            {
                return _responseHandler.BadRequest<ICollection<DigitalContentDTO>>("director ID cannot be empty.");
            }
            var director = await _directorRepository.GetByIdAsync(directorId, false);
            if (director is null)
            {
                return _responseHandler.NotFound<ICollection<DigitalContentDTO>>($"director with ID {directorId} not found.");
            }
            var works = await _directorRepository.GetAllWorksAsync(director.Id);
            if (works is null)
            {
                return _responseHandler.NotFound<ICollection<DigitalContentDTO>>($"No works found for director with ID {directorId}.");
            }
            var workDtos = _mapper.Map<ICollection<DigitalContentDTO>>(works);
            return _responseHandler.Success<ICollection<DigitalContentDTO>>(workDtos);
        }

        public async Task<Response<bool>> UpdateDirectorAsync(UpdateDirectorDTO DirectorDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<bool>> CreateDirectorAsync(CreateDirectorDTO DirectorDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<bool>> DeleteDirectorAsync(Guid DirectorId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
