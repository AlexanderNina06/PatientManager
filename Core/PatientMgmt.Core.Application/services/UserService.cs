
using AutoMapper;

namespace PatientMgmt.Core.Application;

public class UserService : IUserService
{
  private readonly IAccountService _accountService;
  private readonly IMapper _mapper;
  public UserService()
  {

  }
    public Task<AuthenticationResponse> LoginAsync(LoginViewModel vm)
    {
        throw new NotImplementedException();
    }

    public Task<RegisterResponse> RegisterAsync(SaveUserViewModel vm, string origin)
    {
        throw new NotImplementedException();
    }

    public Task SignOutAsync()
    {
        throw new NotImplementedException();
    }
}
