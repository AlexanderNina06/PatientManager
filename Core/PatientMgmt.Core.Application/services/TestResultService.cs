using AutoMapper;
using Microsoft.AspNetCore.Http;
using PatientMgmt.Core.Application.Interfaces.Repositories;
using PatientMgmt.Core.Application.Interfaces.Services;
using PatientMgmt.Core.Application.ViewModels.TestResults;
using PatientMgmt.Core.Domain;
using PatientMgmt.Core.Domain.Enums;

namespace PatientMgmt.Core.Application.services;

public class TestResultService : GenericService<SaveTestResultViewModel, TestResultsViewModel, TestResult>, ITestResultService
{
  private readonly ITestResultRepository _testResultRepository;
  private readonly IMapper _mapper;
  private readonly IHttpContextAccessor _HttpContextAccessor;
  private readonly AuthenticationResponse userViewModel;


    public TestResultService(ITestResultRepository testResultRepository, IMapper mapper, IHttpContextAccessor HttpContextAccessor) : base(testResultRepository, mapper)
    {
      _testResultRepository = testResultRepository;
      _mapper = mapper;
      _HttpContextAccessor = HttpContextAccessor;
      userViewModel = _HttpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
    }
    public override async Task<SaveTestResultViewModel> Add(SaveTestResultViewModel vm)
    {
      vm.UserId = userViewModel.Id;

      return await base.Add(vm);
    }

    public override async Task Update(SaveTestResultViewModel vm, int id)
    {
      vm.UserId = userViewModel.Id;
      vm.ResultStatus = ResultStatus.Complete.ToString();
    
       await base.Update(vm, id);
    }

    public async Task<List<PendingTestResultViewModel>> GetAllPendingTestResults(FilterTrByIdViewModel? FilterIdCard)
    {
        var pendingTr = await _testResultRepository.GetAllPendingTestResults();

        var listViewModel = pendingTr.Where(tr => tr.UserId == userViewModel.Id).Select(tr => new PendingTestResultViewModel
        {
            Id = tr.Id,
            ResultStatus = tr.ResultStatus.ToString(),
            PatientName = tr.Appointment.patient.Name + " " + tr.Appointment.patient.LastName,
            LabTestId = tr.LabTestId,
            AppointmentId = tr.AppointmentIdFK,
            TestName = tr.LabTest.TestName,
            IdCard = tr.Appointment.patient.IdCard

        }).ToList();

        if(FilterIdCard?.IdCard != null)
        {
          listViewModel = listViewModel
          .Where(tr => tr.IdCard == FilterIdCard.IdCard.Value).ToList();
        }

        return listViewModel;
    }


}
