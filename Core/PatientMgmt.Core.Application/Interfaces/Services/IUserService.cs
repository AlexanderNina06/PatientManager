namespace PatientMgmt.Core.Application;

public interface IUserService
{
Task<AuthenticationResponse> LoginAsync(LoginViewModel vm);
Task<RegisterResponse> RegisterAsync(SaveUserViewModel vm, string origin);
Task SignOutAsync();
}
