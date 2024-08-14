using System;
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
}
