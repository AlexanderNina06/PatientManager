using PatientMgmt.Core.Application;
using PatientMgmt.Core.Domain;

namespace PatientMgmt.Infrastructure.Persistence;

public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
{
    private readonly ApplicationContext _db;  
    public DoctorRepository(ApplicationContext context) : base(context)
    {
      _db = context;
    }
}
