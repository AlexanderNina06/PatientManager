using System;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using PatientMgmt.Core.Application.Interfaces.Repositories;
using PatientMgmt.Core.Application.Interfaces.Services;
using PatientMgmt.Core.Domain;

namespace PatientMgmt.Core.Application.services;

public class AppointmentService : GenericService<SaveAppointmentViewModel, AppointmentViewModel, Appointment>, IAppointmentService
{
  private readonly IAppointmentRepository _AppointmentRepository;
  private readonly IHttpContextAccessor _HttpContextAccessor;
  private readonly AuthenticationResponse userViewModel;
  private readonly IMapper _mapper;
    public AppointmentService(IAppointmentRepository appointmentRepository, IMapper mapper, IHttpContextAccessor HttpContextAccessor) : base(appointmentRepository, mapper)
    {
      _AppointmentRepository = appointmentRepository;
      _HttpContextAccessor =  HttpContextAccessor;
      userViewModel = _HttpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
      _mapper = mapper;
    }

    public override async Task<SaveAppointmentViewModel> Add(SaveAppointmentViewModel vm)
    {
      vm.UserId = userViewModel.Id;

      return await base.Add(vm);
    }
    public override async Task Update(SaveAppointmentViewModel vm, int id)
    {
        vm.UserId = userViewModel.Id;

      await base.Update(vm, id);
    }

    public async Task<List<AppointmentViewModel>> GetAllViewModelWithInclude()
    {
      var appList = await _AppointmentRepository.GetAllWithIncludeAsync(new List<string> {"doctor", "patient", "tests"});

      return appList.Where(appt => appt.UserId == userViewModel.Id).Select(appt => new AppointmentViewModel
      {
        Id = appt.Id,
        PatientName = appt.patient.Name + " " + appt.patient.LastName,
        DoctorName = appt.doctor.Name + " " + appt.doctor.LastName,
        PatientId = appt.patient.Id,
        DoctorId = appt.doctor.Id,
        AppointmentDateTime = appt.AppointmentDateTime,
        Cause = appt.Cause,
        appointmentStatus = appt.appointmentStatus.ToString(),
        TestsNames = appt.tests.Select(test => new LabTestViewModel {Id = test.Id, TestName = test.TestName}).ToList()

      }).ToList();

    }
}
