using AutoMapper;
using Microsoft.AspNetCore.Http;
using PatientMgmt.Core.Domain;

namespace PatientMgmt.Core.Application;

public class LabTestService : GenericService<SaveLabTestViewModel, LabTestViewModel, LabTest>, ILabTestService
{
  private readonly ILabTestRepository _labTestRepository;
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly AuthenticationResponse userViewModel;
  private readonly IMapper _mapper;
    public LabTestService(ILabTestRepository labTestRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(labTestRepository, mapper)
    {
      _labTestRepository = labTestRepository;
      _httpContextAccessor = httpContextAccessor;
      _mapper = mapper;
    }
}
