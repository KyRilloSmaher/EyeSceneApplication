using EyeScenceApp.Domain.Entities;
using EyeScenceApp.Domain.RepositoriesInterfaces;
using EyeScenceApp.Infrastructure.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeScenceApp.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<ApplicationUser>, IApplicationUserRepository
    {
        #region Feild(s)
        private readonly UserManager<ApplicationUser> _ApplicationUserManager;
        private readonly ApplicationDbContext _context;
        #endregion

        #region Constructor(s)
        public UserRepository(UserManager<ApplicationUser> ApplicationUserManager, ApplicationDbContext context) : base(context)
        {
            _ApplicationUserManager = ApplicationUserManager ?? throw new ArgumentNullException(nameof(ApplicationUserManager));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        #endregion

        #region Method(s)
        public async Task BeginTransactionAsync()
        {
            await _context.Database.BeginTransactionAsync();
        }
        public async Task CommitTransactionAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        public async Task<ApplicationUser?> GetByIdAsync(string ApplicationUserId, bool trackChanges = false)
        {
            return trackChanges
                ? await _ApplicationUserManager.Users.SingleOrDefaultAsync(u => u.Id == ApplicationUserId)
                : await _ApplicationUserManager.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Id == ApplicationUserId);
        }

        public async Task<ApplicationUser?> GetByEmailAsync(string email, bool trackChanges = false)
        {
            return trackChanges
                ? await _ApplicationUserManager.Users.SingleOrDefaultAsync(u => u.Email == email)
                : await _ApplicationUserManager.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Email == email);
        }
        public async Task<ApplicationUser?> GetByApplicationUsernameAsync(string ApplicationUsername, bool trackChanges = false)
        {
            return trackChanges
                ? await _ApplicationUserManager.Users.SingleOrDefaultAsync(u => u.UserName == ApplicationUsername)
                : await _ApplicationUserManager.Users.AsNoTracking().SingleOrDefaultAsync(u => u.UserName == ApplicationUsername);
        }
        public async Task<IEnumerable<ApplicationUser>> GetAllApplicationUsersAsync()
        {
            return await _ApplicationUserManager.GetUsersInRoleAsync("User");
        }
        public async Task<IEnumerable<ApplicationUser>> GetAllAdminsAsync()
        {
            return await _ApplicationUserManager.GetUsersInRoleAsync("Admin");
        }

        // Basic ApplicationUser Operations

        public async Task<IdentityResult> CreateApplicationUserAsync(ApplicationUser ApplicationUser, string password)
        {
            var result = await _ApplicationUserManager.CreateAsync(ApplicationUser, password);
            return result;
        }

        public async Task<IdentityResult> UpdateApplicationUserAsync(ApplicationUser ApplicationUser)
        {
            var result = await _ApplicationUserManager.UpdateAsync(ApplicationUser);
            return result;
        }

        public async Task<IdentityResult> DeleteApplicationUserAsync(ApplicationUser ApplicationUser)
        {
            var result = await _ApplicationUserManager.DeleteAsync(ApplicationUser);
            return result;
        }

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            var ApplicationUser = await _ApplicationUserManager.FindByEmailAsync(email);
            return ApplicationUser == null;
        }

        public async Task<bool> IsApplicationUsernameUniqueAsync(string ApplicationUsername)
        {
            var ApplicationUser = await _ApplicationUserManager.FindByNameAsync(ApplicationUsername);
            return ApplicationUser == null;
        }

        public async Task<bool> CheckPasswordAsync(ApplicationUser ApplicationUser, string password)
        {
            return await _ApplicationUserManager.CheckPasswordAsync(ApplicationUser, password);
        }

        public async Task<bool> ChangePasswordAsync(ApplicationUser ApplicationUser, string currentPassword, string newPassword)
        {
            var result = await _ApplicationUserManager.ChangePasswordAsync(ApplicationUser, currentPassword, newPassword);
            return result.Succeeded;
        }

        public async Task<string> GeneratePasswordResetTokenAsync(ApplicationUser ApplicationUser)
        {
            return await _ApplicationUserManager.GeneratePasswordResetTokenAsync(ApplicationUser);
        }

        public async Task<bool> ResetPasswordAsync(ApplicationUser ApplicationUser, string token, string newPassword)
        {
            var result = await _ApplicationUserManager.ResetPasswordAsync(ApplicationUser, token, newPassword);
            return result.Succeeded;
        }

        public async Task<IList<string>> GetApplicationUserRolesAsync(ApplicationUser ApplicationUser)
        {
            return await _ApplicationUserManager.GetRolesAsync(ApplicationUser);
        }

        public async Task<bool> AddToRoleAsync(ApplicationUser ApplicationUser, string role)
        {
            var result = await _ApplicationUserManager.AddToRoleAsync(ApplicationUser, role);
            return result.Succeeded;
        }

        public async Task<bool> RemoveFromRoleAsync(ApplicationUser ApplicationUser, string role)
        {
            var result = await _ApplicationUserManager.RemoveFromRoleAsync(ApplicationUser, role);
            return result.Succeeded;
        }

        public async Task<bool> IsInRoleAsync(ApplicationUser ApplicationUser, string role)
        {
            return await _ApplicationUserManager.IsInRoleAsync(ApplicationUser, role);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser ApplicationUser)
        {
            return await _ApplicationUserManager.GenerateEmailConfirmationTokenAsync(ApplicationUser);
        }

        public async Task<bool> ConfirmEmailAsync(string email, string token)
        {
            var ApplicationUser = await GetByEmailAsync(email, true);

            if (ApplicationUser != null)
            {
                // _context.Entry(ApplicationUser).State = EntityState.Detached;

                var result = await _ApplicationUserManager.ConfirmEmailAsync(ApplicationUser, token);
                if (result.Succeeded)
                {
                    ApplicationUser.EmailConfirmed = true;
                    await _ApplicationUserManager.UpdateAsync(ApplicationUser);
                }
                return result.Succeeded;
            }
            return false;
        }

        public async Task<bool> IsEmailConfirmedAsync(ApplicationUser ApplicationUser)
        {
            return await _ApplicationUserManager.IsEmailConfirmedAsync(ApplicationUser);
        }

        public async Task<bool> SetLockoutEnabledAsync(ApplicationUser ApplicationUser, bool enabled)
        {
            var result = await _ApplicationUserManager.SetLockoutEnabledAsync(ApplicationUser, enabled);
            return result.Succeeded;
        }

        public async Task<bool> IsLockedOutAsync(ApplicationUser ApplicationUser)
        {
            return await _ApplicationUserManager.IsLockedOutAsync(ApplicationUser);
        }

        public async Task<DateTimeOffset?> GetLockoutEndDateAsync(ApplicationUser ApplicationUser)
        {
            return await _ApplicationUserManager.GetLockoutEndDateAsync(ApplicationUser);
        }

        public async Task<bool> SetLockoutEndDateAsync(ApplicationUser ApplicationUser, DateTimeOffset? lockoutEnd)
        {
            var result = await _ApplicationUserManager.SetLockoutEndDateAsync(ApplicationUser, lockoutEnd);
            return result.Succeeded;
        }

        public async Task<int> GetAccessFailedCountAsync(ApplicationUser ApplicationUser)
        {
            return await _ApplicationUserManager.GetAccessFailedCountAsync(ApplicationUser);
        }

        public async Task<bool> ResetAccessFailedCountAsync(ApplicationUser ApplicationUser)
        {
            var result = await _ApplicationUserManager.ResetAccessFailedCountAsync(ApplicationUser);
            return result.Succeeded;
        }

        // Additional custom methods not directly available in ApplicationUserManager
        public async Task<IEnumerable<ApplicationUser>> GetApplicationUsersByRoleAsync(string role)
        {
            return await _ApplicationUserManager.GetUsersInRoleAsync(role);
        }

        public async Task<IEnumerable<ApplicationUser>> SearchApplicationUsersAsync(string searchTerm)
        {
            return await _ApplicationUserManager.Users
                .Where(u =>
                    u.Email.Contains(searchTerm) ||
                    u.UserName.Contains(searchTerm))
                .ToListAsync();
        }

        public async Task RemovePasswordAsync(ApplicationUser ApplicationUser)
        {
            await _ApplicationUserManager.RemovePasswordAsync(ApplicationUser);
        }

        public async Task<bool> HasPasswordAsync(ApplicationUser ApplicationUser)
        {
            return await _ApplicationUserManager.HasPasswordAsync(ApplicationUser);
        }

        public Task<IdentityResult> AddPasswordAsync(ApplicationUser ApplicationUser, string password)
        {
            var result = _ApplicationUserManager.AddPasswordAsync(ApplicationUser, password);
            return result;
        } 
        #endregion
    }
}