using EyeScenceApp.Application.Bases;
using EyeScenceApp.Application.DTOs.ApplicationUser;
using EyeScenceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Application.IServices
{
    public interface IUserService
    {
        Task<Response<UserDTO?>> AddApplicationUser(CreateApplicationUserDTO dto);
        Task<Response<UserDTO?>> GetApplicationUserByIdAsync(string id);
        Task<Response<UserDTO?>> GetApplicationUserByEmailAsync(string email);
        Task<Response<IEnumerable<UserDTO>>> GetAllApplicationUsersAsync();
        Task<Response<UserDTO?>> UpdateApplicationUserAsync(UpdateUserDTO userdto);
        Task<Response<bool>> DeleteApplicationUserAsync(string id);
        Task<Response<IEnumerable<ApplicationAdminDTO>>> GetAllAdmins();
    }
}
