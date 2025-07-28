using AutoMapper;
using EyeScenceApp.Application.Bases;
using EyeScenceApp.Application.DTOs.Actors;
using EyeScenceApp.Application.DTOs.SingleDocumentries;
using EyeScenceApp.Application.IServices;
using EyeScenceApp.Domain.Entities;
using EyeScenceApp.Domain.Enums;
using EyeScenceApp.Domain.RepositoriesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.Services
{
    public class SingleDocumantryService : ISingleDocumantryService
    {
        #region Feild(s)
        private readonly ISingleDocumentaryRepository _singleDocumentaryRepository;
        private readonly ICastRepository _castRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IImageUploaderService _imageUploader;
        private readonly IMapper _mapper;
        private readonly ResponseHandler _responseHandler;

        #endregion

        #region Constructor(s)

        public SingleDocumantryService(ISingleDocumentaryRepository singleDocumentaryRepository, IMapper mapper, ResponseHandler responseHandler, IImageRepository imageRepository, IImageUploaderService imageUploader, ICastRepository castRepository)
        {
            _singleDocumentaryRepository = singleDocumentaryRepository;
            _mapper = mapper;
            _responseHandler = responseHandler;
            _imageRepository = imageRepository;
            _imageUploader = imageUploader;
            _castRepository = castRepository;
        }

        #endregion

        #region Method(s)
        public async Task<Response<DocumantaryDTO>> GetSingleDocumantryAsync(Guid documantryId)
        {
            if (documantryId == Guid.Empty)
            {
                return _responseHandler.BadRequest<DocumantaryDTO>("Documentary ID cannot be empty.");
            }
            var documantry = await _singleDocumentaryRepository.GetByIdAsync(documantryId,false);
            if (documantry == null) {
                return _responseHandler.NotFound<DocumantaryDTO>("Documentary not found.");
            }
            var documantaryDTO = _mapper.Map<DocumantaryDTO>(documantry);
            return _responseHandler.Success(documantaryDTO, "Documentary retrieved successfully.");
        }

        public async  Task<Response<ICollection<DocumantaryDTO>>> SearchDocumantriesByTiltle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return _responseHandler.BadRequest<ICollection<DocumantaryDTO>>("Title cannot be empty.");
            }

            var documantries = await _singleDocumentaryRepository.GetSingleDocumentaryByTitleAsync(title);
            if (documantries == null || !documantries.Any())
            {
                return _responseHandler.NotFound<ICollection<DocumantaryDTO>>("No documentaries found with the given title.");
            }

            var documantaryDTOs = _mapper.Map<ICollection<DocumantaryDTO>>(documantries);
            return _responseHandler.Success(documantaryDTOs, "Documentaries retrieved successfully.");
        }

        public async Task<Response<ICollection<DocumantaryDTO>>> GetAllDocumantriesAsync()
        {
            var documantries = await _singleDocumentaryRepository.GetAllAsync(false);
            if (documantries == null)
            {
                return _responseHandler.NotFound<ICollection<DocumantaryDTO>>("No documentaries found.");
            }

            var documantaryDTOs = _mapper.Map<ICollection<DocumantaryDTO>>(documantries);
            return _responseHandler.Success(documantaryDTOs, "Documentaries retrieved successfully.");
        }

        public async Task<Response<ICollection<DocumantaryDTO>>> GetAllDocumantriesByCountryAsync(Nationality countryName)
        {
           
            Expression<Func<SingleDocumentary, bool>> searchPredicate = sd => sd.CountryOfOrigin == countryName;

            Expression<Func<SingleDocumentary, double>> orderBy = sd => sd.Rate; // default ordering

            bool ascending = false;
            var documantries = await _singleDocumentaryRepository.FilterListAsync(orderBy , searchPredicate , ascending);
            if (documantries == null || !documantries.Any())
            {
                return _responseHandler.NotFound<ICollection<DocumantaryDTO>>($"No documentaries found for country: {countryName}.");
            }
            var documantaryDTOs = _mapper.Map<ICollection<DocumantaryDTO>>(documantries);
            return _responseHandler.Success(documantaryDTOs, $"Documentaries from {countryName} retrieved successfully.");
        }

        public async Task<Response<ICollection<DocumantaryDTO>>> GetAllDocumantriesByRealseyearAsync(int year)
        {
            if (year < 1900 || year > DateTime.Now.Year)
            {
                return _responseHandler.BadRequest<ICollection<DocumantaryDTO>>("Invalid year provided.");
            }
            Expression<Func<SingleDocumentary, bool>> searchPredicate = sd => sd.ReleaseYear == year;

            Expression<Func<SingleDocumentary, double>> orderBy = sd => sd.Rate; // default ordering

            bool ascending = false;
            var documantries = await _singleDocumentaryRepository.FilterListAsync(orderBy, searchPredicate, ascending);
            if (documantries == null || !documantries.Any())
            {
                return _responseHandler.NotFound<ICollection<DocumantaryDTO>>($"No documentaries found for year: {year}.");
            }
            var documantaryDTOs = _mapper.Map<ICollection<DocumantaryDTO>>(documantries);
            return _responseHandler.Success(documantaryDTOs, $"Documentaries from {year} retrieved successfully.");
        }

        public async Task<Response<DocumantaryDTO>> CreateSingleDocumantryAsync(CreateDocumantryDTO createDocumantaryDTO)
        {
            if (createDocumantaryDTO is null)
            {
                return _responseHandler.BadRequest<DocumantaryDTO> ("createDocumantaryDTO can not be null");

            }



            try
            {
                var documantary = _mapper.Map<SingleDocumentary>(createDocumantaryDTO);
                // Begin transaction
                await using var transaction = await _singleDocumentaryRepository.BeginTransactionAsync();

                try
                {
                    // Upload primary image
                    var PosterResult = await _imageUploader.UploadImageAsync(
                    createDocumantaryDTO.Poster,
                    ImageFolder.DigitalContentsImages);

                    if (PosterResult.Error is not null)
                    {
                        await transaction.RollbackAsync();
                        return _responseHandler.BadRequest<DocumantaryDTO>(null, "Failed to upload primary Poster.");
                    }
                    // Create image record
                    var Poster = new Image
                    {
                        IsPrimary = true,
                        Url = PosterResult.Url.ToString()
                    };
                    var createdPoster = await _imageRepository.AddAsync(Poster);

                    if (createdPoster == null)
                    {
                        return _responseHandler.Failed<DocumantaryDTO>("Failed to Save documentary poster.");
                    }
                    documantary.PosterId = createdPoster.Id;
                    var createdDocumantary = await _singleDocumentaryRepository.AddAsync(documantary);

                    if (createdDocumantary == null)
                    {
                        return _responseHandler.Failed<DocumantaryDTO>("Failed to create documentary.");
                    }

                    await _singleDocumentaryRepository.CommitAsync();
                    return _responseHandler.Created(_mapper.Map<DocumantaryDTO>(createdDocumantary), "Documentary created successfully.");

                }
                catch (Exception ex)
                {
                    // Handle exceptions and rollback transaction
                    await _singleDocumentaryRepository.RollBackAsync();
                    return _responseHandler.Failed<DocumantaryDTO>($"An error occurred while creating the documentary: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                return _responseHandler.BadRequest<DocumantaryDTO>(null, "An error occurred while starting the transaction.");
            }
        }

        public async Task<Response<DocumantaryDTO>> UpdateSingleDocumantryAsync(UpdateDocumantryDTO updateDocumantaryDTO)
        {
            if (updateDocumantaryDTO is null)
            {
                return _responseHandler.BadRequest<DocumantaryDTO>("createDocumantaryDTO can not be null");

            }

            var documantary = await _singleDocumentaryRepository.GetByIdAsync(updateDocumantaryDTO.Id, true);
            if (documantary == null)
            {
                return _responseHandler.NotFound<DocumantaryDTO>("Documentary not found.");
            }
             _mapper.Map(updateDocumantaryDTO,documantary);
            try
            {
                // Begin transaction
                await using var transaction = await _singleDocumentaryRepository.BeginTransactionAsync();
                if (updateDocumantaryDTO.Poster is not null)
                {
                    // If a new poster is provided, delete the old one if it exists
                    var deleteImage = await _imageRepository.GetByIdAsync(documantary.Id, true);
                    if (deleteImage != null)
                    {
                        var result = await _imageUploader.DeleteImageByUrlAsync(deleteImage.Url);
                        if (!result)
                        {
                            await transaction.RollbackAsync();
                            return _responseHandler.BadRequest<DocumantaryDTO>(null, "Failed to delete old Poster.");
                        }
                        await _imageRepository.DeleteAsync(deleteImage);
                    }
                    // Upload primary image
                    var PosterResult = await _imageUploader.UploadImageAsync(
                        updateDocumantaryDTO.Poster,
                        ImageFolder.DigitalContentsImages);

                    if (PosterResult.Error is not null)
                    {
                        await transaction.RollbackAsync();
                        return _responseHandler.BadRequest<DocumantaryDTO>(null, "Failed to upload primary Poster.");
                    }
                    // Create image record
                    var Poster = new Image
                    {
                        IsPrimary = true,
                        Url = PosterResult.Url.ToString()
                    };
                    var createdPoster = await _imageRepository.AddAsync(Poster);

                    if (createdPoster == null)
                    {
                        return _responseHandler.Failed<DocumantaryDTO>("Failed to Save documentary poster.");
                    }
                    documantary.PosterId = createdPoster.Id;

                }
              
               
                 await _singleDocumentaryRepository.UpdateAsync(documantary);

                 await _singleDocumentaryRepository.CommitAsync();
                var createdDocumantary = await _singleDocumentaryRepository.GetByIdAsync(documantary.Id, false);
                return _responseHandler.Success(_mapper.Map<DocumantaryDTO>(createdDocumantary), "Documentary created successfully.");

            }
            catch (Exception ex)
            {
                // Handle exceptions and rollback transaction
                await _singleDocumentaryRepository.RollBackAsync();
                return _responseHandler.Failed<DocumantaryDTO>($"An error occurred while creating the documentary: {ex.Message}");
            }
        }

        public async Task<Response<bool>> DeleteSingleDocumantryAsync(Guid documantryId)
        {
            if (documantryId == Guid.Empty)
            {
                return _responseHandler.BadRequest<bool>("Documantry ID cannot be empty.");
            }
            var documentary = await _singleDocumentaryRepository.GetByIdAsync(documantryId, false);
            if (documentary == null)
            {
                return _responseHandler.NotFound<bool>($"Documentary with ID {documantryId} not found.");
            }
            var image = await _imageRepository.GetByIdAsync(documentary.PosterId, false);
            if (image != null)
            {
                var deleteResult = await _imageUploader.DeleteImageAsync(image.Url);
                if (!deleteResult)
                {
                    return _responseHandler.Failed<bool>("Failed to delete movie poster image.");
                }
                else
                {
                    await _imageRepository.DeleteAsync(image);
                    await _singleDocumentaryRepository.DeleteAsync(documentary);
                    return _responseHandler.Success(true, "Documentary deleted successfully.");
                }
            }
            else
            {
                await _singleDocumentaryRepository.DeleteAsync(documentary);
                return _responseHandler.Success(true, "Movie deleted successfully ,No poster Found to deleted.");
            }
        }

        public async Task<Response<ICollection<DocumantaryDTO>>> getTopRatedDocumantries()
        {
            var documantries = await _singleDocumentaryRepository.GetTopRatedSingleDocumantaryAsync();
            if (documantries == null || !documantries.Any())
            {
                return _responseHandler.NotFound<ICollection<DocumantaryDTO>>("No top-rated documentaries found.");
            }

            var documantaryDTOs = _mapper.Map<ICollection<DocumantaryDTO>>(documantries);
            return _responseHandler.Success(documantaryDTOs, "Top-rated documentaries retrieved successfully.");
        }
        public async Task<Response<ICollection<ActorDTO>>> GetSingleDocumantryCastAsync(Guid DocumantryId)
        {
            if (DocumantryId == Guid.Empty)
            {
                return _responseHandler.BadRequest<ICollection<ActorDTO>>("Documantry Id Not Valid");
            }
            var ExistMovie = await _singleDocumentaryRepository.GetByIdAsync(DocumantryId, false);
            if (ExistMovie == null)
            {
                return _responseHandler.NotFound<ICollection<ActorDTO>>($"Documantry with ID {DocumantryId} not found.");
            }
            var actors = await _castRepository.GetActorsByDocumantaryIdAsync(DocumantryId);
            if (actors == null || !actors.Any())
            {
                return _responseHandler.NotFound<ICollection<ActorDTO>>($"No cast found for Documantry with ID {DocumantryId}.");
            }
            var actorDTOs = _mapper.Map<ICollection<ActorDTO>>(actors);
            return _responseHandler.Success(actorDTOs, $"Cast for Documantry with ID {DocumantryId} retrieved successfully.");
        }
        public async Task<Response<ICollection<DocumantaryDTO>>> GetNewRealseDocumantries()
        {
            var documantries = await _singleDocumentaryRepository.GetNewAddedSingleDocumentaries();
            if (documantries == null || !documantries.Any())
            {
                return _responseHandler.NotFound<ICollection<DocumantaryDTO>>("No new release documentaries found.");
            }

            var documantaryDTOs = _mapper.Map<ICollection<DocumantaryDTO>>(documantries);
            return _responseHandler.Success(documantaryDTOs, "New release documentaries retrieved successfully.");
        }

        #endregion
    }
}
