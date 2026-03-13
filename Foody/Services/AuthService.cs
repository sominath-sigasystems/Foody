using Foody.Models.ViewModels;
using Foody.Repositories.Interfaces;
using Foody.Services.Interfaces;

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
    }
}
