using RosterTeamAPI.Models;
using RosterTeamAPI.DATA;
namespace RosterTeamAPI.Repositories
{
  public interface IRepository<T> where T : class
  {
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task SaveAsync();

    Task DeleteAsync(T entity);
    Task<IEnumerable<SubTask>> GetSubTasksByParentTaskIdAsync(int parentTaskId);

    Task<IEnumerable<SubTask>> GetSubTasksByEmployeeIdAsync(int employeeId);
    Task AddEmployeeToTeamAsync(int employeeId, int teamId);

    Task<List<TeamEmployeeDto>> GetEmployeesGroupedByTeamAsync();

    // Task ResetWeeklyWorkHours();
  }
}
