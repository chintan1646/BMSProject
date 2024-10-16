using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RosterTeamAPI.DATA;
using RosterTeamAPI.DTOs;
using RosterTeamAPI.Models;
using RosterTeamAPI.Repositories;

namespace RosterTeamAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TeamEmployeeController : ControllerBase
  {
    private readonly IRepository<Service> _ServiceRepository;
    private readonly IRepository<Employee> _EmployeeRepository;
    private readonly IRepository<SubTask> _SubtaskRepository;
    private readonly IRepository<Team> _TeamRepository;
    private readonly IRepository<TeamEmployee> _TeamEmployeeRepository;
    private readonly IMapper _mapper;
    public TeamEmployeeController(IRepository<Service> ServiceRepository, IMapper mapper,
    IRepository<Employee> EmployeeRepository, IRepository<SubTask> SubtaskRepository
    , IRepository<TeamEmployee> TeamEmployeeRepository, IRepository<Team> TeamRepository)
    {
      _ServiceRepository = ServiceRepository;
      _EmployeeRepository = EmployeeRepository;
      _SubtaskRepository = SubtaskRepository;
      _TeamEmployeeRepository = TeamEmployeeRepository;
      _TeamRepository = TeamRepository;
      _mapper = mapper;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTeamById(int id)
    {
      var team = await _TeamRepository.GetByIdAsync(id);

      if (team == null)
      {
        return NotFound($"Team with ID {id} not found.");
      }

      // Optionally map to a DTO if you don't want to expose the entity directly

      return Ok(team);
    }

    [HttpPost("{teamId}/employees/{employeeId}")]
    public async Task<IActionResult> AddEmployeeToTeam(int teamId, int employeeId)
    {
      try
      {
        await _TeamEmployeeRepository.AddEmployeeToTeamAsync(employeeId, teamId);
        return Ok($"Employee {employeeId} added to Team {teamId}.");
      }
      catch (InvalidOperationException ex)
      {
        return NotFound(ex.Message);
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"Internal server error: {ex.Message}");
      }
    }

    [HttpPost]
    public async Task<IActionResult> AddEmployee([FromBody] TeamPostDto teamPostDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      // Use AutoMapper to map the DTO to the model
      var team = _mapper.Map<Team>(teamPostDto);

      await _TeamRepository.AddAsync(team);
      await _TeamRepository.SaveAsync();

      return CreatedAtAction(nameof(GetTeamById), new { id = team.Id }, team);
    }

    // [HttpGet("employees-grouped-by-team")]
    // public async Task<IActionResult> GetEmployeesGroupedByTeam()
    // {
    //   try
    //   {
    //     var result = await _TeamEmployeeRepository.GetEmployeesGroupedByTeamAsync();
    //     return Ok(result);
    //   }
    //   catch (Exception ex)
    //   {
    //     return StatusCode(500, $"Internal server error: {ex.Message}");
    //   }
    // }











  }
}
