using PatientMgmt.Core.Domain;

namespace PatientMgmt.Core.Application;

public interface IPatientService : IGenericService<SavePatientViewModel, PatientViewModel, Patient>
{

}
