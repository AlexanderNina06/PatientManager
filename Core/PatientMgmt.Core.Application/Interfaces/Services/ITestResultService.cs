using System;
using PatientMgmt.Core.Application.ViewModels.TestResults;
using PatientMgmt.Core.Domain;

namespace PatientMgmt.Core.Application.Interfaces.Services;

public interface ITestResultService : IGenericService<SaveTestResultViewModel,TestResultsViewModel, TestResult>
{
  Task<List<PendingTestResultViewModel>> GetAllPendingTestResults(FilterTrByIdViewModel? IdCard);
}
