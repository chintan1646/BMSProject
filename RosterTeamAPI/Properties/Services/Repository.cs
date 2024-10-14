using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RosterTeamAPI.Repositories;
using RosterTeamAPI.Models;
using RosterTeamAPI.DATA;

namespace RosterTeamAPI.Repositories
{
  public class Repository<T> : IRepository<T> where T : class
  {
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(AppDbContext context)
    {
      _context = context;
      _dbSet = context.Set<T>();
    }

    // Generic method to get entity by ID
    public async Task<T> GetByIdAsync(int id)
    {
      return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
      return await _dbSet.ToListAsync();
    }

    // Generic method to add an entity to the database
    public async Task AddAsync(T entity)
    {
      await _dbSet.AddAsync(entity);
    }

    // Generic method to save changes to the database
    public async Task SaveAsync()
    {
      await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
      _dbSet.Remove(entity);
      await _context.SaveChangesAsync();
    }


    public async Task<IEnumerable<SubTask>> GetSubTasksByParentTaskIdAsync(int parentTaskId)
    {
      if (typeof(T) == typeof(SubTask))
      {
        return await _context.Subtasks
            .Where(st => st.ParentTaskId == parentTaskId)
            .ToListAsync();
      }
      throw new InvalidOperationException("This method is only valid for SubTask entities.");
    }



  }
}
