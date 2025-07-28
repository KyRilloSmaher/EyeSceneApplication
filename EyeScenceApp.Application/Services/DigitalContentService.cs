using AutoMapper;
using EyeScenceApp.Application.Bases;
using EyeScenceApp.Application.DTOs.DigitalContent;
using EyeScenceApp.Application.IServices;
using EyeScenceApp.Domain.Entities;
using EyeScenceApp.Domain.RepositoriesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.Services
{
    public class DigitalContentService :IDigitalContentService
    {
        #region Feild(s)
        private readonly IGenreRepository _genreRepository;
        private readonly ICastRepository _castRepository;
        private readonly IMapper _mapper;
        private readonly IDigitalContentRepository _digitalContentRepository;
        private readonly IAwardRepository _awardRepository;
        private readonly ResponseHandler _responseHandler;
        #endregion

        #region Constructor(s)
        public DigitalContentService(IGenreRepository genreRepository, ICastRepository castRepository, IMapper mapper, IDigitalContentRepository digitalContentRepository, ResponseHandler responseHandler, IAwardRepository awardRepository)
        {
            _genreRepository = genreRepository;
            _castRepository = castRepository;
            _mapper = mapper;
            _digitalContentRepository = digitalContentRepository;
            _responseHandler = responseHandler;
            _awardRepository = awardRepository;
        }
        #endregion

        #region Method(s)
        public async Task<Response<bool>> AddGenreToDigitalContentAsync(AddGenreToDigitalContentRequest request)
        {
            if (request == null)
            {
                return _responseHandler.BadRequest<bool>("Request cannot be null.");
            }

            if (request.DigitalContentId == Guid.Empty)
            {
                return _responseHandler.BadRequest<bool>("Digital content ID cannot be empty.");
            }

            if (request.GenreId <= 0)
            {
                return _responseHandler.BadRequest<bool>("Invalid genre ID.");
            }

            var ExistDigitalContent = await _digitalContentRepository.IsDigitalContentExist(request.DigitalContentId);
            if (!ExistDigitalContent)
            {
                return _responseHandler.BadRequest<bool>("Digital content does not exist.");
            }
            Genre genre = await _genreRepository.GetByIdAsync(request.GenreId, true);
            if (genre == null) {
                return _responseHandler.BadRequest<bool>("Genre does not exist.");
            }
            DigitalContentGenres DGg = new DigitalContentGenres
            {
                DigitalContentId = request.DigitalContentId,
                GenreId = request.GenreId
            };
             var digtialContent = await _digitalContentRepository.GetDigitalContentByIdAsync(request.DigitalContentId);
            if (digtialContent is not null)
            {
                digtialContent.Genres.Add(DGg);
                await _digitalContentRepository.SaveChangesAsync();
                return _responseHandler.Success(true, "Genre added to digital content successfully.");
            }
            return _responseHandler.BadRequest<bool>("Digital content not found.");
        }

        public async Task<Response<bool>> AddActorToDigitalContentCastAsync(AddActorToDigitalContentCastRequest request)
        {
            if (request == null)
            {
                return _responseHandler.BadRequest<bool>("Request cannot be null.");
            }

            if (request.DigitalContentId == Guid.Empty)
            {
                return _responseHandler.BadRequest<bool>("Digital content ID cannot be empty.");
            }

            if (request.ActorId == Guid.Empty)
            {
                return _responseHandler.BadRequest<bool>("Invalid Actor ID.");
            }

            var ExistDigitalContent = await _digitalContentRepository.IsDigitalContentExist(request.DigitalContentId);
            if (!ExistDigitalContent)
            {
                return _responseHandler.BadRequest<bool>("Digital content does not exist.");
            }
            Actor actor = await _castRepository.GetByIdAsync(request.ActorId, true);
            if (actor == null)
            {
                return _responseHandler.BadRequest<bool>("Actor does not exist.");
            }
            MovieCast cast = new MovieCast
            {
                DigitalContentId = request.DigitalContentId,
                ActorId = request.ActorId
            };
            var digtialContent = await _digitalContentRepository.GetDigitalContentByIdAsync(request.DigitalContentId);
            if (digtialContent is not null)
            {
                digtialContent.MovieCasts.Add(cast);
                await _digitalContentRepository.SaveChangesAsync();
                return _responseHandler.Success(true, "Actor added to digital content Cast successfully.");
            }
            return _responseHandler.BadRequest<bool>("Digital content not found.");
        }

        public async Task<Response<bool>> AddCrewMemberToDigitalContentAsync(AddCrewMemberFromDigitalContentCastRequest request)
        {
            if (request == null)
            {
                return _responseHandler.BadRequest<bool>("Request cannot be null.");
            }

            if (request.DigitalContentId == Guid.Empty)
            {
                return _responseHandler.BadRequest<bool>("Digital content ID cannot be empty.");
            }

            if (request.CrewId == Guid.Empty)
            {
                return _responseHandler.BadRequest<bool>("Invalid Crew ID.");
            }

            var ExistDigitalContent = await _digitalContentRepository.GetByIdAsync(request.DigitalContentId);
            if (ExistDigitalContent is null)
            {
                return _responseHandler.BadRequest<bool>("Digital content does not exist.");
            }
            Crew? crew = await _digitalContentRepository.GetCrewAsync(request.CrewId, true);
            if (crew == null)
            {
                return _responseHandler.BadRequest<bool>("Crew Member does not exist.");
            }
            var result = await _digitalContentRepository.AddToDigitalContentCrewAsync(ExistDigitalContent,crew);
            if (result)
            {
                return _responseHandler.Created(true, "Crew Member added to digital content successfully.");
            }
            return _responseHandler.Failed<bool>(" Failed to add Crew Member  to digital content.");
        }
        public async Task<Response<bool>> AddAwardToDigitalContentAsync(AddAwardToDigitalContentRequest request)
        {
            if (request == null)
            {
                return _responseHandler.BadRequest<bool>("Request cannot be null.");
            }

            if (request.DigitalContentId == Guid.Empty)
            {
                return _responseHandler.BadRequest<bool>("Digital content ID cannot be empty.");
            }

            if (request.AwardId == Guid.Empty)
            {
                return _responseHandler.BadRequest<bool>("Invalid Award ID.");
            }
            if( request.Year < 1900 || request.Year > DateTime.UtcNow.Year) 
            {
                return _responseHandler.BadRequest<bool>("Invalid Year.");
            }

            var Award = await _awardRepository.GetByIdAsync(request.AwardId , true);

            if (Award is null)
            {
                return _responseHandler.BadRequest<bool>(" Award is Not Exist ");
            }
            var DigitalContent = await _digitalContentRepository.GetByIdAsync(request.DigitalContentId , true);
            if (DigitalContent is null)
            {
                return _responseHandler.BadRequest<bool>(" Digital Content is Not Exist ");
            }

            var result = await _awardRepository.AddAwardToDigitalContentAsync(Award, DigitalContent,request.Year);

            if(result)
                return _responseHandler.Created<bool>(result , "Award Added To Digital Content SuccessFully");
            return _responseHandler.Failed<bool>("Failed To Added Award To Digital Content SuccessFully");

        }

        public async Task<Response<bool>> DeleteGenreFromDigitalContentAsync(DeleteGenreFromDigitalContentRequest request)
        {
            if (request == null)
            {
                return _responseHandler.BadRequest<bool>("Request cannot be null.");
            }

            if (request.DigitalContentId == Guid.Empty)
            {
                return _responseHandler.BadRequest<bool>("Digital content ID cannot be empty.");
            }

            if (request.GenreId <=0)
            {
                return _responseHandler.BadRequest<bool>("Invalid genre ID.");
            }

            var ExistDigitalContent = await _digitalContentRepository.IsDigitalContentExist(request.DigitalContentId);
            if (!ExistDigitalContent)
            {
                return _responseHandler.BadRequest<bool>("Digital content does not exist.");
            }
            Genre genre = await _genreRepository.GetByIdAsync(request.GenreId, true);
            if (genre == null)
            {
                return _responseHandler.BadRequest<bool>("genre does not exist.");
            }

            var digtialContent = await _digitalContentRepository.GetDigitalContentByIdAsync(request.DigitalContentId);
            if (digtialContent == null)
            {
                return _responseHandler.BadRequest<bool>("Digital content not found.");
            }
            if (!digtialContent.Genres.Any(mc => mc.Genre.Id == request.GenreId))
            {
                return _responseHandler.BadRequest<bool>("Genre is part of the digital content genres.");
            }
            // Remove the genre from the digital content genres
            var DCg = digtialContent.Genres.FirstOrDefault(mc => mc.Genre.Id == request.GenreId);
            digtialContent.Genres.Remove(DCg);
            await _digitalContentRepository.SaveChangesAsync();
            return _responseHandler.Success(true, "genre removed from digital content Genres successfully.");
        }

        public async Task<Response<bool>> DeleteActorFromDigitalContentCastAsync(DeleteActorFromDigitalContentCastRequest request)
        {
            if (request == null)
            {
                return _responseHandler.BadRequest<bool>("Request cannot be null.");
            }

            if (request.DigitalContentId == Guid.Empty)
            {
                return _responseHandler.BadRequest<bool>("Digital content ID cannot be empty.");
            }

            if (request.ActorId == Guid.Empty)
            {
                return _responseHandler.BadRequest<bool>("Invalid Actor ID.");
            }

            var ExistDigitalContent = await _digitalContentRepository.IsDigitalContentExist(request.DigitalContentId);
            if (!ExistDigitalContent)
            {
                return _responseHandler.BadRequest<bool>("Digital content does not exist.");
            }
            Actor actor = await _castRepository.GetByIdAsync(request.ActorId, true);
            if (actor == null)
            {
                return _responseHandler.BadRequest<bool>("Actor does not exist.");
            }

            var digtialContent = await _digitalContentRepository.GetDigitalContentByIdAsync(request.DigitalContentId);
            if (digtialContent == null)
            {
                return _responseHandler.BadRequest<bool>("Digital content not found.");
            }
            if (!digtialContent.MovieCasts.Any(mc => mc.ActorId == request.ActorId))
            {
                return _responseHandler.BadRequest<bool>("Actor is not part of the digital content cast.");
            }
            // Remove the actor from the digital content cast
            var movieCast = digtialContent.MovieCasts.FirstOrDefault(mc => mc.ActorId == request.ActorId);
            digtialContent.MovieCasts.Remove(movieCast);
            await _digitalContentRepository.SaveChangesAsync();
            return _responseHandler.Success(true, "Actor removed from digital content cast successfully.");
        }

        public async Task<Response<bool>> DeleteCrewMemberFromDigitalContentAsync(DeleteCrewMemberFromDigitalContentCastRequest request)
        {
            if (request == null)
            {
                return _responseHandler.BadRequest<bool>("Request cannot be null.");
            }

            if (request.DigitalContentId == Guid.Empty)
            {
                return _responseHandler.BadRequest<bool>("Digital content ID cannot be empty.");
            }

            if (request.CrewId == Guid.Empty)
            {
                return _responseHandler.BadRequest<bool>("Invalid Crew Member ID.");
            }
            Crew? crew = await _digitalContentRepository.GetCrewAsync(request.CrewId, true);
            if (crew == null)
            {
                return _responseHandler.BadRequest<bool>("Crew Member does not exist.");
            }

            var digtialContent = await _digitalContentRepository.GetDigitalContentByIdAsync(request.DigitalContentId);
            if (digtialContent == null)
            {
                return _responseHandler.BadRequest<bool>("Digital content not found.");
            }
            if (!digtialContent.WorksOn.Any(mc => mc.CrewId == request.CrewId))
            {
                return _responseHandler.BadRequest<bool>("Crew Member is not part of the digital content Crew.");
            }
            var crewdc = digtialContent.WorksOn.FirstOrDefault(mc => mc.CrewId == request.CrewId);
            digtialContent.WorksOn.Remove(crewdc);
            await _digitalContentRepository.SaveChangesAsync();
            return _responseHandler.Success(true, "Crew Member removed from digital content  successfully.");
        }

        public async Task<Response<bool>> DeleteAwardFromDigitalContentAsync(DeleteAwardFromDigitalContentRequest request)
        {
            if (request == null)
            {
                return _responseHandler.BadRequest<bool>("Request cannot be null.");
            }

            if (request.DigitalContentId == Guid.Empty)
            {
                return _responseHandler.BadRequest<bool>("Digital content ID cannot be empty.");
            }

            if (request.AwardId == Guid.Empty)
            {
                return _responseHandler.BadRequest<bool>("Invalid Award ID.");
            }
            if (request.Year < 1900 || request.Year > DateTime.UtcNow.Year)
            {
                return _responseHandler.BadRequest<bool>("Invalid Year.");
            }

            var Award = await _awardRepository.GetByIdAsync(request.AwardId, true);

            if (Award is null)
            {
                return _responseHandler.BadRequest<bool>(" Award is Not Exist ");
            }
            var DigitalContent = await _digitalContentRepository.GetByIdAsync(request.DigitalContentId, true);
            if (DigitalContent is null)
            {
                return _responseHandler.BadRequest<bool>(" Digital Content is Not Exist ");
            }

            var result = await _awardRepository.RemoveAwardFromDigitalContentAsync(request.AwardId, request.DigitalContentId, request.Year);

            if (result)
                return _responseHandler.Created<bool>(result, "Award Removed From Digital Content SuccessFully");
            return _responseHandler.Failed<bool>("Failed To Remove Award From Digital Content SuccessFully");
        }

        #endregion

    }
}
