﻿namespace PatientMgmt.Core.Application;

public interface IGenericRepository<Entity> where Entity : class
{
Task<Entity> AddAsync(Entity entity);
Task UpdateAsync(Entity entity, int id);
Task DeleteAsync(Entity entity);
Task<List<Entity>> GetAllAsync();
Task<Entity> GetByIdAsync(int id);

}
