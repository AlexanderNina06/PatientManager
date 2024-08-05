using AutoMapper;
using Microsoft.AspNetCore.Http;
using PatientMgmt.Core.Domain;

namespace PatientMgmt.Core.Application;

public class PatientService : GenericService<SavePatientViewModel, PatientViewModel, Patient>, IPatientService
{
      private readonly IPatientRepository _patientRepository;
      private readonly IHttpContextAccessor _httpContextAccessor;
      private readonly AuthenticationResponse userViewModel;
      private readonly IMapper _mapper;
    public PatientService(IPatientRepository patientepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(patientepository, mapper)
    {
      _patientRepository = patientepository;
      _httpContextAccessor = httpContextAccessor;
      userViewModel = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
      _mapper = mapper;
    }

    public virtual async Task<SavePatientViewModel> Add(SavePatientViewModel vm)
    {
        vm.UserId = userViewModel.Id;

        return await base.Add(vm);
    }

    public override async Task Update(SavePatientViewModel vm, int id)
    {
    vm.UserId = userViewModel.Id;

	  await base.Update(vm, id);
    }

    public override async Task<List<PatientViewModel>> GetAll()
    {
      var patientList = await _patientRepository.GetAllAsync();

      return patientList.Where(patient => patient.UserId == userViewModel.Id).Select(patient => new PatientViewModel
      {
        
        Id = patient.Id,
        Name = patient.Name,
        LastName = patient.LastName,
        IdCard = patient.IdCard,
        Picture = patient.Picture,
        Phone = patient.Phone,
        hasAllergies = patient.hasAllergies,
        isSmoker = patient.isSmoker,
        Direction = patient.Direction

      }).ToList();
    }
}
