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
  public class EmployeeController : ControllerBase
  {
    private readonly IRepository<Service> _ServiceRepository;
    private readonly IRepository<Employee> _EmployeeRepository;
    private readonly IRepository<SubTask> _SubtaskRepository;
    private readonly IMapper _mapper;
    public EmployeeController(IRepository<Service> ServiceRepository, IMapper mapper,
    IRepository<Employee> EmployeeRepository, IRepository<SubTask> SubtaskRepository)
    {
      _ServiceRepository = ServiceRepository;
      _EmployeeRepository = EmployeeRepository;
      _SubtaskRepository = SubtaskRepository;
      _mapper = mapper;
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployeeById(int id)
    {
      var employee = await _EmployeeRepository.GetByIdAsync(id);

      if (employee == null)
      {
        return NotFound($"Employee with ID {id} not found.");
      }

      // Optionally map to a DTO if you don't want to expose the entity directly
      
      return Ok(employee);
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeGetDto>>> GetAllEmployees()
    {
      var employees = await _EmployeeRepository.GetAllAsync();
      var employeeDto = _mapper.Map<IEnumerable<EmployeeGetDto>>(employees);
      return Ok(employeeDto);
    }


    [HttpPost]
    public async Task<IActionResult> AddEmployee([FromBody] EmployeePostDto employeeDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      // Use AutoMapper to map the DTO to the model
      var employee = _mapper.Map<Employee>(employeeDto);

      // Construct the image URL using the image file name
      employee.ProfilePicture = GetProfilePictureUrl(employeeDto.ImageFileName);

      await _EmployeeRepository.AddAsync(employee);
      await _EmployeeRepository.SaveAsync();

      return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
    }

    private string GetProfilePictureUrl(string imageFileName)
    {
      // Construct the URL to match where the files are stored
      return $"/uploads/{imageFileName}";
    }


    // Delete Employee
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
      try
      {
        var employee = await _EmployeeRepository.GetByIdAsync(id);
        if (employee == null)
        {
          return NotFound($"Employee with ID {id} not found.");
        }

        // Load subtasks for the employee
        var subtasks = await  _SubtaskRepository.GetSubTasksByEmployeeIdAsync(id);
        foreach (var subTask in subtasks)
        {
          var serviceId = subTask.ParentTaskId;
          var service = await _ServiceRepository.GetByIdAsync(serviceId);
          if (service != null)
          {
            // Update service hours
            service.HoursAssigned -= subTask.TotalHoursScheduled;
          }
        }

        await _ServiceRepository.SaveAsync();
        await _EmployeeRepository.DeleteAsync(employee);
        await _EmployeeRepository.SaveAsync();

        return Ok($"Employee with ID {id} and their subtasks have been deleted.");
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"Internal server error: {ex.Message}");
      }
    }


  }
}
