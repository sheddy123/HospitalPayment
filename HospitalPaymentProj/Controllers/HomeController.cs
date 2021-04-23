using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HospitalPaymentProj.Models;
using HospitalPaymentProj.Repository.IRepository;
using HospitalPaymentProj.Helper;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace HospitalPaymentProj.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepository _userRepo;

        public HomeController(ILogger<HomeController> logger, IUserRepository userRepo)
        {
            _logger = logger;
            _userRepo = userRepo;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register_Login()
        {
            return View();
        }

        
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync("LoginScheme");
            HttpContext.Session.Clear();
            return RedirectToAction("Register_Login");
        }
        

        [HttpPost]
        public async Task<IActionResult> RegisterLogin(Users user)
        {
            
            try
            {
                
                if (user.Password != null)
                {
                    user.Password = PasswordEncrypt.textToEncrypt(user.Password);
                    var _registerUser = await _userRepo.CreateAsync(StaticDetails._registerUserPath, user);

                    if (_registerUser == false)
                    {
                        TempData["ErrorUser"] = "Error creating user";
                        return RedirectToAction(nameof(Register_Login));
                    }
                }
                if (user.PasswordLogin != null)
                {
                    user.PasswordLogin = PasswordEncrypt.textToEncrypt(user.PasswordLogin);
                    var _authenticateModel = await _userRepo.Authenticate(StaticDetails._loginPath, user);
                    
                    if (_authenticateModel == "0")
                    {
                        TempData["Error"] = "Please input correct username or password";
                        return RedirectToAction(nameof(Register_Login));
                    }
                    HttpContext.Session.SetString("UserID", _authenticateModel);
                }
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
                
                var principal = new ClaimsPrincipal(identity);
                TempData["Success"] = "Created successfully";
                await HttpContext.SignInAsync("LoginScheme", principal);
                return RedirectToAction("Index", "Patient");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
