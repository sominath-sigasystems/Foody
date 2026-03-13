using Foody.Models;
using Foody.Models.ViewModels;
using Foody.Services.Interfaces;
using Foody.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Foody.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(
            IAuthService authService,
            UserManager<ApplicationUser> userManager)
        {
            _authService = authService;
            _userManager = userManager;
        }

        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            // If already logged in, redirect based on role
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToRoleBasedPage();
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                if (await _authService.LoginAsync(model))
                {
                    // ✅ Role-based redirection after successful login
                    return await RedirectToRoleBasedPageAsync(model.Email);
                }

                ModelState.AddModelError(string.Empty, "Invalid email or password.");
            }

            return View(model);
        }

        // GET: /Account/Register
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string? returnUrl = null)
        {
            // If already logged in, redirect based on role
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToRoleBasedPage();
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                // ✅ Validate role selection (security: prevent tampering)
                if (!model.AvailableRoles.Contains(model.UserRole))
                {
                    ModelState.AddModelError(nameof(model.UserRole), "Invalid role selection.");
                    return View(model);
                }

                var result = await _authService.RegisterAsync(model);

                if (result)
                {
                    // Auto-login after successful registration
                    await _authService.LoginAsync(new LoginViewModel
                    {
                        Email = model.Email,
                        Password = model.Password
                    });

                    // ✅ Role-based redirection after registration
                    return await RedirectToRoleBasedPageAsync(model.Email);
                }

                ModelState.AddModelError(string.Empty, "Registration failed. Please check details and try again.");
            }

            return View(model);
        }

        // POST: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        // GET: /Account/ForgotPassword
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                // ✅ Security: Always return success to prevent user enumeration
                await _authService.RequestPasswordResetAsync(model);
                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }
            return View(model);
        }

        // GET: /Account/ForgotPasswordConfirmation
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        // GET: /Account/ResetPassword
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string? code = null)
        {
            if (string.IsNullOrEmpty(code))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var model = new ResetPasswordViewModel { Code = code };
            return View(model);
        }

        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _authService.ResetPasswordAsync(model);

            if (result)
            {
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }

            ModelState.AddModelError(string.Empty, "Error resetting password. The token may have expired.");
            return View(model);
        }

        // GET: /Account/ResetPasswordConfirmation
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        // GET: /Account/Profile
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _authService.GetUserByEmailAsync(User.Identity?.Name ?? "");

            if (user == null)
            {
                return NotFound();
            }

            // Pass roles to view for display
            ViewBag.UserRoles = await _authService.GetUserRolesAsync(user);
            return View(user);
        }

        // ✅ HELPER: Role-based redirection logic (CRITICAL for multi-role app)
        private async Task<IActionResult> RedirectToRoleBasedPageAsync(string email)
        {
            var user = await _authService.GetUserByEmailAsync(email);

            if (user == null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var roles = await _authService.GetUserRolesAsync(user);
            var primaryRole = roles.FirstOrDefault();

            return primaryRole switch
            {
                AppRoles.User => RedirectToAction("Index", "Home"),
                AppRoles.DeliveryBoy => RedirectToAction("PendingOrders", "Delivery"),
                AppRoles.RestaurantOwner => RedirectToAction("Dashboard", "Restaurant"),
                _ => RedirectToAction("Index", "Home")
            };
        }

        // ✅ HELPER: Synchronous version for already-authenticated checks
        private IActionResult RedirectToRoleBasedPage()
        {
            // Note: For production, consider caching roles in Claims to avoid DB hit
            return RedirectToAction("Index", "Home"); // Default fallback
        }

        // ✅ HELPER: Safe local URL redirect
        private IActionResult RedirectToLocal(string? returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}