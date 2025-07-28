using AutoMapper;
using EyeScenceApp.Application.Bases;
using EyeScenceApp.Application.DTOs.Awards.CelebirtyAwards;
using EyeScenceApp.Application.DTOs.Crew.Base;
using EyeScenceApp.Application.DTOs.Crew.Editiors;
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
    public class EditorService: IEditorService
    {
        #region Feilds 
        private readonly IEditorRepository _editorRepository;
        private readonly IAwardRepository _awardRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IImageUploaderService _imageUploader;
        private readonly ResponseHandler _responseHandler;
        private IMapper _mapper;
        #endregion

        #region Constructor
        public EditorService(IEditorRepository editorRepository, IAwardRepository awardRepository, IImageRepository imageRepository, IImageUploaderService imageUploader, ResponseHandler responseHandler, IMapper mapper)
        {
            _editorRepository = editorRepository;
            _awardRepository = awardRepository;
            _imageRepository = imageRepository;
            _imageUploader = imageUploader;
            _responseHandler = responseHandler;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task<Response<EditorDTO?>> GetEditorByIdAsync(Guid EditorId)
        {
            if (EditorId == Guid.Empty)
            {
                return _responseHandler.BadRequest<EditorDTO?>("Editor ID cannot be empty.");
            }

            var Editor = await _editorRepository.GetByIdAsync(EditorId, false);
            if (Editor is null)
            {
                return _responseHandler.NotFound<EditorDTO?>($"Editor with ID {EditorId} not found.");
            }

            var EditorDto = _mapper.Map<EditorDTO>(Editor);

            return _responseHandler.Success<EditorDTO?>(EditorDto);
        }

        public async Task<Response<ICollection<EditorDTO>>> GetAllEditorsAsync()
        {
            var Editors = await _editorRepository.GetAllAsync();
            if (Editors is null)
            {
                return _responseHandler.NotFound<ICollection<EditorDTO>>($"No Editors found.");
            }

            var EditorsDTOs = _mapper.Map<ICollection<EditorDTO>>(Editors);
            return _responseHandler.Success<ICollection<EditorDTO>>(EditorsDTOs);
        }

        public async Task<Response<ICollection<EditorDTO>>> FilterEditorAsync(FilterDTO dto)
        {
            if (dto == null)
            {
                return _responseHandler.BadRequest<ICollection<EditorDTO>>("Filter DTO cannot be null.");
            }

            Expression<Func<Editor, bool>> searchPredicate = Editor =>
                (string.IsNullOrEmpty(dto.Name) ||
                 (Editor.FirstName != null && Editor.FirstName.Contains(dto.Name)) ||
                 (Editor.MiddleName != null && Editor.MiddleName.Contains(dto.Name)) ||
                 (Editor.LastName != null && Editor.LastName.Contains(dto.Name)))
                &&
                (dto.sex == null || Editor.Sex == dto.sex)
                &&
                (dto.nationality == null || Editor.Nationality == dto.nationality);

            Expression<Func<Editor, string>> orderBy = Editor => Editor.FirstName;

            bool ascending = dto.ascendenig;

            var EditorEntities = await _editorRepository.FilterListAsync(orderBy, searchPredicate, ascending);

            if (EditorEntities == null)
            {
                return _responseHandler.NotFound<ICollection<EditorDTO>>("No Editors found matching the criteria.");
            }

            var EditorDTOs = _mapper.Map<ICollection<EditorDTO>>(EditorEntities);

            return EditorDTOs == null
                ? _responseHandler.UnprocessableEntity<ICollection<EditorDTO>>("Error mapping Editors.")
                : _responseHandler.Success(EditorDTOs);
        }

        public async Task<Response<ICollection<CelebirtyAwardDTO>>> GetEditorAwardsAsync(Guid EditorId)
        {
            if (EditorId == Guid.Empty)
            {
                return _responseHandler.BadRequest<ICollection<CelebirtyAwardDTO>>("Editor ID cannot be empty.");
            }

            var Editor = await _editorRepository.GetByIdAsync(EditorId, false);
            if (Editor is null)
            {
                return _responseHandler.NotFound<ICollection<CelebirtyAwardDTO>>($"Editor with ID {EditorId} not found.");
            }

            var awards = await _awardRepository.GetAwardsByCelebrityAsync(Editor.Id);
            if (awards is null)
            {
                return _responseHandler.NotFound<ICollection<CelebirtyAwardDTO>>($"No awards found for Editor with ID {EditorId}.");
            }

            var awardDtos = _mapper.Map<ICollection<CelebirtyAwardDTO>>(awards);
            return _responseHandler.Success<ICollection<CelebirtyAwardDTO>>(awardDtos);
        }

        public async Task<Response<ICollection<DigitalContentDTO>>> GetEditorWorksAsync(Guid EditorId)
        {
            if (EditorId == Guid.Empty)
            {
                return _responseHandler.BadRequest<ICollection<DigitalContentDTO>>("Editor ID cannot be empty.");
            }
            var Editor = await _editorRepository.GetByIdAsync(EditorId, false);
            if (Editor is null)
            {
                return _responseHandler.NotFound<ICollection<DigitalContentDTO>>($"Editor with ID {EditorId} not found.");
            }
            var works = await _editorRepository.GetAllWorksAsync(Editor.Id);
            if (works is null)
            {
                return _responseHandler.NotFound<ICollection<DigitalContentDTO>>($"No works found for Editor with ID {EditorId}.");
            }
            var workDtos = _mapper.Map<ICollection<DigitalContentDTO>>(works);
            return _responseHandler.Success<ICollection<DigitalContentDTO>>(workDtos);
        }



        public async Task<Response<bool>> UpdateEditorAsync(UpdateEditorDTO EditorDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<bool>> CreateEditorAsync(CreateEditorDTO EditorDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<bool>> DeleteEditorAsync(Guid EditorId)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
