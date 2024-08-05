using AutoMapper;
using PatientMgmt.Core.Domain;

namespace PatientMgmt.Core.Application;

public class GeneralProfile : Profile
{
public GeneralProfile()
{

// Product LabTest Mappings
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

// Patient LabTest Mappings
CreateMap<Patient,PatientViewModel>()
.ReverseMap()
.ForMember(dest => dest.Created, opt => opt.Ignore())
.ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
.ForMember(dest => dest.LastModified, opt => opt.Ignore())
.ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

CreateMap<Patient,SavePatientViewModel>()
.ForMember(dest => dest.PictureFile, opt => opt.Ignore())
.ReverseMap()
.ForMember(dest => dest.Created, opt => opt.Ignore())
.ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
.ForMember(dest => dest.LastModified, opt => opt.Ignore())
.ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

// LabTest Mappings
CreateMap<LabTest,LabTestViewModel>()
.ReverseMap()
.ForMember(dest => dest.Created, opt => opt.Ignore())
.ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
.ForMember(dest => dest.LastModified, opt => opt.Ignore())
.ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

CreateMap<LabTest,SaveLabTestViewModel>()
.ReverseMap()
.ForMember(dest => dest.Created, opt => opt.Ignore())
.ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
.ForMember(dest => dest.LastModified, opt => opt.Ignore())
.ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

// Appointment Mappings
CreateMap<Appointment,AppointmentViewModel>()
.ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => $"{src.patient.Name} {src.patient.LastName}"))
.ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => $"{src.doctor.Name} {src.doctor.LastName}"))
.ForMember(dest => dest.appointmentStatus, opt => opt.MapFrom(src => src.appointmentStatus.ToString()))
.ForMember(dest => dest.TestFinalResults, opt => opt.Ignore())
.ForMember(dest => dest.TestResultState, opt => opt.Ignore())
.ForMember(dest => dest.TestsNames, opt => opt.Ignore())
.ReverseMap()
.ForMember(dest => dest.Created, opt => opt.Ignore())
.ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
.ForMember(dest => dest.LastModified, opt => opt.Ignore())
.ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore())
.ForMember(dest => dest.doctor, opt => opt.Ignore())
.ForMember(dest => dest.tests, opt => opt.Ignore())
.ForMember(dest => dest.TestResults, opt => opt.Ignore())
.ForMember(dest => dest.patient, opt => opt.Ignore());

// TestResults Mappings
CreateMap<TestResult,TestResultsViewModel>()
.ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => $"{src.patient.Name} {src.patient.LastName}"))
.ForMember(dest => dest.ResultStatus, opt => opt.MapFrom(src => src.ResultStatus.ToString()))
.ReverseMap()
.ForMember(dest => dest.Created, opt => opt.Ignore())
.ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
.ForMember(dest => dest.LastModified, opt => opt.Ignore())
.ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore())
.ForMember(dest => dest.Appointment, opt => opt.Ignore())
.ForMember(dest => dest.patient, opt => opt.Ignore());

// User Mappings
CreateMap<AuthenticationRequest, LoginViewModel>()
	.ForMember(dest => dest.HasError, opt => opt.Ignore())
	.ForMember(dest => dest.Error, opt => opt.Ignore())
	.ReverseMap();

CreateMap<RegisterRequest, SaveUserViewModel>()
	.ForMember(dest => dest.HasError, opt => opt.Ignore())
	.ForMember(dest => dest.Error, opt => opt.Ignore())
	.ReverseMap();
}
}
