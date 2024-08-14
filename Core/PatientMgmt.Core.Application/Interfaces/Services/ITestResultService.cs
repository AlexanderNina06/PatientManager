using System;
using PatientMgmt.Core.Domain;

namespace PatientMgmt.Core.Application.Interfaces.Services;

public interface ITestResultService : IGenericService<SaveTestResultViewModel,TestResultsViewModel, TestResult>
{

}
