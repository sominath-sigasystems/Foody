using Foody.Models;
using Foody.Models.ViewModels;
using Foody.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Foody.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthRepository(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterViewModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // ✅ Assign the selected role during registration
                await _userManager.AddToRoleAsync(user, model.UserRole);
            }

            return result;
        }

        public async Task<bool> LoginAsync(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                model.RememberMe,
                lockoutOnFailure: false);

            return result.Succeeded;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> ForgotPasswordAsync(ForgotPasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            // ✅ Security: Don't reveal if user exists or not
            if (user == null)
            {
                return true;
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            // TODO: Integrate email service (SendGrid, Azure, SMTP, etc.)
            // Example implementation:
            // var callbackUrl = $"https://yourapp.com/Account/ResetPassword?code={Uri.EscapeDataString(code)}&email={Uri.EscapeDataString(model.Email)}";
            // await _emailService.SendPasswordResetEmailAsync(model.Email, callbackUrl);

            return true;
        }

        public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = "Invalid password reset request."
                });
            }

            return await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
        }

        public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        // ✅ NEW: Assign role to existing user (for Admin to assign DeliveryBoy)
        public async Task<bool> AssignRoleAsync(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return false;
            }

            // Remove existing roles first (users should have only one primary role)
            var currentRoles = await _userManager.GetRolesAsync(user);
            if (currentRoles.Any())
            {
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);
            return result.Succeeded;
        }

        // ✅ NEW: Get user's roles (for navbar display, authorization checks)
        public async Task<IList<string>> GetUserRolesAsync(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }
    }
}