using PatientMgmt.Core.Domain;

namespace PatientMgmt.Core.Application;

public interface ILabTestRepository : IGenericRepository<LabTest>
{
  Task<List<LabTest>> GetLabTestNameByIdsAsync(ICollection<int> LabTestsId);
  
}
