using System;
using AutoMapper;
using PatientMgmt.Core.Application.DTOs.User;
using PatientMgmt.Infrastructure.Identity.Entities;

namespace PatientMgmt.Infrastructure.Identity.Mappings;

public class UserMappingProfile : Profile
{
  public UserMappingProfile()
  {
    CreateMap<ApplicationUser, UserDto>()
      .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
      .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
      .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))   

      .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
      .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));   
  }

}
