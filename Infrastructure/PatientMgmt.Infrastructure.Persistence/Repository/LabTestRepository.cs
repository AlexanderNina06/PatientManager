using PatientMgmt.Core.Application;
using PatientMgmt.Core.Domain;
using Microsoft.EntityFrameworkCore;
using PatientMgmt.Infrastructure.Persistence;

namespace PatientMgmt.Infrastructure.Persistence;

public class LabTestRepository : GenericRepository<LabTest>, ILabTestRepository
{
      private readonly ApplicationContext _db;  

    public LabTestRepository(ApplicationContext context) : base(context)
    {
      _db = context;
    }

    public async Task<List<LabTest>> GetLabTestNameByIdsAsync(ICollection<int> LabTestsId)
    {
        return await _db.Set<LabTest>().Where(l => LabTestsId.Contains(l.Id)).ToListAsync();
    }
}
