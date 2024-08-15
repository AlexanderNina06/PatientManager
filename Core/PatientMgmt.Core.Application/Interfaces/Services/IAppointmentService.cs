using System;
using PatientMgmt.Core.Application.ViewModels.TestResults;
using PatientMgmt.Core.Domain;

namespace PatientMgmt.Core.Application.Interfaces.Services;

public interface IAppointmentService : IGenericService<SaveAppointmentViewModel, AppointmentViewModel, Appointment>
{
  Task<List<AppointmentViewModel>> GetAllViewModelWithInclude();
  Task AssignLabTestsToAppointment(int appointmentId, List<int> labTestIds);
  Task<List<ConsultResultsViewModel>> ConsutResults(int appointmentId);
  Task CompleteAppt(int appointmentId);
}
