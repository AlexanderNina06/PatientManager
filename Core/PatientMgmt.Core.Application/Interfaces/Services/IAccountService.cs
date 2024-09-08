namespace PatientMgmt.Core.Application;

public interface IAccountService
{
Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
Task<RegisterResponse> RegisterAdminUserAsync(RegisterRequest request, string origin);
Task<RegisterResponse> RegisterUserAsync(RegisterRequest request, string origin);
Task SignOutAsync();
Task<string> ConfirmAcountAsync(string userId, string token);

}
