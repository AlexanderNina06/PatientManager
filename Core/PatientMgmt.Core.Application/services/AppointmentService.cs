using System;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using PatientMgmt.Core.Application.Interfaces.Repositories;
using PatientMgmt.Core.Application.Interfaces.Services;
using PatientMgmt.Core.Domain;
using PatientMgmt.Core.Domain.Enums;

namespace PatientMgmt.Core.Application.services;

public class AppointmentService : GenericService<SaveAppointmentViewModel, AppointmentViewModel, Appointment>, IAppointmentService
{
  private readonly IAppointmentRepository _AppointmentRepository;
  private readonly ITestResultRepository _testResultRepository;
  private readonly IHttpContextAccessor _HttpContextAccessor;
  private readonly AuthenticationResponse userViewModel;
  private readonly IMapper _mapper;
    public AppointmentService(IAppointmentRepository appointmentRepository, IMapper mapper, IHttpContextAccessor HttpContextAccessor, ITestResultRepository testResultRepository) : base(appointmentRepository, mapper)
    {
      _AppointmentRepository = appointmentRepository;
      _HttpContextAccessor =  HttpContextAccessor;
      userViewModel = _HttpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
      _mapper = mapper;
      _testResultRepository = testResultRepository;
    }

    public override async Task<SaveAppointmentViewModel> Add(SaveAppointmentViewModel vm)
    {
      vm.UserId = userViewModel.Id;

      return await base.Add(vm);
    }
    
    public async Task AssignLabTestsToAppointment(int appointmentId, List<int> labTestIds)
    {
      var appointment = await _AppointmentRepository.GetByIdAsync(appointmentId);
      if (appointment == null)
      {
        throw new Exception("Appointment not found");
      }

      foreach (var labTestId in labTestIds)
      {
        var testResult = new TestResult
        {
          AppointmentIdFK = appointmentId,
          LabTestId = labTestId,
          ResultStatus = ResultStatus.pending,
          Result = null,
          UserId = userViewModel.Id
        };
          await _testResultRepository.AddAsync(testResult);

          appointment.appointmentStatus = ApptStatus.pendingResults;
          await _AppointmentRepository.UpdateAsync(appointment, appointmentId);
      }
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
