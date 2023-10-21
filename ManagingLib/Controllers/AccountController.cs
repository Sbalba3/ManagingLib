using ManagingLib.DAL.Models;
using ManagingLib.Helpers;
using ManagingLib.ViewModels;
using MangaingLib.BLL.Interfaces;
using MangaingLib.BLL.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Managinglibrary.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IGenericRepo<Error> _genericRepo;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,IGenericRepo<Error> genericRepo)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _genericRepo = genericRepo;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new ApplicationUser()
                    {
                        FName = registerVm.FName,
                        LName = registerVm.LName,
                        UserName = registerVm.Email.Split('@')[0],
                        Email = registerVm.Email,
                        IsAgree = registerVm.IsAgree

                    };
                    var result = await _userManager.CreateAsync(user, registerVm.Pass);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Login));
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }

                }
                catch (Exception ex)
                {
                    var error = new Error() { DateTime = DateTime.Now, Message = ex.Message, ControllerName = "AccountController", ActionName = "Register" };
                    await _genericRepo.Add(error);
                    await _genericRepo.Save();

                }

            }
            return View(registerVm);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByEmailAsync(loginVm.Email);
                    if (user is not null)
                    {
                        var flag = await _userManager.CheckPasswordAsync(user, loginVm.Password);
                        if (flag)
                        {
                            var result = await _signInManager.PasswordSignInAsync(user, loginVm.Password, loginVm.RememberMe, false);
                            if (result.Succeeded)
                            {
                                return RedirectToAction("Index", "Book");
                            }
                        }
                        ModelState.AddModelError(string.Empty, "Password Is Not Correct");

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Email Is Not Existed");

                    }

                }
            }catch (Exception ex)
            {
                var error = new Error() { DateTime = DateTime.Now, Message = ex.Message, ControllerName = "AccountController", ActionName = "Login" };
                await _genericRepo.Add(error);
                await _genericRepo.Save();

            }
            return View();

        }
        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (user is not null)
                    {
                        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                        var passwordRestLink = Url.Action("ResetPassword", "Account", new { email = user.Email, Token = token }, Request.Scheme);
                        var email = new Email()
                        {
                            Subject = "Rest Password",
                            Body = passwordRestLink,
                            To = user.Email
                        };
                        EmailSettings.SendEmail(email);
                        return RedirectToAction(nameof(CheckedYourInbox));
                    }
                    ModelState.AddModelError(string.Empty, "Email Is Not Existed");

                }
                catch (Exception ex)
                {
                    var error = new Error() { DateTime = DateTime.Now, Message = ex.Message, ControllerName = "AccountController", ActionName = "SendEmail" };
                    await _genericRepo.Add(error);
                    await _genericRepo.Save();


                }

            }
            return View(model);
        }
        public IActionResult CheckedYourInbox()
        {
            return View();
        }
    }
}
