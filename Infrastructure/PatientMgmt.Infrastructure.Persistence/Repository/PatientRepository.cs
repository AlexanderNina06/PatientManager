using PatientMgmt.Core.Application;
using PatientMgmt.Core.Domain;

namespace PatientMgmt.Infrastructure.Persistence;

public class PatientRepository : GenericRepository<Patient>, IPatientRepository
{
      private readonly ApplicationContext _db;  

    public PatientRepository(ApplicationContext context) : base(context)
    {
      _db = context;
    }
}
