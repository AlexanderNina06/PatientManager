using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PatientMgmt.Core.Application;

namespace PatientMgmt.Infrastructure.Persistence;

public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class
{
  private readonly ApplicationContext _db;
  public GenericRepository(ApplicationContext context){

      _db = context;
  
  }
    public virtual async Task<Entity> AddAsync(Entity entity)
    {
        await _db.Set<Entity>().AddAsync(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(Entity entity)
    {
        _db.Set<Entity>().Remove(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<List<Entity>> GetAllAsync()
    {
        return await _db.Set<Entity>().ToListAsync();
    }

    public async Task<Entity> GetByIdAsync(int id)
    {
        return await _db.Set<Entity>().FindAsync(id);
    }

    public async Task UpdateAsync(Entity entity, int id)
    {
        Entity entry = await _db.Set<Entity>().FindAsync(id);
        _db.Entry(entry).CurrentValues.SetValues(entity);
        await _db.SaveChangesAsync();
    }
}
