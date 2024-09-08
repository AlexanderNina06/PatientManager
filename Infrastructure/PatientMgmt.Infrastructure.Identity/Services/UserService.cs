using System;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PatientMgmt.Core.Application.DTOs.User;
using PatientMgmt.Core.Application.Interfaces.Services;
using PatientMgmt.Infrastructure.Identity.Entities;
using PatientMgmt.Infrastructure.Identity.Interfaces;

namespace PatientMgmt.Infrastructure.Identity.Services;

public class UserService : IInfrastructureUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public UserService(UserManager<ApplicationUser> userManager, IMapper mapper)
    {
      _userManager = userManager;
      _mapper = mapper;
    }
    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        return _mapper.Map<List<UserDto>>(users);
    }

    /*public Task CreateUserAsync(CreateUserDto dto)
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
    }*/
}
