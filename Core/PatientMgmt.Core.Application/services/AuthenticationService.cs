
using AutoMapper;
using PatientMgmt.Core.Application.Interfaces.Repositories;
using PatientMgmt.Core.Application.ViewModels.Users;
using PatientMgmt.Core.Domain;

namespace PatientMgmt.Core.Application;

public class AuthenticationService : IAuthenticationService
{
  private readonly IAccountService _accountService;
  private readonly IMapper _mapper;
  public AuthenticationService(IAccountService accountService, IMapper mapper)
  {
    _accountService = accountService;
    _mapper = mapper;
  }
    public async Task<AuthenticationResponse> LoginAsync(LoginViewModel vm)
    {
        AuthenticationRequest loginRequest = _mapper.Map<AuthenticationRequest>(vm);
        AuthenticationResponse userResponse = await _accountService.AuthenticateAsync(loginRequest);
        return userResponse;
    }

    public async Task<RegisterResponse> RegisterAsync(SaveUserViewModel vm, string origin)
    {
        RegisterRequest registerRequest= _mapper.Map<RegisterRequest>(vm);
        if(vm.UserType == Roles.Admin.ToString() || vm.UserType == null)
        {
        return await _accountService.RegisterAdminUserAsync(registerRequest, origin);
        }
        return await _accountService.RegisterUserAsync(registerRequest, origin);
        
    }

    public async Task SignOutAsync()
    {
        await _accountService.SignOutAsync();
    }
}
