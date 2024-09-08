using System;
using PatientMgmt.Core.Application.DTOs.User;
using PatientMgmt.Core.Application.Interfaces.Services;

namespace PatientMgmt.Core.Application.services;

public class ApplicationUserService : IUserService
{
    private readonly IUserService _userService;

    public ApplicationUserService(IUserService userService)
    {
      _userService = userService;
    }
    public async Task<List<UserDto>> GetAllUsersAsync()
    {
      return await _userService.GetAllUsersAsync();
    }

    public Task CreateUserAsync(CreateUserDto dto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUserAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUserAsync(UpdateUserDto dto, int userId)
    {
        throw new NotImplementedException();
    }
}
