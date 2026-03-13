using Foody.Models;
using Foody.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Foody.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<IdentityResult> RegisterAsync(RegisterViewModel model);
        Task<bool> LoginAsync(LoginViewModel model);
        Task LogoutAsync();
        Task<bool> ForgotPasswordAsync(ForgotPasswordViewModel model);
        Task<IdentityResult> ResetPasswordAsync(ResetPasswordViewModel model);
        Task<ApplicationUser?> GetUserByEmailAsync(string email);
    }
}
