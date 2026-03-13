using Foody.Models;
using Foody.Models.ViewModels;

namespace Foody.Services.Interfaces
{
    public interface IAuthService
    {
        // Authentication Methods
        Task<bool> RegisterAsync(RegisterViewModel model);
        Task<bool> LoginAsync(LoginViewModel model);
        Task LogoutAsync();

        // Password Management
        Task<bool> RequestPasswordResetAsync(ForgotPasswordViewModel model);
        Task<bool> ResetPasswordAsync(ResetPasswordViewModel model);

        // ✅ NEW: Role & User Management (for role-based redirection)
        Task<ApplicationUser?> GetUserByEmailAsync(string email);
        Task<IList<string>> GetUserRolesAsync(ApplicationUser user);
        Task<bool> AssignRoleToUserAsync(string email, string roleName);
    }
}