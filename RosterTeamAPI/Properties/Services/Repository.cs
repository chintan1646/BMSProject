using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RosterTeamAPI.Repositories;
using RosterTeamAPI.Models;
using RosterTeamAPI.DATA;
using AutoMapper;

namespace RosterTeamAPI.Repositories
{
  public class Repository<T> : IRepository<T> where T : class
  {
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;
    private readonly IMapper _mapper;

    public Repository(AppDbContext context , IMapper mapper)
    {
      _context = context;
      _dbSet = context.Set<T>();
      _mapper = mapper;
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

    public async Task<IEnumerable<SubTask>> GetSubTasksByEmployeeIdAsync(int employeeId)
    {
      if (typeof(T) == typeof(SubTask))
      {
        return await _context.Subtasks
            .Where(st => st.EmployeeId == employeeId)
            .ToListAsync();
      }
      throw new InvalidOperationException("This method is only valid for SubTask entities.");
    }

    public async Task AddEmployeeToTeamAsync(int employeeId, int teamId)
    {
      // Check if the employee and team exist
      var employee = await _context.Employees.FindAsync(employeeId);
      var team = await _context.Teams.FindAsync(teamId);

      if (employee == null || team == null)
      {
        throw new InvalidOperationException("Employee or Team not found.");
      }

      // Create a new TeamEmployee association
      var teamEmployee = new TeamEmployee
      {
        EmployeeId = employeeId,
        TeamId = teamId
      };

      // Add the association to the context
      await _context.TeamEmployees.AddAsync(teamEmployee);
      await _context.SaveChangesAsync();
    }

    public async Task<List<TeamEmployeeDto>> GetEmployeesGroupedByTeamAsync()
    {
      var teamEmployees = await _context.TeamEmployees
          .Include(te => te.Team)
          .Include(te => te.Employee)
          .ToListAsync();

      var groupedTeams = teamEmployees
          .GroupBy(te => te.Team)
          .Select(g => new TeamEmployeeDto
          {
            Id = g.Key.Id,
            Name = g.Key.Name,
            Employees = g.Select(te => _mapper.Map<EmployeeGetDto>(te.Employee)).ToList()
          })
          .ToList();

      return groupedTeams;
    }


  }
}
