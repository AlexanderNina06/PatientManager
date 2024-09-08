using System;
using PatientMgmt.Core.Application.DTOs.User;

namespace PatientMgmt.Core.Application.Interfaces.Services;

public interface IUserService
{
    Task<List<UserDto>> GetAllUsersAsync();
    Task CreateUserAsync(CreateUserDto dto);
    Task UpdateUserAsync(UpdateUserDto dto, int userId);
    Task DeleteUserAsync(int userId);
}
