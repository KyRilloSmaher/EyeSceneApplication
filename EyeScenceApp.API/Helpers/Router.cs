using EyeScenceApp.Application.Bases;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EyeScenceApp.API.Helpers
{

        public static class Router
        {
            public const string SignleRoute = "/{Id:GUID}";
            public const string root = "EyeSceneAPI";
            public const string Rule = root + "/";
            public static ObjectResult FinalResponse<T>(Response<T> response)
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            return new OkObjectResult(response);
                        case HttpStatusCode.Created:
                            return new CreatedResult(string.Empty, response);
                        case HttpStatusCode.Unauthorized:
                            return new UnauthorizedObjectResult(response);
                        case HttpStatusCode.BadRequest:
                            return new BadRequestObjectResult(response);
                        case HttpStatusCode.NotFound:
                            return new NotFoundObjectResult(response);
                        case HttpStatusCode.Accepted:
                            return new AcceptedResult(string.Empty, response);
                        case HttpStatusCode.UnprocessableEntity:
                            return new UnprocessableEntityObjectResult(response);
                        default:
                            return new BadRequestObjectResult(response);
                    }
                }

        #region  EndPoints-Actor
        public static class Actor
                {
                    //******** HttpGet ********//
                    public const string Prefix = Rule + "Actor";
                    public const string GetById = Prefix + SignleRoute;
                    public const string GetAll = Prefix + "/All";
                    public const string GetAllAuthorPaginted = Prefix + "/Paginted";
                    public const string GetActorAwards = Prefix  + SignleRoute + "/Awards";
                    public const string GetActorWorks = Prefix  + SignleRoute + "/Works";
                    public const string SearchActorsByName = Prefix + "/Search-Actors/{name}";
                    public const string TotalActors = Prefix + "/Actors-count";
                    public const string FilterActors = Prefix + "/Filter-Actors";
    
                    //******** HttpPost ********//
                    public const string CreateActor = Prefix + "/Create";

                    //******** HttpPut ********//
                    public const string UpdateActor = Prefix + "/Update";

                    //******** HttpDelete ********//
                    public const string DeleteActor = Prefix + "/Delete" + SignleRoute;


                }


        #endregion

        #region EndPoints-Awards

        public static class Award
        {
            //******** HttpGet ********//
            public const string Prefix = Rule + "Awards";
            public const string GetById = Prefix + SignleRoute;
            public const string GetAll = Prefix + "/All";
            public const string GetAwardsByOrganization = Prefix + "/By-Organization/{organizationName}";
            public const string GetAwardsByCategory = Prefix + "/By-Category/{category}";
            public const string GetAwardByName = Prefix + "/By-Name/{name}";
            public const string TotalAwards = Prefix + "/Awards-count";

                    // Digital Content Awards
                    public const string GetDigitalContentAwardById = Prefix + "/DigitalContent" + SignleRoute;
                    public const string GetAllDigitalContentAwards = Prefix + "/DigitalContent/All";
                    public const string GetDigitalContentAwardsByContent = Prefix + "/DigitalContent/By-Content/{digitalContentId}";

        

                    // Celebrity Awards
                    public const string GetCelebirtyAwardById = Prefix + "/Celebrity" + SignleRoute;
                    public const string GetAllCelebirtyAwards = Prefix + "/Celebrity/All";
                    public const string GetAwardsByCelebrity = Prefix + "/Celebrity/By-Celebrity/{celebrityId}";
          
 

            //******** HttpPost ********//
            public const string CreateDigitalContentAward = Prefix + "/DigitalContent/Create";
            public const string CreateCelebirtyAward = Prefix + "/Celebrity/Create";
            public const string AddAwardToCelebrity = Prefix + "/Celebrity/Add-To-Celebrity";
            public const string AddAwardToDigitalContent = Prefix + "/DigitalContent/Add-To-Content";

            //******** HttpPut ********//
            public const string UpdateDigitalContentAward = Prefix + "/DigitalContent/Update";
            public const string UpdateCelebirtyAward = Prefix + "/Celebrity/Update";

            //******** HttpDelete ********//
            public const string DeleteDigitalContentAward = Prefix + "/DigitalContent/Delete" + SignleRoute;
            public const string DeleteCelebirtyAward = Prefix + "/Celebrity/Delete" + SignleRoute;
            public const string RemoveAwardFromDigitalContent = Prefix + "/DigitalContent/Remove-From-Content";
            public const string RemoveAwardFromCelebrity = Prefix + "/Celebrity/Remove-From-Celebrity";
        }

        #endregion

        #region  EndPoints-Genres
        public static class Genres {
            //******** HttpGet ********//
            public const string Prefix = Rule + "Genres";
            public const string GetById = Prefix + "/{Id:int}";
            public const string GetAll = Prefix + "/All";
            public const string GetGenreByName = Prefix + "/{name}";

            //******** HttpPost ********//
            public const string CreateGenre = Prefix + "/Create";

            //******** HttpPut ********//
            public const string UpdateGenre = Prefix + "/Update";

            //******** HttpDelete ********//
            public const string DeleteGenre = Prefix + "/Delete" + "/{Id:int}";

        }
        #endregion

        #region  EndPoints-Favorites
        public static class Favorites
        {
            //******** HttpGet ********//
            public const string Prefix = Rule + "Favorites";
            public const string GetAllFavoritesOfUser = Prefix + "/All"+"/{Id}";
            public const string IsExistinFavorites = Prefix + "/IsExistsInFavoritesList";

            //******** HttpPost ********//
            public const string AddToFavoritesList = Prefix + "/Create";

            //******** HttpPut ********//
           

            //******** HttpDelete ********//
            public const string DeleteFromFavoritesList = Prefix + "/Delete"  ;
            public const string ClearFavorites = Prefix + "/Clear" + "/{Id}";

        }
        #endregion

        #region  EndPoints-WatchList
        public static class WatchList
        {
            //******** HttpGet ********//
            public const string Prefix = Rule + "WatchList";
            public const string GetAllForUser = Prefix + "/All" + "/{Id}";
            public const string IsInWatchList = Prefix + "/IsExistsInWatchList";

            //******** HttpPost ********//
            public const string AddToWatchList = Prefix + "/Create";

            //******** HttpPut ********//


            //******** HttpDelete ********//
            public const string DeleteFromWatchList = Prefix + "/Delete";
            public const string ClearWatchList = Prefix + "/Clear" + "/{Id}";
        }
        #endregion

        #region EndPoints-Rates
    
        public static class Rates
        {
            //******** HttpGet ********//
            public const string Prefix = Rule + "Rates";
            public const string GetAllForUser = Prefix + "/User-Rates" + "/{Id}";
            public const string GetRateById = Prefix + SignleRoute;
            public const string GetRateByDigitalContentId = Prefix + "/Digital-Content-Rates" + "/{Id}";
            public const string GetTopRatedDigitalContent = Prefix + "/Top-Rated-DigitalContent";
            public const string GetTopRatedDigitalContentInGenre = Prefix + "/Top-Rated-DigitalContent-In-Genre"+ "/{name}";
           

            //******** HttpPost ********//
            public const string CreateRate = Prefix + "/Create";

            //******** HttpPut ********//
            public const string UpdateRate = Prefix + "/Update" ;
            public const string LikeRate = "EyeScence/Rates/Like/{Id}";
            public const string RemoveLikeRate = "EyeScence/Rates/Unlike/{Id}";
            public const string DisLikeRate = "EyeScence/Rates/Dislike/{Id}";
            public const string RemoveDisLikeRate = "EyeScence/Rates/Undislike/{Id}";

            //******** HttpDelete ********//
            public const string DeleteRate = Prefix + "/Delete" + SignleRoute;
            public const string ClearUserRatesList = Prefix + "/Clear-For-User" + "/{Id}";
        }
        #endregion

        #region EndPoints-Documantries
        public static class Documantries
        {
            //******** Base Prefix ********//
            public const string Prefix = Rule + "Documantries";

            //******** HttpGet ********//
            public const string GetDocumantryById = Prefix + SignleRoute;
            public const string GetAll = Prefix + "/All";
            public const string SearchDocumantries = Prefix + "/search-By-Title";
            public const string SearchDocumantriesByCountry = Prefix + "/Filter-by-country";
            public const string GetAllDocumantriesByReleaseYear = Prefix + "/Filter-by-release-year";
            public const string TopRatedDocumantries = Prefix + "/Top-Rated";
            public const string NewReleaseDocumantries = Prefix + "/New-Release";
            public const string GetDocumantriesByGenre = Prefix + "/Genre/{genreId:int}";
            //******** HttpPost ********//
            public const string CreateDocumantry = Prefix + "/Create";

            //******** HttpPut ********//
            public const string UpdateDocumantry = Prefix + "/Update";

            //******** HttpDelete ********//
            public const string DeleteDocumantry = Prefix + "/Delete" + SignleRoute;
        }


        #endregion

        #region EndPoints-Movies
        public static class Movies
        {
            //******** Base Prefix ********//
            public const string Prefix = Rule + "Movies";

            //******** HttpGet ********//
            public const string GetMovieById = Prefix + SignleRoute;
            public const string GetAll = Prefix + "/All";
            public const string SearchMovies = Prefix + "/search-By-Title";
            public const string SearchMoviesByCountry = Prefix + "/Filter-by-country";
            public const string GetAllMoviesByReleaseYear = Prefix + "/Filter-by-release-year";
            public const string TopRatedMovies= Prefix + "/Top-Rated";
            public const string GetMovieCast = Prefix + SignleRoute+"/Cast";
            public const string NewReleaseMovies = Prefix + "/New-Release";
            public const string OrderMoviesByRevenues = Prefix + "/orderd-By-Revenues";
            public const string GetMoviesByGenre = Prefix + "/Genre/{genreId:int}";
            //******** HttpPost ********//
            public const string CreateMovie = Prefix + "/Create";

            //******** HttpPut ********//
            public const string UpdateMovie = Prefix + "/Update";

            //******** HttpDelete ********//
            public const string DeleteMovie = Prefix + "/Delete" + SignleRoute;
        }
        #endregion

        #region EndPoints-Series

        public static class Series
        {
            //******** Base Prefix ********//
            public const string Prefix = Rule + "Series";

            //******** HttpGet ********//
            public const string GetAll = Prefix + "/All";
            public const string GetById = Prefix + SignleRoute;
            public const string GetByTitle = Prefix + "/ByTitle/{title}";
            public const string SearchSeriesByCountry = Prefix + "/Filter-by-country";
            public const string GetAllSeriesByReleaseYear = Prefix + "/Filter-by-release-year";
            public const string TopRatedSeries = Prefix + "/Top-Rated";
            public const string NewReleaseSeries = Prefix + "/New-Release";
            public const string GetSeriesByGenre = Prefix + "/Genre/{genreId:int}";
            public const string GetSeriesEpisodes = Prefix + SignleRoute + "/Episodes";
            public const string GetEpisodeId = "/Episode" + SignleRoute;
            public const string GetSeriesEpisodesInSeason = Prefix + SignleRoute + "/Episodes/{seasonNum}";
            public const string GetNewRealse = Prefix + "/New-Release-Series";                  
            public const string GetTopRated = Prefix + "/TopRated";      

            //******** HttpPost ********//
            public const string CreateSeries = Prefix + "/Create";
            public const string AddEpisode = Prefix + "/AddEpisode";

            //******** HttpPut ********//
            public const string UpdateSeries = Prefix + SignleRoute + "/Update";
            public const string UpdateEpisode = Rule+"Episode" + SignleRoute + "/Update";

            //******** HttpDelete ********//
            public const string DeleteSeries = Prefix + SignleRoute + "/Delete";
            public const string DeleteEpisode = Rule+"Episode" + SignleRoute + "/Delete";
        }


        #endregion

        #region EndPoints-DigitalContent
        public static class DigitalContents
        {
            //******** Base Prefix ********//
            public const string Prefix = Rule + "DigitalContents";

            //******** HttpGet ********//
            public const string GetAll = Prefix + "/All";
            public const string GetById = Prefix + SignleRoute;
            public const string GetByTitle = Prefix + "/ByTitle/{title}";
            public const string GetDigitalContentAwards = Prefix + "/Awards";
            public const string GetCastMembers = Prefix + SignleRoute + "/Cast";
            public const string GetDirectors = Prefix + SignleRoute + "/Directors";
            public const string GetWriters = Prefix + SignleRoute + "/Writers";
            public const string GetProducers = Prefix + SignleRoute + "/Producers";
            public const string GetEditors = Prefix + SignleRoute + "/Editors";
            public const string GetSoundDesigners = Prefix + SignleRoute + "/SoundDesigners";

            //******** HttpPost ********//
            public const string AddActorToDigitalContentCast = Prefix + SignleRoute + "/Add-Cast-member" + "/{actorId:GUID}";
            public const string AddCrewMemberToDigitalContent = Prefix + SignleRoute + "/Add-Crew-member" + "/{crewId:GUID}";
            public const string AddGenreToDigitalContent = Prefix + SignleRoute + "/Add-Genre" + "/{genreId:int}";
            public const string AddAwardToDigitalContent = Prefix + "/Add-Award";

            //******** HttpPut ********//


            //******** HttpDelete ********//
            public const string Delete = Prefix + SignleRoute + "/Delete";
            public const string RemoveDigitalContentAward = Prefix+ SignleRoute +"/Rempve-Award" + "{awardId}";
            public const string RemoveActor = Prefix + SignleRoute + "/Remove-Cast-member" + "/{actorId:GUID}";
            public const string RemoveAward = Prefix + SignleRoute + "/Remove-Award" + "/{awardId:GUID}";
            public const string RemoveCrewMember= Prefix + SignleRoute + "/Remove-Crew-member" + "/{crewId:GUID}";
            public const string RemoveGenre = Prefix + SignleRoute + "/Remove-Genre" + "/{genreId:int}";
        }

        #endregion

        #region EndPoints-Users
        public static class UserAccount
        {
            public const string Prefix = Rule + "User";
            public const string GetById = Prefix + "/{userId}";
            public const string GetAllUsers = Prefix + "/All-Users";
            public const string GetAllAdmins = Prefix + "/All-Admins";
            public const string GetByEmail = Prefix + "/Email/{email}";
            public const string GetByUsername = Prefix + "/Username/{username}";
            public const string GetByPhoneNumber = Prefix + "/PhoneNumber/{phoneNumber}";
            public const string GetUserRoles = Prefix + SignleRoute + "/Roles";
            public const string GetUserClaims = Prefix + SignleRoute + "/Claims";

            //******** HttpPost ********//
            public const string RegisterUser = Prefix + "/Register-User";
            public const string RegisterAdmin = Prefix + "/Register-Admin";
            //******** HttpPut ********//
            public const string UpdateUser = Prefix + "/Update";
            //******** HttpDelete ********//
            public const string DeleteUser = Prefix + "/Delete" + SignleRoute;

        }
        #endregion

        #region   EndPoints-Authentication

        public static class Authentication
        {
            public const string Prefix = Rule + "Authentication";
            public const string Login = Prefix + "/Login";
            public const string ConfirmEmail = Prefix + "/Confirm-Email";
            public const string ForgotPassword = Prefix + "/Forgot-Password";
            public const string ConfirmResetPasswordCode = Prefix + "/Confirm-Reset-Password-Code";
            public const string ChangePassword = Prefix + "/Change-Password";
            public const string SendResetCode = Prefix + "/Send-Reset-Code";
            public const string ResetPassword = Prefix + "/Reset-Password";
            public const string RefreshToken = Prefix + "/Refresh-Token";
        }
        #endregion

        #region  EndPoints-Director
        public static class Director
        {
            //******** HttpGet ********//
            public const string Prefix = Rule + "Crews/Directors";
            public const string GetById = Prefix + SignleRoute;
            public const string GetAll = Prefix + "/All";
            public const string GetDirectorAwards = Prefix + SignleRoute + "/Awards";
            public const string GetDirectorWorks = Prefix + SignleRoute + "/Works";
            public const string SearchDirectorsByName = Prefix + "/Search-Directors/{name}";
            public const string TotalDirectors = Prefix + "/Directors-count";
            public const string FilterDirectors= Prefix + "/Filter-Directors";

            //******** HttpPost ********//
            public const string CreateDirector = Prefix + "/Create";

            //******** HttpPut ********//
            public const string UpdateDirector = Prefix + "/Update";

            //******** HttpDelete ********//
            public const string DeleteDirector = Prefix + "/Delete" + SignleRoute;


        }


        #endregion

        #region  EndPoints-Producer
        public static class Producer
        {
            //******** HttpGet ********//
            public const string Prefix = Rule + "Crews/Producers";
            public const string GetById = Prefix + SignleRoute;
            public const string GetAll = Prefix + "/All";
            public const string GetProducerAwards = Prefix + SignleRoute + "/Awards";
            public const string GetProducerWorks = Prefix + SignleRoute + "/Works";
            public const string SearchProducersByName = Prefix + "/Search-Producers/{name}";
            public const string TotalProducers = Prefix + "/Producers-count";
            public const string FilterProducers = Prefix + "/Filter-Producers";

            //******** HttpPost ********//
            public const string CreateProducer = Prefix + "/Create";

            //******** HttpPut ********//
            public const string UpdateProducer = Prefix + "/Update";

            //******** HttpDelete ********//
            public const string DeleteProducer = Prefix + "/Delete" + SignleRoute;


        }


        #endregion

        #region  EndPoints-Writer
        public static class Writer
        {
            //******** HttpGet ********//
            public const string Prefix = Rule + "Crews/Writers";
            public const string GetById = Prefix + SignleRoute;
            public const string GetAll = Prefix + "/All";
            public const string GetWritersAwards = Prefix + SignleRoute + "/Awards";
            public const string GetWritersWorks = Prefix + SignleRoute + "/Works";
            public const string SearchWritersByName = Prefix + "/Search-Writers/{name}";
            public const string TotalWriters = Prefix + "/Writers-count";
            public const string FilterWriters = Prefix + "/Filter-Writers";

            //******** HttpPost ********//
            public const string CreateWriter = Prefix + "/Create";

            //******** HttpPut ********//
            public const string UpdateWriter = Prefix + "/Update";

            //******** HttpDelete ********//
            public const string DeleteWriter = Prefix + "/Delete" + SignleRoute;


        }
        #endregion
        
        #region  EndPoints-Editor
        public static class Editor
        {
            //******** HttpGet ********//
            public const string Prefix = Rule + "Crews/Editors";
            public const string GetById = Prefix + SignleRoute;
            public const string GetAll = Prefix + "/All";
            public const string GetEditorsAwards = Prefix + SignleRoute + "/Awards";
            public const string GetEditorsWorks = Prefix + SignleRoute + "/Works";
            public const string SearchEditorsByName = Prefix + "/Search-Editors/{name}";
            public const string TotalEditors = Prefix + "/Editors-count";
            public const string FilterEditors = Prefix + "/Filter-Editors";

            //******** HttpPost ********//
            public const string CreateEditor = Prefix + "/Create";

            //******** HttpPut ********//
            public const string UpdateEditor = Prefix + "/Update";

            //******** HttpDelete ********//
            public const string DeleteEditor = Prefix + "/Delete" + SignleRoute;


        }
        #endregion

        #region  EndPoints-SoundDesigner
        public static class SoundDesigner
        {
            //******** HttpGet ********//
            public const string Prefix = Rule + "Crews/SoundDesigners";
            public const string GetById = Prefix + SignleRoute;
            public const string GetAll = Prefix + "/All";
            public const string GetSoundDesignersAwards = Prefix + SignleRoute + "/Awards";
            public const string GetSoundDesignersWorks = Prefix + SignleRoute + "/Works";
            public const string SearchSoundDesignersByName = Prefix + "/Search-SoundDesigners/{name}";
            public const string TotalSoundDesigners = Prefix + "/SoundDesigners-count";
            public const string FilterSoundDesigners = Prefix + "/Filter-SoundDesigners";

            //******** HttpPost ********//
            public const string CreateSoundDesigner = Prefix + "/Create";

            //******** HttpPut ********//
            public const string UpdateSoundDesigner = Prefix + "/Update";

            //******** HttpDelete ********//
            public const string DeleteSoundDesigner = Prefix + "/Delete" + SignleRoute;


        }
        #endregion
    }
}
