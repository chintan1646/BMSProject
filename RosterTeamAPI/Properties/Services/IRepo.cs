using RosterTeamAPI.Models;

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

    // Task ResetWeeklyWorkHours();
  }
}
