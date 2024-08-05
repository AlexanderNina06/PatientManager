using AutoMapper;
using PatientMgmt.Core.Domain;
using AutoMapper;
using Microsoft.AspNetCore.Http;
namespace PatientMgmt.Core.Application;

public class DoctorService : GenericService<SaveDoctorViewModel, DoctorViewModel, Doctor>, IDoctorService
{
  private readonly IDoctorRepository _doctorRepository;
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly AuthenticationResponse userViewModel;
  private readonly IMapper _mapper;
  public DoctorService(IDoctorRepository doctorRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(doctorRepository, mapper)
  {
    _doctorRepository = doctorRepository;
    _httpContextAccessor = httpContextAccessor;
    _mapper = mapper;
     userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
  }

}
