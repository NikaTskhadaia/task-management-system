using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TMS.Application.Interfaces
{
    public interface IRepository<TEntity, TId> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(TId id);
        Task<TId> AddAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(TId id);
    }
}
