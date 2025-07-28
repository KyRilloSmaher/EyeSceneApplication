using AutoMapper;
using CloudinaryDotNet.Actions;
using EyeScenceApp.Application.Bases;
using EyeScenceApp.Application.DTOs.ApplicationUser;
using EyeScenceApp.Application.IServices;
using EyeScenceApp.Domain.Entities;
using EyeScenceApp.Domain.RepositoriesInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationUserRepository _ApplicationUserRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUrlHelper _urlHelper;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ResponseHandler _responseHandler;

        public UserService(
            IApplicationUserRepository ApplicationUserRepository,
            IAuthenticationService authenticationService,
            IEmailService emailService,
            IHttpContextAccessor httpContextAccessor,
            IUrlHelper urlHelper,
            IMapper mapper,
            ResponseHandler responseHandler)
        {
            _ApplicationUserRepository = ApplicationUserRepository;
            _authenticationService = authenticationService;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
            _urlHelper = urlHelper;
            _mapper = mapper;
            _responseHandler = responseHandler;
        }





        public async Task<Response<UserDTO?>> AddApplicationUser(CreateApplicationUserDTO dto)
        {
            try
            {
                if (dto is null)
                    return _responseHandler.BadRequest<UserDTO?>("Dto is Null");
                await _ApplicationUserRepository.BeginTransactionAsync();
                //Check If Email is Exists
                var existApplicationUser = await _ApplicationUserRepository.GetByEmailAsync(dto.Email);
                if (existApplicationUser != null) return _responseHandler.Failed<UserDTO?>( "Email Already Exists");

                //Check If ApplicationUsername Exists
                var ApplicationUserByApplicationUserName = await _ApplicationUserRepository.GetByApplicationUsernameAsync(dto.UserName);
                if (ApplicationUserByApplicationUserName != null) return _responseHandler.Failed<UserDTO?>("Application UserName Already Exists ");

                //Create ApplicationUser
                var user = _mapper.Map<ApplicationUser>(dto);
                var result = await _ApplicationUserRepository.CreateApplicationUserAsync(user, dto.Password);
                //Failed to Create ApplicationUser
                if (!result.Succeeded)
                    return _responseHandler.Failed<UserDTO?>(string.Join(",", result.Errors.Select(x => x.Description).ToList()));

                await _ApplicationUserRepository.AddToRoleAsync(user, "USER");

                //Send Confirm Email
                var code = await _ApplicationUserRepository.GenerateEmailConfirmationTokenAsync(user);
                var resquestAccessor = _httpContextAccessor.HttpContext.Request;
                var returnUrl = resquestAccessor.Scheme + "://" + resquestAccessor.Host + _urlHelper.Action("ConfirmEmail", "Authentication", new { email = user.Email, code = code });
                await _emailService.SendConfirmationEmailAsync(user.Email, returnUrl);

                await _ApplicationUserRepository.CommitTransactionAsync();
                
                return _responseHandler.Success<UserDTO?>(_mapper.Map<UserDTO?>(user),"Success");

            }
            catch (Exception ex)
            {
                _ApplicationUserRepository.RollbackTransactionAsync();
                return _responseHandler.Failed<UserDTO?>("Failed To Add User");
            }
        }

        public async Task<Response<UserDTO?>> GetApplicationUserByIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return _responseHandler.BadRequest<UserDTO?>("ApplicationUser ID cannot be empty");

            var ApplicationUser = await _ApplicationUserRepository.GetByIdAsync(id,false);
            if (ApplicationUser == null)
            {
                return _responseHandler.Failed<UserDTO?>($"ApplicationUser not found with ID: {id}");
            }
            var user = _mapper.Map<UserDTO>(ApplicationUser);
            return _responseHandler.Success<UserDTO?>(user, "User Retrieved SuccessFully");
        }

        public async Task<Response<UserDTO?>> GetApplicationUserByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return _responseHandler.BadRequest<UserDTO?>("ApplicationUser Email cannot be empty");
            var ApplicationUser = await _ApplicationUserRepository.GetByEmailAsync(email);
            if (ApplicationUser == null)
            {

                return _responseHandler.Failed<UserDTO?>($"ApplicationUser not found with email: {email}");
            }
            var user = _mapper.Map<UserDTO>(ApplicationUser);
            return _responseHandler.Success<UserDTO?>(user, "User Retrieved SuccessFully");
        }

        public async Task<Response<IEnumerable<UserDTO>>> GetAllApplicationUsersAsync()
        {
            var users = await _ApplicationUserRepository.GetAllApplicationUsersAsync();

            if( users == null)
                return _responseHandler.Failed<IEnumerable<UserDTO>>("Can not retrieve Users");
            var usersDTOs = _mapper.Map<IEnumerable<UserDTO>>(users);

            return _responseHandler.Success<IEnumerable<UserDTO>>(usersDTOs);
        }

        public async Task<Response<UserDTO?>> UpdateApplicationUserAsync(UpdateUserDTO userdto)
        {
            if (userdto == null)
                return _responseHandler.BadRequest<UserDTO?>("Dto is Null");

            var ExistingUser = await _ApplicationUserRepository.GetByIdAsync(userdto.Id);
            if (ExistingUser == null)
                return _responseHandler.Failed<UserDTO?>("user not Found");
            _mapper.Map(userdto, ExistingUser);
            var result = await _ApplicationUserRepository.UpdateApplicationUserAsync(ExistingUser);
            if (!result.Succeeded)
            {
                return _responseHandler.Failed<UserDTO?>("Error Happend when update ApplicationUser");
            }
            return _responseHandler.Success<UserDTO?>(_mapper.Map<UserDTO?>(ExistingUser), "User Login SuccessFully");
        }

        public async Task<Response<IEnumerable<ApplicationAdminDTO>>> GetAllAdmins()
        {
            var admins = await _ApplicationUserRepository.GetAllAdminsAsync();

            if (admins == null)
                return _responseHandler.Failed<IEnumerable<ApplicationAdminDTO>>("Can not retrieve Admins");
            var usersDTOs = _mapper.Map<IEnumerable<ApplicationAdminDTO>>(admins);

            return _responseHandler.Success<IEnumerable<ApplicationAdminDTO>>(usersDTOs);
        }
        public async Task<Response<bool>> DeleteApplicationUserAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return _responseHandler.BadRequest<bool>(false,"Id is Null");

            var ApplicationUser = await _ApplicationUserRepository.GetByIdAsync(id, true);
            if (ApplicationUser == null)
                return _responseHandler.Failed<bool>("user not Found");

            await _ApplicationUserRepository.DeleteApplicationUserAsync(ApplicationUser);
            return _responseHandler.Deleted<bool>(true);
        }

    }
}
