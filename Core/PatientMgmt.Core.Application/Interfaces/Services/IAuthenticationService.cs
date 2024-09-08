using PatientMgmt.Core.Application.ViewModels.Users;

namespace PatientMgmt.Core.Application;

public interface IAuthenticationService
{
Task<AuthenticationResponse> LoginAsync(LoginViewModel vm);
Task<RegisterResponse> RegisterAsync(SaveUserViewModel vm, string origin);
Task SignOutAsync();
}
