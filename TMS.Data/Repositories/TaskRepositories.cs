using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMS.Application.Interfaces;
using TMS.Domain.Entities;
using TMS.Infrastructure.Identity;
using TMS.Infrastructure.Persistence;

namespace TMS.Infrastructure.Repositories
{
    public class TaskRepositories : IRepository<TaskEntity, Guid>
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public TaskRepositories(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<Guid> AddAsync(TaskEntity entity)
        {
            var user = await _userManager.FindByNameAsync(entity.UserName);
            if (user is null)
            {
                throw new InvalidOperationException("The user does not exist");
            }
            var result = await _context.Tasks.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
            task.Deleted = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TaskEntity>> GetAllAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<TaskEntity> GetByIdAsync(Guid id)
        {
            return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<bool> UpdateAsync(TaskEntity entity)
        {
            var user = await _userManager.FindByNameAsync(entity.UserName);
            if (user is null)
            {
                throw new InvalidOperationException("The user does not exist");
            }
            _context.Tasks.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
