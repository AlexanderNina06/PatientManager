using System;
using PatientMgmt.Core.Domain;

namespace PatientMgmt.Core.Application.Interfaces.Repositories;

public interface IAppointmentRepository : IGenericRepository<Appointment>
{
  Task<List<Appointment>> GetAllWithIncludeAsync(List<string> properties);
  Task AssignLabTestsToAppointment(int appointmentId, List<int> labTestIds);

}
