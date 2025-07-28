using AutoMapper;
using EyeScenceApp.Application.Bases;
using EyeScenceApp.Application.DTOs.Awards;
using EyeScenceApp.Application.DTOs.Awards.CelebirtyAwards;
using EyeScenceApp.Application.DTOs.Awards.DigitalContentAwards;
using EyeScenceApp.Application.DTOs.Awards;
using EyeScenceApp.Application.IServices;
using EyeScenceApp.Domain.Entities;
using EyeScenceApp.Domain.Enums;
using EyeScenceApp.Domain.RepositoriesInterfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.Services
{
    public class AwardService : IAwardService
    {
        #region Feild(s)
        private readonly IAwardRepository _awardRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IImageUploaderService _imageUploaderService;
        private readonly IDigitalContentRepository _digitalContentRepository;
        private readonly ICastRepository _castRepository;
        private readonly IMapper _mapper;
        private readonly ResponseHandler _responseHandler;
        #endregion

        #region Constructor(s)
        public AwardService(IAwardRepository awardRepository, IImageRepository imageRepository, IImageUploaderService imageUploaderService, IDigitalContentRepository digitalContentRepository, ICastRepository castRepository, IMapper mapper, ResponseHandler responseHandler)
        {
            _awardRepository = awardRepository ?? throw new ArgumentNullException(nameof(awardRepository));
            _imageRepository = imageRepository ?? throw new ArgumentNullException(nameof(imageRepository));
            _imageUploaderService = imageUploaderService ?? throw new ArgumentNullException(nameof(imageUploaderService));
            _digitalContentRepository = digitalContentRepository ?? throw new ArgumentNullException(nameof(digitalContentRepository));
            _castRepository = castRepository ?? throw new ArgumentNullException(nameof(castRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _responseHandler = responseHandler ?? throw new ArgumentNullException(nameof(responseHandler));
        }
        #endregion

        #region Method(s)
        public async Task<Response<AwardDTO>> GetAwardByNameAsync(string name)
        {
            if (name.IsNullOrEmpty())
            {
                return _responseHandler.BadRequest<AwardDTO>("Award name cannot be null or Empty.");
            }
            var award = await _awardRepository.GetAwardByNameAsync(name);
            if (award == null)
            {
                return _responseHandler.NotFound<AwardDTO>($"Award with name '{name}' not found.");
            }
            var awardDTO = _mapper.Map<AwardDTO>(award);
            return _responseHandler.Success<AwardDTO>(awardDTO , "Award Restrived SuccessFully");
        }

        public async Task<Response<IEnumerable<AwardDTO>>> GetAwardsByOrganizationAsync(string organizationName)
        {
            if (organizationName.IsNullOrEmpty())
            {
                return _responseHandler.BadRequest<IEnumerable<AwardDTO>>("Organization name cannot be null or Empty.");
            }
            var awards = await _awardRepository.GetAwardsByOrganizationAsync(organizationName);
            if (awards == null)
            {
                return _responseHandler.NotFound<IEnumerable<AwardDTO>>($"No awards found for organization '{organizationName}'.");
            }
            var awardDTOs = _mapper.Map<IEnumerable<AwardDTO>>(awards);
            return _responseHandler.Success<IEnumerable<AwardDTO>>(awardDTOs, "Awards retrieved successfully.");
        }

        public async Task<Response<IEnumerable<AwardDTO>>> GetAwardsByCategoryAsync(string category)
        {
            if (category.IsNullOrEmpty())
            {
                return _responseHandler.BadRequest<IEnumerable<AwardDTO>>("Category cannot be null or Empty.");
            }
            var awards = await _awardRepository.GetAwardsByCategoryAsync(category);
            if (awards == null )
            {
                return _responseHandler.NotFound<IEnumerable<AwardDTO>>($"No awards found for category '{category}'.");
            }
            var awardDTOs = _mapper.Map<IEnumerable<AwardDTO>>(awards);
            return _responseHandler.Success<IEnumerable<AwardDTO>>(awardDTOs, "Awards retrieved successfully.");
        }

        public async Task<Response<IEnumerable<AwardDTO>>> GetAllAwardsAsync()
        {
            var awards = await _awardRepository.GetAllAsync();
            if (awards == null)
            {
                return _responseHandler.NotFound<IEnumerable<AwardDTO>>("No awards found.");
            }
            var awardDTOs = _mapper.Map<IEnumerable<AwardDTO>>(awards);
            return _responseHandler.Success<IEnumerable<AwardDTO>>(awardDTOs, "Awards retrieved successfully.");
        }

        public async Task<Response<DigitalContentAwardDTO>> GetDigitalContentAwardByIdAsync(Guid awardId)
        {
            if (awardId == Guid.Empty)
            {
                return _responseHandler.BadRequest<DigitalContentAwardDTO>("Award ID cannot be empty.");
            }
            var award = await _awardRepository.GetDigitalContentAwardByIdAsync(awardId);
            if (award == null)
            {
                return _responseHandler.NotFound<DigitalContentAwardDTO>($"Digital content award with ID '{awardId}' not found.");
            }
            var awardDTO = _mapper.Map<DigitalContentAwardDTO>(award);
            return _responseHandler.Success<DigitalContentAwardDTO>(awardDTO, "Digital content award retrieved successfully.");

        }

        public async Task<Response<IEnumerable<DigitalContentAwardDTO>>> GetAllDigitalContentAwardsAsync()
        {
            var awards = await _awardRepository.GetAllDigitalContentAwardsAsync();
            if (awards == null)
            {
                return _responseHandler.NotFound<IEnumerable<DigitalContentAwardDTO>>("No digital content awards found.");
            }
            var awardDTOs = _mapper.Map<IEnumerable<DigitalContentAwardDTO>>(awards);
            return _responseHandler.Success<IEnumerable<DigitalContentAwardDTO>>(awardDTOs, "Digital content awards retrieved successfully.");
        }

        public async Task<Response<IEnumerable<DigitalContentAwardDTO>>> GetDigitalContentAwardsByDigitalContentAsync(Guid digitalContentId)
        {
            if (digitalContentId == Guid.Empty)
            {
                return _responseHandler.BadRequest<IEnumerable<DigitalContentAwardDTO>>("Digital content ID cannot be empty.");
            }
            var isDigitalContentExist = await _digitalContentRepository.IsDigitalContentExist(digitalContentId);
            if (!isDigitalContentExist)
            {
                return _responseHandler.NotFound<IEnumerable<DigitalContentAwardDTO>>($"Digital content with ID '{digitalContentId}' not found.");
            }
            var awards = await _awardRepository.GetAwardsByDigitalContentAsync(digitalContentId);
            if (awards == null)
            {
                return _responseHandler.NotFound<IEnumerable<DigitalContentAwardDTO>>($"No awards found for digital content with ID '{digitalContentId}'.");
            }
            var awardDTOs = _mapper.Map<IEnumerable<DigitalContentAwardDTO>>(awards);
            return _responseHandler.Success<IEnumerable<DigitalContentAwardDTO>>(awardDTOs, "Digital content awards retrieved successfully.");
        }

        public async Task<Response<bool>> AddAwardToDigitalContentAsync(Guid awardId, Guid digitalContentId, int Year)
        {
            if (awardId == Guid.Empty)
            {
                return _responseHandler.BadRequest<bool>("Award ID cannot be empty.");
            }
            if (digitalContentId == Guid.Empty)
            {
                return _responseHandler.BadRequest<bool>("Digital content ID cannot be empty.");
            }
            if (Year == 0) {
                return _responseHandler.BadRequest<bool>("Year cannot be zero.");
            }
            var digitalContent = await _digitalContentRepository.GetDigitalContentByIdAsync(digitalContentId);
            if (digitalContent == null)
            {
                return _responseHandler.NotFound<bool>($"Digital content with ID '{digitalContentId}' not found.");
            }
            var award = await _awardRepository.GetByIdAsync(awardId);
            if (award == null)
            {
                return _responseHandler.NotFound<bool>($"Award with ID '{awardId}' not found.");
            }
            var Added = await _awardRepository.AddAwardToDigitalContentAsync(award, digitalContent, Year);
            if (!Added)
            {
                return _responseHandler.Failed<bool>("Failed to add award to digital content.");
            }
            return _responseHandler.Success<bool>(true, "Award added to digital content successfully.");
        }

        public async Task<Response<bool>> RemoveAwardFromDigitalContentAsync(Guid awardId, Guid digitalContentId, int Year)
        {
            if (awardId == Guid.Empty)
            {
                return _responseHandler.BadRequest<bool>("Award ID cannot be empty.");
            }
            if (digitalContentId == Guid.Empty)
            {
                return _responseHandler.BadRequest<bool>("Digital content ID cannot be empty.");
            }
            if (Year == 0)
            {
                return _responseHandler.BadRequest<bool>("Year cannot be zero.");
            }
            var digitalContent = await _digitalContentRepository.GetDigitalContentByIdAsync(digitalContentId);
            if (digitalContent == null)
            {
                return _responseHandler.NotFound<bool>($"Digital content with ID '{digitalContentId}' not found.");
            }
            var award = await _awardRepository.GetByIdAsync(awardId);
            if (award == null)
            {
                return _responseHandler.NotFound<bool>($"Award with ID '{awardId}' not found.");
            }
            var Removed = await _awardRepository.RemoveAwardFromDigitalContentAsync(awardId, digitalContentId, Year);
            if (!Removed)
            {
                return _responseHandler.Failed<bool>("Failed to remove award from digital content.");
            }
            return _responseHandler.Deleted<bool>(true, "Award removed from digital content successfully.");
        }

        public async Task<Response<DigitalContentAwardDTO>> CreateDigitalContentAwardAsync(CreateDigitalContentAwardDTO awardDTO)
        {
            if (awardDTO is null)
            {
                return _responseHandler.BadRequest<DigitalContentAwardDTO>("Create Award DTO can not be null");

            }
            try
            {
                var award = _mapper.Map<DigitalContentAward>(awardDTO);
                // Begin transaction
                await using var transaction = await _awardRepository.BeginTransactionAsync();

                try
                {
                    // Upload primary image
                    var PosterResult = await _imageUploaderService.UploadImageAsync(
                    awardDTO.Poster,
                    ImageFolder.DigitalContentsImages);

                    if (PosterResult.Error is not null)
                    {
                        await transaction.RollbackAsync();
                        return _responseHandler.BadRequest<DigitalContentAwardDTO>(null, "Failed to upload primary Poster.");
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
                        return _responseHandler.Failed<DigitalContentAwardDTO>("Failed to Save Award poster.");
                    }
                    award.PosterId = createdPoster.Id;
                    var createdAward = await _awardRepository.AddAsync(award);

                    if (createdAward == null)
                    {
                        return _responseHandler.Failed<DigitalContentAwardDTO>("Failed to create Award.");
                    }

                    await _awardRepository.CommitAsync();
                    return _responseHandler.Created(_mapper.Map<DigitalContentAwardDTO>(createdAward), "Award created successfully.");

                }
                catch (Exception ex)
                {
                    // Handle exceptions and rollback transaction
                    await _awardRepository.RollBackAsync();
                    return _responseHandler.Failed<DigitalContentAwardDTO>($"An error occurred while creating the Award: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                return _responseHandler.BadRequest<DigitalContentAwardDTO>(null, "An error occurred while starting the transaction.");
            }
        }

        public async Task<Response<DigitalContentAwardDTO>> UpdateDigitalContentAwardAsync(UpdateDigitalContentAwardDTO awardDTO)
        {
            if (awardDTO is null)
            {
                return _responseHandler.BadRequest<DigitalContentAwardDTO>("Updated Award DTO cannot be null");
            }

            var award = await _awardRepository.GetDigitalContentAwardByIdAsync(awardDTO.Id, true);
            if (award == null)
            {
                return _responseHandler.NotFound<DigitalContentAwardDTO>("Award not found.");
            }
            _mapper.Map(awardDTO, award);
            try
            {
                await using var transaction = await _awardRepository.BeginTransactionAsync();

                if (awardDTO.Poster is not null)
                {
                    var oldImage = await _imageRepository.GetByIdAsync(award.PosterId, true);
                    if (oldImage != null)
                    {
                        var deleteResult = await _imageUploaderService.DeleteImageByUrlAsync(oldImage.Url);
                        if (!deleteResult)
                        {
                            await transaction.RollbackAsync();
                            return _responseHandler.BadRequest<DigitalContentAwardDTO>(null, "Failed to delete old poster.");
                        }
                        await _imageRepository.DeleteAsync(oldImage);
                    }

                    var posterResult = await _imageUploaderService.UploadImageAsync(
                        awardDTO.Poster,
                        ImageFolder.DigitalContentsImages);

                    if (posterResult.Error is not null)
                    {
                        await transaction.RollbackAsync();
                        return _responseHandler.BadRequest<DigitalContentAwardDTO>(null, "Failed to upload primary poster.");
                    }

                    var poster = new Image
                    {
                        IsPrimary = true,
                        Url = posterResult.Url.ToString()
                    };
                    var createdPoster = await _imageRepository.AddAsync(poster);
                    if (createdPoster == null)
                    {
                        await transaction.RollbackAsync();
                        return _responseHandler.Failed<DigitalContentAwardDTO>("Failed to save award poster.");
                    }

                    award.PosterId = createdPoster.Id;
                }

                await _awardRepository.UpdateAsync(award);
                await _awardRepository.CommitAsync();
                var updatedAward = await _awardRepository.GetByIdAsync(award.Id, false);
                return _responseHandler.Success(_mapper.Map<DigitalContentAwardDTO>(updatedAward), "Award updated successfully.");
            }
            catch (Exception ex)
            {
                await _awardRepository.RollBackAsync();
                return _responseHandler.Failed<DigitalContentAwardDTO>($"An error occurred while updating the award: {ex.Message}");
            }
        }


        public async Task<Response<bool>> DeleteDigitalContentAwardAsync(Guid awardId)
        {
            if (awardId == Guid.Empty)
            {
                return _responseHandler.BadRequest<bool>("Award ID cannot be empty.");
            }
            var award = await _awardRepository.GetByIdAsync(awardId, false);
            if (award == null)
            {
                return _responseHandler.NotFound<bool>($"Award with ID {awardId} not found.");
            }
            var image = await _imageRepository.GetByIdAsync(award.PosterId, false);
            if (image != null)
            {
                var deleteResult = await _imageUploaderService.DeleteImageByUrlAsync(image.Url);
                if (!deleteResult)
                {
                    return _responseHandler.Failed<bool>("Failed to delete award poster image.");
                }
                else
                {
                    await _imageRepository.DeleteAsync(image);
                    await _awardRepository.DeleteAsync(award);
                    return _responseHandler.Success(true, "Award deleted successfully.");
                }
            }
            else
            {
                await _awardRepository.DeleteAsync(award);
                return _responseHandler.Success(true, "Award deleted successfully ,No poster Found to deleted.");
            }

        }

        // Celebirty Awards Methods

        public async Task<Response<CelebirtyAwardDTO>> GetCelebirtyAwardByIdAsync(Guid awardId)
        {
            if(awardId == Guid.Empty)
            {
                return _responseHandler.BadRequest<CelebirtyAwardDTO>("Award ID cannot be empty.");
            }
            var award = await _awardRepository.GetCelebirtyAwardByIdAsync(awardId);
            if (award == null)
            {
                return _responseHandler.NotFound<CelebirtyAwardDTO>($"Celebrity award with ID '{awardId}' not found.");
            }
            var awardDTO = _mapper.Map<CelebirtyAwardDTO>(award);
            return _responseHandler.Success<CelebirtyAwardDTO>(awardDTO, "Celebrity award retrieved successfully.");
        }

        public async Task<Response<IEnumerable<CelebirtyAwardDTO>>> GetAllCelebirtyAwardsAsync()
        {
            var awards = await _awardRepository.GetAllCelebirtyAwardsAsync();
            if (awards == null)
            {
                return _responseHandler.NotFound<IEnumerable<CelebirtyAwardDTO>>("No celebrity awards found.");
            }
            var awardDTOs = _mapper.Map<IEnumerable<CelebirtyAwardDTO>>(awards);
            return _responseHandler.Success<IEnumerable<CelebirtyAwardDTO>>(awardDTOs, "Celebrity awards retrieved successfully.");
        }

        public async Task<Response<IEnumerable<CelebirtyAwardDTO>>> GetAwardsByCelebrityAsync(Guid celebirtyId)
        {
            if (celebirtyId == Guid.Empty)
            {
                return _responseHandler.BadRequest<IEnumerable<CelebirtyAwardDTO>>("Celebrity ID cannot be empty.");
            }
            var CelebirtyExist = await _castRepository.GetByIdAsync(celebirtyId);
            if (CelebirtyExist is  null)
            {
                return _responseHandler.NotFound<IEnumerable<CelebirtyAwardDTO>>($"Celebrity with ID '{celebirtyId}' not found.");
            }
            var awards = await _awardRepository.GetAwardsByCelebrityAsync(celebirtyId);
            if (awards == null)
            {
                return _responseHandler.NotFound<IEnumerable<CelebirtyAwardDTO>>($"No awards found for celebrity with ID '{celebirtyId}'.");
            }
            var awardDTOs = _mapper.Map<IEnumerable<CelebirtyAwardDTO>>(awards);
            return _responseHandler.Success<IEnumerable<CelebirtyAwardDTO>>(awardDTOs, "Celebrity awards retrieved successfully.");
        }

        public async Task<Response<bool>> AddAwardToCelebirtyAsync(Guid awardId, Guid celebirtyId, int year)
        {
            if (awardId == Guid.Empty)
            {
                return _responseHandler.BadRequest<bool>("Award ID cannot be empty.");
            }
            if (celebirtyId == Guid.Empty)
            {
                return _responseHandler.BadRequest<bool>("Celebrity ID cannot be empty.");
            }
            if (year <= 0)
            {
                return _responseHandler.BadRequest<bool>("Year cannot be zero.");
            }
            var CelebirtyExist = await _castRepository.GetByIdAsync(celebirtyId);
            if (CelebirtyExist is null)
            {
                return _responseHandler.NotFound<bool>($"Celebrity with ID '{celebirtyId}' not found.");
            }
            var award = await _awardRepository.GetByIdAsync(awardId);
            if (award == null)
            {
                return _responseHandler.NotFound<bool>($"Award with ID '{awardId}' not found.");
            }
            var Added = await _awardRepository.AddAwardToCelebirtyAsync(award, CelebirtyExist, year);
            if (!Added)
            {
                return _responseHandler.Failed<bool>("Failed to add award to celebrity.");
            }
            return _responseHandler.Success<bool>(true, "Award added to celebrity successfully.");
        }

        public async Task<Response<bool>> RemoveAwardFromCelebrityAsync(Guid awardId, Guid celebirtyId, int Year)
        {
            if (awardId == Guid.Empty)
            {
                return _responseHandler.BadRequest<bool>("Award ID cannot be empty.");
            }
            if (celebirtyId == Guid.Empty)
            {
                return _responseHandler.BadRequest<bool>("Celebrity ID cannot be empty.");
            }
            if (Year <= 0)
            {
                return _responseHandler.BadRequest<bool>("Year cannot be zero.");
            }
            var CelebirtyExist = await _castRepository.GetByIdAsync(celebirtyId);
            if (CelebirtyExist is null)
            {
                return _responseHandler.NotFound<bool>($"Celebrity with ID '{celebirtyId}' not found.");
            }
            var award = await _awardRepository.GetByIdAsync(awardId);
            if (award == null)
            {
                return _responseHandler.NotFound<bool>($"Award with ID '{awardId}' not found.");
            }
            var Removed = await _awardRepository.RemoveAwardFromCelebrityAsync(awardId, celebirtyId, Year);
            if (!Removed)
            {
                return _responseHandler.Failed<bool>("Failed to remove award from celebrity.");
            }
            return _responseHandler.Deleted<bool>(true, "Award removed from celebrity successfully.");
        }

        public async Task<Response<CelebirtyAwardDTO>> CreateCelebirtyAwardAsync(CreateCelebirtyAwardDTO awardDto)
        {
            
                if (awardDto is null)
                {
                    return _responseHandler.BadRequest<CelebirtyAwardDTO>("Create Award DTO can not be null");

                }
                try
                {
                    var award = _mapper.Map<CelebirtyAward>(awardDto);
                    // Begin transaction
                    await using var transaction = await _awardRepository.BeginTransactionAsync();

                    try
                    {
                        // Upload primary image
                        var PosterResult = await _imageUploaderService.UploadImageAsync(
                        awardDto.Poster,
                        ImageFolder.DigitalContentsImages);

                        if (PosterResult.Error is not null)
                        {
                            await transaction.RollbackAsync();
                            return _responseHandler.BadRequest<CelebirtyAwardDTO>(null, "Failed to upload primary Poster.");
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
                            return _responseHandler.Failed<CelebirtyAwardDTO>("Failed to Save Award poster.");
                        }
                        award.PosterId = createdPoster.Id;
                        var createdAward = await _awardRepository.AddAsync(award);

                        if (createdAward == null)
                        {
                            return _responseHandler.Failed<CelebirtyAwardDTO>("Failed to create Award.");
                        }

                        await _awardRepository.CommitAsync();
                        return _responseHandler.Created(_mapper.Map<CelebirtyAwardDTO>(createdAward), "Award created successfully.");

                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions and rollback transaction
                        await _awardRepository.RollBackAsync();
                        return _responseHandler.Failed<CelebirtyAwardDTO>($"An error occurred while creating the Award: {ex.Message}");
                    }
                }
                catch (Exception ex)
                {
                    return _responseHandler.BadRequest<CelebirtyAwardDTO>(null, "An error occurred while starting the transaction.");
                }
            
        }

        public async Task<Response<CelebirtyAwardDTO>> UpdateCelebirtyAwardAsync(UpdateCelebirtyAwardDTO awardDTO)
        {
            if (awardDTO is null)
            {

                return _responseHandler.BadRequest<CelebirtyAwardDTO>(" Updated Award DTO can not be null");

            }

            var award = await _awardRepository.GetByIdAsync(awardDTO.Id, true);
            if (award == null)
            {
                return _responseHandler.NotFound<CelebirtyAwardDTO>("award not found.");
            }
            _mapper.Map(awardDTO, award);
            try
            {
                // Begin transaction
                await using var transaction = await _awardRepository.BeginTransactionAsync();
                if (awardDTO.Poster is not null)
                {
                    // If a new poster is provided, delete the old one if it exists
                    var deleteImage = await _imageRepository.GetByIdAsync(award.Id, true);
                    if (deleteImage != null)
                    {
                        var result = await _imageUploaderService.DeleteImageByUrlAsync(deleteImage.Url);
                        if (!result)
                        {
                            await transaction.RollbackAsync();
                            return _responseHandler.BadRequest<CelebirtyAwardDTO>(null, "Failed to delete old Poster.");
                        }
                        await _imageRepository.DeleteAsync(deleteImage);
                    }
                    // Upload primary image
                    var PosterResult = await _imageUploaderService.UploadImageAsync(
                        awardDTO.Poster,
                        ImageFolder.DigitalContentsImages);

                    if (PosterResult.Error is not null)
                    {
                        await transaction.RollbackAsync();
                        return _responseHandler.BadRequest<CelebirtyAwardDTO>(null, "Failed to upload primary Poster.");
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
                        return _responseHandler.Failed<CelebirtyAwardDTO>("Failed to Save award poster.");
                    }
                    award.PosterId = createdPoster.Id;

                }


                await _awardRepository.UpdateAsync(award);

                await _awardRepository.CommitAsync();
                var createdAward = await _awardRepository.GetByIdAsync(award.Id, false);
                return _responseHandler.Success(_mapper.Map<CelebirtyAwardDTO>(createdAward), "Award created successfully.");

            }
            catch (Exception ex)
            {
                // Handle exceptions and rollback transaction
                await _awardRepository.RollBackAsync();
                return _responseHandler.Failed<CelebirtyAwardDTO>($"An error occurred while creating the Award: {ex.Message}");
            }
        }

        public async Task<Response<bool>> DeleteCelebirtyAwardAsync(Guid awardId)
        {
            if (awardId == Guid.Empty)
            {
                return _responseHandler.BadRequest<bool>("Award ID cannot be empty.");
            }
            var award = await _awardRepository.GetByIdAsync(awardId, false);
            if (award == null)
            {
                return _responseHandler.NotFound<bool>($"Award with ID {awardId} not found.");
            }
            var image = await _imageRepository.GetByIdAsync(award.PosterId, false);
            if (image != null)
            {
                var deleteResult = await _imageUploaderService.DeleteImageByUrlAsync(image.Url);
                if (!deleteResult)
                {
                    return _responseHandler.Failed<bool>("Failed to delete award poster image.");
                }
                else
                {
                    await _imageRepository.DeleteAsync(image);
                    await _awardRepository.DeleteAsync(award);
                    return _responseHandler.Success(true, "Award deleted successfully.");
                }
            }
            else
            {
                await _awardRepository.DeleteAsync(award);
                return _responseHandler.Success(true, "Award deleted successfully ,No poster Found to deleted.");
            }

        }
        #endregion
    }
}
