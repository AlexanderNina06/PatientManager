using System;

namespace PatientMgmt.Core.Application.ViewModels.Appointments;

public class AssignLabTestsViewModel
{
   public int AppointmentId { get; set; }
    public List<int> SelectedLabTestIds { get; set; }
    public List<LabTestViewModel>? LabTests { get; set; }
}
