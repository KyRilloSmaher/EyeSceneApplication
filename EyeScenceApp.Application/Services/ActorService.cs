using AutoMapper;
using EyeScenceApp.Application.Bases;
using EyeScenceApp.Application.DTOs.Actors;
using EyeScenceApp.Application.DTOs.Awards.CelebirtyAwards;
using EyeScenceApp.Application.DTOs.DigitalContent;
using EyeScenceApp.Application.IServices;
using EyeScenceApp.Domain.Entities;
using EyeScenceApp.Domain.Enums;
using EyeScenceApp.Domain.RepositoriesInterfaces;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;


namespace EyeScenceApp.Application.Services
{
    public class ActorService : IActorService
    {
        #region Field(s)
        private readonly ICastRepository _castRepository;
        private readonly IAwardRepository _awardRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IImageUploaderService _imageUploader;
        private readonly ResponseHandler _responseHandler;
        private IMapper _mapper;
        #endregion

        #region Constructor(s)
        public ActorService(ICastRepository castRepository, IAwardRepository awardRepository, ResponseHandler responseHandler, IMapper mapper, IImageRepository imageRepository, IImageUploaderService imageUploader)
        {
            _castRepository = castRepository ?? throw new ArgumentNullException(nameof(castRepository), "castRepository cannot be null.");
            _awardRepository = awardRepository ?? throw new ArgumentNullException(nameof(awardRepository), "awardRepository cannot be null.");
            _responseHandler = responseHandler ?? throw new ArgumentNullException(nameof(responseHandler), "responseHandler cannot be null.");
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), "mapper cannot be null.");
            _imageRepository = imageRepository ?? throw new ArgumentNullException(nameof(responseHandler), "imageRepository cannot be null.");
            _imageUploader = imageUploader ?? throw new ArgumentNullException(nameof(responseHandler), "imageUploader cannot be null.");
        }

        #endregion


        #region Method(s)



        public async Task<Response<ActorDTO?>> GetActorByIdAsync(Guid actorId)
        {
            if (actorId == Guid.Empty)
            {
                return _responseHandler.BadRequest<ActorDTO?>("Actor ID cannot be empty.");
            }

            var actor = await _castRepository.GetByIdAsync(actorId,false);
            if (actor is null)
            {
                return _responseHandler.NotFound<ActorDTO?>($"Actor with ID {actorId} not found.");
            }

            var actorDto = _mapper.Map<ActorDTO>(actor);

            return _responseHandler.Success<ActorDTO?>(actorDto);
        }
        public async  Task<Response<ICollection<ActorDTO>>> SearchActorAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return _responseHandler.BadRequest<ICollection<ActorDTO>>("Name cannot be null or empty.");
            }

            var actors = await _castRepository.GetActorsByNameAsync(name);
            if (actors is null || !actors.Any())
            {
                return _responseHandler.NotFound<ICollection<ActorDTO>>($"No actors found with name {name}.");
            }

            var actorDtos = _mapper.Map<ICollection<ActorDTO>>(actors);
            return _responseHandler.Success<ICollection<ActorDTO>>(actorDtos);
        }

        public async Task<Response<ICollection<ActorDTO>>> GetAllActorsAsync()
        {
            var actors = await _castRepository.GetAllAsync();
            if (actors is null)
            {
                return _responseHandler.NotFound<ICollection<ActorDTO>>($"No actors found.");
            }
            var actorDtos = _mapper.Map<ICollection<ActorDTO>>(actors);
            return _responseHandler.Success<ICollection<ActorDTO>>(actorDtos);
        }
        public async Task<Response<ICollection<ActorDTO>>> FilterActorAsync(ActorFilterDTO dto)
        {
            if (dto == null)
            {
                return _responseHandler.BadRequest<ICollection<ActorDTO>>("Filter DTO cannot be null.");
            }

            // Build the predicate directly with && operators
            Expression<Func<Actor, bool>> searchPredicate = actor =>
                (string.IsNullOrEmpty(dto.Name) ||
                 (actor.FirstName != null && actor.FirstName.Contains(dto.Name)) ||
                 (actor.MiddleName != null && actor.MiddleName.Contains(dto.Name)) ||
                 (actor.LastName != null && actor.LastName.Contains(dto.Name)))
                &&
                (dto.sex == null || actor.Sex == dto.sex)
                &&
                (dto.nationality == null || actor.Nationality == dto.nationality);

            Expression<Func<Actor, string>> orderBy = actor => actor.FirstName; // default ordering

            bool ascending = dto.ascendenig;

            var actorEntities = await _castRepository.FilterListAsync(orderBy, searchPredicate, ascending);

            if (actorEntities == null)
            {
                return _responseHandler.NotFound<ICollection<ActorDTO>>("No actors found matching the criteria.");
            }

            var actorDTOs = _mapper.Map<ICollection<ActorDTO>>(actorEntities);

            return actorDTOs == null
                ? _responseHandler.UnprocessableEntity<ICollection<ActorDTO>>("Error mapping actors.")
                : _responseHandler.Success(actorDTOs);
        }
        public async Task<Response<ICollection<CelebirtyAwardDTO>>> GetActorAwardsAsync(Guid actorId)
        {
            if (actorId == Guid.Empty)
            {
                return _responseHandler.BadRequest<ICollection<CelebirtyAwardDTO>>("Actor ID cannot be empty.");
            }

            var actor = await _castRepository.GetByIdAsync(actorId, false);
            if (actor is null)
            {
                return _responseHandler.NotFound<ICollection<CelebirtyAwardDTO>>($"Actor with ID {actorId} not found.");
            }

            var awards = await _awardRepository.GetAwardsByCelebrityAsync(actor.Id);
            if (awards is null )
            {
                return _responseHandler.NotFound<ICollection<CelebirtyAwardDTO>>($"No awards found for actor with ID {actorId}.");
            }

            var awardDtos = _mapper.Map<ICollection<CelebirtyAwardDTO>>(awards);
            return _responseHandler.Success<ICollection<CelebirtyAwardDTO>>(awardDtos);
        }
        public async Task<Response<ICollection<DigitalContentDTO>>> GetActorWorksAsync(Guid actorId)
        {
            if (actorId == Guid.Empty)
            {
                return _responseHandler.BadRequest<ICollection<DigitalContentDTO>>("Actor ID cannot be empty.");
            }
            var actor = await _castRepository.GetByIdAsync(actorId, false);
            if (actor is null)
            {
                return _responseHandler.NotFound<ICollection<DigitalContentDTO>>($"Actor with ID {actorId} not found.");
            }
            var works = await _castRepository.GetActorWorksAsync(actor.Id);
            if (works is null)
            {
                return _responseHandler.NotFound<ICollection<DigitalContentDTO>>($"No works found for actor with ID {actorId}.");
            }
            var workDtos = _mapper.Map<ICollection<DigitalContentDTO>>(works);
            return _responseHandler.Success<ICollection<DigitalContentDTO>>(workDtos);
        }
        public async Task<Response<bool>> CreateActorAsync(CreateActorDTO actorDto)
        {
            // Validate input
            if (actorDto == null)
            {
                return _responseHandler.BadRequest<bool>(false, "Actor DTO cannot be null.");
            }

            if (actorDto.PrimaryImage == null)
            {
                return _responseHandler.BadRequest<bool>(false, "Actor PrimaryImage cannot be null.");
            }

            try
            {
                // Begin transaction
                await using var transaction = await _castRepository.BeginTransactionAsync();

                try
                {
                    // Map and add actor
                    var actor = _mapper.Map<Actor>(actorDto);
                    var addedActor = await _castRepository.AddAsync(actor);

                    // Upload primary image
                    var primaryImageResult = await _imageUploader.UploadImageAsync(
                        actorDto.PrimaryImage,
                        ImageFolder.CelebirtiesImages);

                    if (primaryImageResult.Error is not null )
                    {
                        await transaction.RollbackAsync();
                        return _responseHandler.BadRequest<bool>(false, "Failed to upload primary image.");
                    }

                    // Create image record
                    var primaryImage = new Image
                    {
                        IsPrimary = true,
                        Url = primaryImageResult.Url.ToString()
                    };

                    var addedImage = await _imageRepository.AddAsync(primaryImage);

                    // Link image to actor
                    var actorPrimaryImage = new CelebirtyImages
                    {
                        CelebirtyId = addedActor.Id,
                        ImageId = addedImage.Id
                    };

                    await _imageRepository.AddCelebirtyImage(actorPrimaryImage);

                    // Handle additional images if provided
                    if (actorDto.Images != null && actorDto.Images.Any())
                    {
                        foreach (var image in actorDto.Images)
                        {
                            var additionalImageResult = await _imageUploader.UploadImageAsync(
                                image,
                                ImageFolder.CelebirtiesImages);

                            if (additionalImageResult.Error  is not null)
                            {
                                var additionalImage = new Image
                                {
                                    IsPrimary = false,
                                    Url = additionalImageResult.Url.ToString()
                                };

                                var addedAdditionalImage = await _imageRepository.AddAsync(additionalImage);

                                await _imageRepository.AddCelebirtyImage(new CelebirtyImages
                                {
                                    CelebirtyId = addedActor.Id,
                                    ImageId = addedAdditionalImage.Id
                                });
                            }
                        }
                    }

                    // Commit transaction
                    await transaction.CommitAsync();

                    return _responseHandler.Success<bool>(true, "Actor created successfully.");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    //_logger.LogError(ex, "Failed to create actor");
                    return _responseHandler.BadRequest<bool>(false, "An error occurred while creating the actor.");
                }
            }
            catch (Exception ex)
            {
               // _logger.LogError(ex, "Failed to begin transaction");
                return _responseHandler.BadRequest<bool>(false, "An error occurred while starting the transaction.");
            }
        }
        public async Task<Response<bool>> UpdateActorAsync(UpdateActorDTO actorDto)
        {
            if (actorDto == null)
            {
                return _responseHandler.BadRequest<bool>(false, "Actor DTO cannot be null.");

            }
            if (actorDto.Id == Guid.Empty)
            {
                return _responseHandler.BadRequest<bool>(false, "Actor ID cannot be empty.");
            }
            var existingActor = await _castRepository.GetByIdAsync(actorDto.Id, true);
             _mapper.Map(actorDto, existingActor);
             await _castRepository.UpdateAsync(existingActor);   
             return _responseHandler.Success<bool>(true, "Actor created successfully.");
        }

        public async Task<Response<bool>> DeleteActorAsync(Guid actorId)
        {
            if (actorId == Guid.Empty)
            {
                return _responseHandler.BadRequest<bool>(false, "Actor ID cannot be empty.");
            }

            var actor = await _castRepository.GetByIdAsync(actorId, true);
            if (actor is null)
            {
                return _responseHandler.NotFound<bool>($"Actor with ID {actorId} not found.");
            }

            await _castRepository.DeleteAsync(actor);
            return _responseHandler.Deleted<bool>(true,"Actor deleted successfully.");
        }
     

        #endregion
    }
}
