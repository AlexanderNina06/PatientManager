using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Security;
using PatientMgmt.Core.Application;
using PatientMgmt.Core.Application.Interfaces.Services;
using PatientMgmt.Infrastructure.Identity.Contexts;
using PatientMgmt.Infrastructure.Identity.Entities;
using PatientMgmt.Middlewares;

namespace PatientMgmt.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userMangaer;

        private IAuthenticationService _authenticationService;
        private readonly IdentityContext _identityContext;

        public UserController(IAuthenticationService authenticationService, IdentityContext identityContex, UserManager<ApplicationUser> userMangaer)
        {
            _authenticationService = authenticationService;
            _identityContext = identityContex;
            _userMangaer = userMangaer;
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public ActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
            return View(vm);
            }

            AuthenticationResponse userVm = await _authenticationService.LoginAsync(vm);
            if(userVm != null && userVm.HasError != true)
            {
                HttpContext.Session.Set<AuthenticationResponse>("user", userVm);
                return RedirectToRoute(new { Controller = "Home", Action = "Index" });
            }
            else
            {
              vm.HasError = userVm.HasError;
              vm.Error = userVm.Error;
              return View(vm);
            }
        }
        public async Task<IActionResult> UsersList()
        {
            var users = _userMangaer.Users;
            return View("UsersList", users);
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public async Task<IActionResult> Register()
        { 
            return View("Register", new SaveUserViewModel());
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel vm)
        {
        
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var origin = Request.Headers["origin"];
            RegisterResponse response = await _authenticationService.RegisterAsync(vm, origin);
            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;               
                return View(vm);
            }

            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public async Task<IActionResult> LogOut()
        {
            await _authenticationService.SignOutAsync();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new {Controller = "User", Action="Index"});
        }


    }
}
