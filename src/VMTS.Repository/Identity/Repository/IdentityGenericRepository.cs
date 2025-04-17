using Microsoft.EntityFrameworkCore;
using VMTS.Core.Interfaces.Repositories;

namespace VMTS.Repository.Identity.Repository;

public class IdentityGenericRepository<T> : IIdentityGenericRepository<T> where T : class
{
    private readonly IdentityDbContext _dbContext;

    public IdentityGenericRepository(IdentityDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(string id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task CreateAsync(T entity)
    {
        await _dbContext.AddAsync(entity);
    }

    public void Update(T entity)
    {
        _dbContext.Update(entity);
    }

    public void Delete(T entity)
    {
        _dbContext.Remove(entity);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }
}
