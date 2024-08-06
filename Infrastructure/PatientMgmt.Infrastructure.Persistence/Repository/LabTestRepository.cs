using PatientMgmt.Core.Application;
using PatientMgmt.Core.Domain;

namespace PatientMgmt.Infrastructure.Persistence;

public class LabTestRepository : GenericRepository<LabTest>, ILabTestRepository
{
      private readonly ApplicationContext _db;  

    public LabTestRepository(ApplicationContext context) : base(context)
    {
      _db = context;
    }
}
