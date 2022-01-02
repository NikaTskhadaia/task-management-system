using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMS.Application.Interfaces;
using TMS.Domain.Entities;
using TMS.Infrastructure.Identity;
using TMS.Infrastructure.Persistence;

namespace TMS.Infrastructure.Repositories
{
    public class TaskRepository : IRepository<TaskEntity, Guid>
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public TaskRepository(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<Guid> AddAsync(TaskEntity entity, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(entity.UserName);
            if (user is null)
            {
                throw new InvalidOperationException("The user does not exist");
            }
            await _context.Tasks.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
            task.Deleted = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<IEnumerable<TaskEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Tasks.ToListAsync(cancellationToken);
        }

        public async Task<TaskEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }

        public async Task<bool> UpdateAsync(TaskEntity entity, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(entity.UserName);
            if (user is null)
            {
                throw new InvalidOperationException("The user does not exist");
            }
            _context.Tasks.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
