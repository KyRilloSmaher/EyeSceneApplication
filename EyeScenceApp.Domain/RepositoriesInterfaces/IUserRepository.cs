using EyeScenceApp.Domain.Bases;
using EyeScenceApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Domain.RepositoriesInterfaces
{
    public interface IApplicationUserRepository : IGenericRepository<ApplicationUser>
    {
            // Transaction Management
            Task BeginTransactionAsync();

            Task CommitTransactionAsync();
            Task RollbackTransactionAsync();

            // Basic User Operations
            Task<ApplicationUser?> GetByIdAsync(string ApplicationUserId, bool trackChanges = false);
            Task<ApplicationUser?> GetByEmailAsync(string email, bool trackChanges = false);
            Task<ApplicationUser?> GetByApplicationUsernameAsync(string ApplicationUsername, bool trackChanges = false);
            Task<IEnumerable<ApplicationUser>> GetAllApplicationUsersAsync();
            Task<IEnumerable<ApplicationUser>> GetAllAdminsAsync();
            Task<IdentityResult> CreateApplicationUserAsync(ApplicationUser ApplicationUser, string password);
            Task<IdentityResult> UpdateApplicationUserAsync(ApplicationUser ApplicationUser);
            Task<IdentityResult> DeleteApplicationUserAsync(ApplicationUser ApplicationUser);

            // ApplicationUser Verification
            Task<bool> IsEmailUniqueAsync(string email);
            Task<bool> IsApplicationUsernameUniqueAsync(string ApplicationUsername);

            // Password Management
            Task<bool> CheckPasswordAsync(ApplicationUser ApplicationUser, string password);
            Task<bool> ChangePasswordAsync(ApplicationUser ApplicationUser, string currentPassword, string newPassword);
            Task<string> GeneratePasswordResetTokenAsync(ApplicationUser ApplicationUser);
            Task<bool> ResetPasswordAsync(ApplicationUser ApplicationUser, string token, string newPassword);
            Task RemovePasswordAsync(ApplicationUser ApplicationUser);
            Task<bool> HasPasswordAsync(ApplicationUser ApplicationUser);
            Task<IdentityResult> AddPasswordAsync(ApplicationUser ApplicationUser, string password);
            // Role Management
            Task<IList<string>> GetApplicationUserRolesAsync(ApplicationUser ApplicationUser);
            Task<bool> AddToRoleAsync(ApplicationUser ApplicationUser, string role);
            Task<bool> RemoveFromRoleAsync(ApplicationUser ApplicationUser, string role);
            Task<bool> IsInRoleAsync(ApplicationUser ApplicationUser, string role);

            // Email Confirmation
            Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser ApplicationUser);
            Task<bool> ConfirmEmailAsync(string email, string token);
            Task<bool> IsEmailConfirmedAsync(ApplicationUser ApplicationUser);

            // Lockout/Account Status
            Task<bool> SetLockoutEnabledAsync(ApplicationUser ApplicationUser, bool enabled);
            Task<bool> IsLockedOutAsync(ApplicationUser ApplicationUser);
            Task<DateTimeOffset?> GetLockoutEndDateAsync(ApplicationUser ApplicationUser);
            Task<bool> SetLockoutEndDateAsync(ApplicationUser ApplicationUser, DateTimeOffset? lockoutEnd);
            Task<int> GetAccessFailedCountAsync(ApplicationUser ApplicationUser);
            Task<bool> ResetAccessFailedCountAsync(ApplicationUser ApplicationUser);
        
    }
}
