using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AbsoluteCinema.Models.ViewModels;
using System.Threading.Tasks;
using System.Linq;

namespace AbsoluteCinema.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userMgr,
                                 SignInManager<IdentityUser> signInMgr)
        {
            userManager = userMgr;
            signInManager = signInMgr;
        }

        [AllowAnonymous]
        public ViewResult Login(string returnUrl)
        {
            return View(new LoginModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser? user = await userManager.FindByEmailAsync(loginModel.Email);

                if (user != null)
                {
                    await signInManager.SignOutAsync();

                    var result = await signInManager.PasswordSignInAsync(user,
                        loginModel.Password, isPersistent: false, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        return Redirect(loginModel.ReturnUrl ?? "/");
                    }
                }

                ModelState.AddModelError("", "Невірний Email або пароль.");
            }
            return View(loginModel);
        }

        [AllowAnonymous]
        public ViewResult Register() => View(new RegisterModel());

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Profile");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }
        public async Task<IActionResult> Profile()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var model = new ProfileModel { Email = user.Email ?? string.Empty };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(ProfileModel model)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            if (!string.IsNullOrEmpty(model.NewPassword) || !string.IsNullOrEmpty(model.ConfirmNewPassword))
            {
                if (model.NewPassword != model.ConfirmNewPassword)
                {
                    ModelState.AddModelError("", "Новий пароль та його підтвердження не збігаються.");
                    model.Email = user.Email ?? string.Empty;
                    return View(model);
                }

                var changePasswordResult = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

                if (!changePasswordResult.Succeeded)
                {
                    foreach (var error in changePasswordResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    model.Email = user.Email ?? string.Empty;
                    return View(model);
                }

                await signInManager.RefreshSignInAsync(user);
                TempData["message"] = "Ваш пароль успішно змінено!";
                return RedirectToAction("Profile");
            }

            TempData["message"] = "Дані оновлено успішно.";
            return RedirectToAction("Profile");
        }
    }
}