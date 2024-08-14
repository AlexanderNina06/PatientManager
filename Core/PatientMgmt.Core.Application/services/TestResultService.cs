using System;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using PatientMgmt.Core.Application.Interfaces.Repositories;
using PatientMgmt.Core.Application.Interfaces.Services;
using PatientMgmt.Core.Domain;

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

     
}
