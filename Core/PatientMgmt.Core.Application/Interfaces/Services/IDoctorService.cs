using PatientMgmt.Core.Domain;

namespace PatientMgmt.Core.Application;

public interface IDoctorService : IGenericService<SaveDoctorViewModel, DoctorViewModel, Doctor>
{

}
