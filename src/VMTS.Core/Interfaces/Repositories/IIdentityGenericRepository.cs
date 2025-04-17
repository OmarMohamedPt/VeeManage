namespace VMTS.Core.Interfaces.Repositories;

public interface IIdentityGenericRepository<T> where T : class
{
    Task<IReadOnlyList<T>> GetAllAsync();
    Task CreateAsync(T entity);
    Task<int> SaveChangesAsync();
}

