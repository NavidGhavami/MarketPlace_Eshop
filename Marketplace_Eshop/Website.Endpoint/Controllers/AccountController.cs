using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Website.EndPoint.Models.ViewModels.User;

namespace Website.EndPoint.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            User newUser = new User()
            {
                Email = model.Email,
                UserName = model.Email,
                FullName = model.Fullname,
                PhoneNumber = model.PhoneNumber,
            };

            var result = _userManager.CreateAsync(newUser, model.Password).Result;

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Profile));
            }

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError(item.Code,item.Description);
            }

            return View(model);
        }

        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult Login(string returnUrl = "/")
        {
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl
            });

        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _userManager.FindByNameAsync(model.Email).Result;

            if (user == null)
            {
                ModelState.AddModelError("","کاربر یافت نشد");
                return View(model);
            }

            _signInManager.SignOutAsync();

            var result = _signInManager.PasswordSignInAsync(user, model.Password, model.IsPersistent, true).Result;

            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl);
            }

            if (result.RequiresTwoFactor)
            {
                
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
