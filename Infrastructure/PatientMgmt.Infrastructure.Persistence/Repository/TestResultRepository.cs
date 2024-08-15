using System;
using Microsoft.EntityFrameworkCore;
using PatientMgmt.Core.Application.Interfaces.Repositories;
using PatientMgmt.Core.Domain;

namespace PatientMgmt.Infrastructure.Persistence.Repository;

public class TestResultRepository : GenericRepository<TestResult>, ITestResultRepository
{
  private readonly ApplicationContext _db;
    public TestResultRepository(ApplicationContext context) : base(context)
    {
      _db = context;
    }

    public async Task<List<TestResult>> GetResultState(int appointmentId)
    {
      return await _db.Set<TestResult>()
      .Where(tr => tr.AppointmentIdFK == appointmentId)
      .Include(tr => tr.LabTest)
      .ToListAsync();
    }
}
