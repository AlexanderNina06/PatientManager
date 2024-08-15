using System;
using PatientMgmt.Core.Domain;

namespace PatientMgmt.Core.Application.Interfaces.Repositories;

public interface ITestResultRepository : IGenericRepository<TestResult>
{
    Task<List<TestResult>> GetResultState(int appointmentId);

}
