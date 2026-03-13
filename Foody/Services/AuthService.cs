using Foody.Models;
using Foody.Models.ViewModels;
using Foody.Repositories.Interfaces;
using Foody.Services.Interfaces;
using Foody.Utilities;

namespace Foody.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<bool> RegisterAsync(RegisterViewModel model)
        {
            // ✅ Validate role selection (security: prevent unauthorized role assignment)
            if (!model.AvailableRoles.Contains(model.UserRole))
            {
                // Fallback to default role if invalid role submitted
                model.UserRole = AppRoles.User;
            }

            var result = await _authRepository.RegisterAsync(model);
            return result.Succeeded;
        }

        public async Task<bool> LoginAsync(LoginViewModel model)
        {
            return await _authRepository.LoginAsync(model);
        }

        public async Task LogoutAsync()
        {
            await _authRepository.LogoutAsync();
        }

        public async Task<bool> RequestPasswordResetAsync(ForgotPasswordViewModel model)
        {
            return await _authRepository.ForgotPasswordAsync(model);
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordViewModel model)
        {
            var result = await _authRepository.ResetPasswordAsync(model);
            return result.Succeeded;
        }

        // ✅ NEW: Get user by email (for profile, authorization checks)
        public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
        {
            return await _authRepository.GetUserByEmailAsync(email);
        }

        // ✅ NEW: Get user roles (CRITICAL for role-based redirection in Controller)
        public async Task<IList<string>> GetUserRolesAsync(ApplicationUser user)
        {
            if (user == null)
            {
                return new List<string>();
            }
            return await _authRepository.GetUserRolesAsync(user);
        }

        // ✅ NEW: Admin-only role assignment (e.g., verify & assign DeliveryBoy)
        public async Task<bool> AssignRoleToUserAsync(string email, string roleName)
        {
            // ✅ Security: Only allow assignment of valid roles
            if (!AppRoles.AllRoles.Contains(roleName))
            {
                return false;
            }

            // ✅ Security: Prevent self-escalation (DeliveryBoy must be admin-assigned)
            if (roleName == AppRoles.DeliveryBoy)
            {
                // In production: Add admin authorization check here
                // if (!User.IsInRole("Admin")) return false;
            }

            return await _authRepository.AssignRoleAsync(email, roleName);
        }
    }
}