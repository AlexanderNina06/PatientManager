using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using PatientMgmt.Core.Application.Interfaces.Repositories;
using PatientMgmt.Core.Domain;
using PatientMgmt.Core.Domain.Enums;

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

    public async Task<List<TestResult>> GetAllPendingTestResults()
    {
      return await _db.Set<TestResult>()
      .Where(tr => tr.ResultStatus == ResultStatus.pending)
      .Include(tr => tr.Appointment).Include(tr => tr.Appointment.patient)
      .Include(tr => tr.LabTest).ToListAsync();
    }

    public async Task<List<TestResult>> GetAllPendingTestResultsById(int IdCard)
    {
      return await _db.Set<TestResult>()
      .Where(tr => tr.ResultStatus == ResultStatus.pending && tr.Appointment.patient.IdCard == IdCard)
      .Include(tr => tr.Appointment).Include(tr => tr.Appointment.patient)
      .Include(tr => tr.LabTest).ToListAsync();
    }

}
