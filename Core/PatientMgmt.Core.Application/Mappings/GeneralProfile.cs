using AutoMapper;
using PatientMgmt.Core.Domain;

namespace PatientMgmt.Core.Application;

public class GeneralProfile : Profile
{
public GeneralProfile()
{

// Product
CreateMap<Doctor,DoctorViewModel>()
.ReverseMap()
.ForMember(dest => dest.Created, opt => opt.Ignore())
.ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
.ForMember(dest => dest.LastModified, opt => opt.Ignore())
.ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

CreateMap<Doctor,SaveDoctorViewModel>()
.ForMember(dest => dest.PictureFile, opt => opt.Ignore())
.ReverseMap()
.ForMember(dest => dest.Created, opt => opt.Ignore())
.ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
.ForMember(dest => dest.LastModified, opt => opt.Ignore())
.ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());


}
}
