using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using News.DataLayer.Context;
using News.Services.Repositories;
using News.ViewModel.Account.Login;
using News.ViewModel.Account.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signmanager;
        private readonly IEmailSend _smssender;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IEmailSend smsSender)
        {
            _userManager = userManager;
            _signmanager = signInManager;
            _smssender = smsSender;
        }
        [HttpGet]
        public IActionResult Register(string returnUrl = null)
        {
            if (_signmanager.IsSignedIn(User))
            {
                return Redirect("/");
            }
            ViewData["returnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    UserName = model.Username,
                    Email = model.Email,
                    EmailConfirmed=true
                };
                var res = await _userManager.CreateAsync(user: user, password: model.Password);
                if (res.Succeeded)
                {
                    // Send Email Message For Confirm Email
                    var EmailConfirmToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var emailMessage = Url.Action("ConfirmEmail", "Account", new { username = user.UserName, token = EmailConfirmToken }, Request.Scheme);
                    await _smssender.Sendsms(model.Email, "تایید ایمیل ~ علی همت نیا", emailMessage);
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    ViewData["title"] = "با موفقیت ثبت نام شد";
                    ViewData["des"] = "لطفا از طریق لینک ارسالی به ایمیل شما اکانت خود را فعال کنید";

                    ViewData["mode"] = "success";
                }
                foreach (var i in res.Errors)
                {
                    ModelState.AddModelError("", i.Description);
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            if (_signmanager.IsSignedIn(User))
            {
                return Redirect("/");
            }
            ViewData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var res = await _signmanager.PasswordSignInAsync(model.Username, model.Password, model.Remem, true);

                if (res.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return Redirect("/Pages/");
                }

                if (res.IsLockedOut)
                {
                    ViewData["ErrorMessage"] = "اکانت شما به دلیل وارد کردن پسورد اشتباه به مدت 5 دقیقه قفل شد";
                    ViewData["mode"] = "warning";
                    return View(model);
                }
                if (res.IsNotAllowed)
                {

                    ViewData["ErrorMessage"] = "ایمیل شما تایید نشده است";
                    ViewData["mode"] = "warning";
                    return View(model);
                }
                
                ViewData["ErrorMessage"] = "رمز عبور یا نام کاربری اشتباه هست";
                ViewData["mode"] = "error";
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _signmanager.SignOutAsync();
            return Redirect("/");
        }
        
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> ConfirmEmail(string token, string username)
        {
            if (string.IsNullOrEmpty(token) && string.IsNullOrEmpty(username))
            {
                return NotFound();
            }
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound();
            }
            var res = await _userManager.ConfirmEmailAsync(user, token);

            return Content(res.Succeeded ? "با موفقیت تایید شد" : "ایمیل تایید نشد");
        }

    }
}
