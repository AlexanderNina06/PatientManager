using Microsoft.AspNetCore.Mvc;
using PatientMgmt.Core.Application;
using PatientMgmt.Middlewares;

namespace PatientMgmt.Controllers
{
    public class UserController : Controller
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
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

            AuthenticationResponse userVm = await _userService.LoginAsync(vm);
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

        public async Task<IActionResult> LogOut()
        {
            await _userService.SignOutAsync();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new {Controller = "User", Action="Index"});
        }


    }
}
