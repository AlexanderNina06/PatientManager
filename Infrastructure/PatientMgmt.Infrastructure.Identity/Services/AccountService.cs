using Microsoft.AspNetCore.Identity;
using PatientMgmt.Core.Application;
using PatientMgmt.Infrastructure.Identity.Entities;
using PatientMgmt.Core.Domain;
namespace PatientMgmt.Infrastructure.Identity;

public class AccountService : IAccountService
{
  //User Manger: Update, Creation, Delete
  private readonly UserManager<ApplicationUser> _userMangaer;
  //SignInManger: LogIn and LogOut
  private readonly SignInManager<ApplicationUser> _SignInManager;

  public AccountService (UserManager<ApplicationUser> userMangaer, SignInManager<ApplicationUser> signInManager)
  {
    _userMangaer = userMangaer;
    _SignInManager = signInManager;
  }
    public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
    {
      AuthenticationResponse response = new();

      var user = await _userMangaer.FindByNameAsync(request.UserName);
      if(user == null)
      {
        response.HasError = true;
        response.Error = $"No account registered with {request.UserName} is found.";
        return response;
      }

      var result = await _SignInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
      if(!result.Succeeded)
      {
        response.HasError = true;
        response.Error= $"Invalid Credentials for {request.UserName}";
        return response;
      }
      
      response.Id = user.Id;
      response.Email = user.Email;  
      response.UserName = user.UserName;
      var roleList = await _userMangaer.GetRolesAsync(user).ConfigureAwait(false);
      response.Roles = roleList.ToList();
    
      return response;
    }

    public Task<string> ConfirmAcountAsync(string userId, string token)
    {
        throw new NotImplementedException();
    }

    public async Task<RegisterResponse> RegisterAdminUserAsync(RegisterRequest request, string origin)
    {
      RegisterResponse response = new()
      {
        HasError = false
      };

      var userWithSameUserName = await _userMangaer.FindByNameAsync(request.UserName);
      if(userWithSameUserName != null)
      {
        response.HasError = true;
        response.Error = $"User name '{request.UserName}' already Exist.";
        return response;
      }

      var userWithSameEmail = await _userMangaer.FindByEmailAsync(request.Email);
      if(userWithSameEmail != null)
      {
        response.HasError = true;
        response.Error= $"Email {request.Email} is already registered.";
        return response;
      }

      var user = new ApplicationUser
      {
        Email = request.Email,
        FirstName = request.FirstName,  
        LastName = request.LastName,  
        UserName = request.UserName,
      };

      var result = await _userMangaer.CreateAsync(user, request.Password);
      if(result.Succeeded)
      {
         await _userMangaer.AddToRoleAsync(user, Roles.Admin.ToString());
      }
      else
      {
        response.HasError = true;
        response.Error = "An error has ocurred during process to register your user";
        return response;
      }
      
      return response;
    }

    public async Task SignOutAsync()
    {
        await _SignInManager.SignOutAsync();
    }
}
