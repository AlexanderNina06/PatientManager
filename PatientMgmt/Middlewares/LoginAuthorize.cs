using System;
using Microsoft.AspNetCore.Mvc.Filters;
using PatientMgmt.Controllers;

namespace PatientMgmt.Middlewares;

public class LoginAuthorize : IAsyncActionFilter
{
    private readonly ValidateUserSession _userSession;
    public LoginAuthorize(ValidateUserSession validateUserSession)
{
    _userSession = validateUserSession;
}
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (_userSession.HasUser())
        {
            var controller = (UserController)context.Controller;
            context.Result = controller.RedirectToAction("index", "home");
        }
        else
        {
            await next();
        }
    }
}
