using System;
using Microsoft.EntityFrameworkCore;
using PatientMgmt.Core.Application.Interfaces.Repositories;
using PatientMgmt.Core.Domain;

namespace PatientMgmt.Infrastructure.Persistence.Repository;

public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
{
   private readonly ApplicationContext _db;
    public AppointmentRepository(ApplicationContext context) : base(context)
    {
      _db = context;
    }

    public async Task<List<Appointment>> GetAllWithIncludeAsync(List<string> properties)
    {
        var query = _db.Set<Appointment>().AsQueryable();

        foreach(string property in properties)
        {
          query = query.Include(property);
        }

        return await query.ToListAsync();
    }
}
