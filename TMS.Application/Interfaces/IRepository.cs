using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TMS.Application.Interfaces
{
    public interface IRepository<TEntity, TId> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
        Task<TEntity> GetByIdAsync(TId id, CancellationToken cancellationToken);
        Task<TId> AddAsync(TEntity entity, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(TId id, CancellationToken cancellationToken);
    }
}
