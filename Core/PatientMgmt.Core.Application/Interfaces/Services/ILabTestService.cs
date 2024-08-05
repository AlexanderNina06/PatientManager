using PatientMgmt.Core.Domain;

namespace PatientMgmt.Core.Application;

public interface ILabTestService : IGenericService<SaveLabTestViewModel, LabTestViewModel, LabTestViewModel>
{

}
