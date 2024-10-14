using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RosterTeamAPI.DTOs;
using RosterTeamAPI.Models;
using RosterTeamAPI.Repositories;

namespace RosterTeamAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ServiceController : ControllerBase
  {
    private readonly IRepository<Service>  _ServiceRepository;
    private readonly IRepository<Employee> _EmployeeRepository;
    private readonly IRepository<SubTask> _SubtaskRepository;
    private readonly IMapper _mapper;
    public ServiceController(IRepository<Service> ServiceRepository, IMapper mapper, 
    IRepository<Employee> EmployeeRepository, IRepository<SubTask> SubtaskRepository)
    {
      _ServiceRepository = ServiceRepository;
      _EmployeeRepository = EmployeeRepository;
      _SubtaskRepository = SubtaskRepository;
      _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> AddService([FromBody] ServicePostDto serviceDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      // Use AutoMapper to map the DTO to the model
      var service = _mapper.Map<Service>(serviceDto);

      await _ServiceRepository.AddAsync(service);
      await _ServiceRepository.SaveAsync();

      return CreatedAtAction(nameof(AddService), new { id = service.Id }, service);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ServiceGetDto>>> GetAllServices()
    {
      var services = await _ServiceRepository.GetAllAsync();
      var serviceDtos = _mapper.Map<IEnumerable<ServiceGetDto>>(services);
      return Ok(serviceDtos);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteService(int id)
    {
      var service = await _ServiceRepository.GetByIdAsync(id);
      if (service == null)
      {
        return NotFound($"Service with ID {id} not found.");
      }

      await _ServiceRepository.DeleteAsync(service);
      return Ok($"Service with ID {id} has been deleted.");
    }

    [HttpPost("assign")]
    public async Task<IActionResult> AssignTaskToEmployee(int serviceId, int employeeId)
    {
      try
      {
        var service = await _ServiceRepository.GetByIdAsync(serviceId);

        var employee = await _EmployeeRepository.GetByIdAsync(employeeId);



        if (service == null || employee == null)
        {
          return NotFound("Service or Employee not found.");
        }

        double remainingTaskTime = service.TotalHoursScheduled - (service.HoursAssigned ?? 0);
        double availableHours = employee.TotalWorkHoursDay - employee.UsedWorkHoursDay;

        if (availableHours < remainingTaskTime)
        {
          var subTask = new SubTask
          {
            ServiceToBePerformed = service.ServiceToBePerformed,
            TotalHoursScheduled = availableHours,
            EmployeeId = employeeId,
            ParentTaskId = serviceId,
            Status = "InProgress"
          };

          await _SubtaskRepository.AddAsync(subTask);
          await _SubtaskRepository.SaveAsync();
          service.HoursAssigned = (service.HoursAssigned ?? 0) + availableHours;
          employee.UsedWorkHoursDay = employee.TotalWorkHoursDay;
        }
        else
        {
          var subTask = new SubTask
          {
            ServiceToBePerformed = service.ServiceToBePerformed,
            TotalHoursScheduled = remainingTaskTime,
            EmployeeId = employeeId,
            ParentTaskId = serviceId,
            Status = "InProgress"
          };

          await _SubtaskRepository.AddAsync(subTask);
          await _SubtaskRepository.SaveAsync();
          service.HoursAssigned = service.TotalHoursScheduled;
          employee.UsedWorkHoursDay += remainingTaskTime;
        }

        await _ServiceRepository.SaveAsync();
        await _EmployeeRepository.SaveAsync();

        return Ok("Task assigned successfully.");
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"Internal server error: {ex.Message}");
      }
    }


    [HttpGet("subtasks/{parentTaskId}")]
    public async Task<IActionResult> GetSubTasksByParentTaskId(int parentTaskId)
    {
      try
      {
        var subTasks = await _SubtaskRepository.GetSubTasksByParentTaskIdAsync(parentTaskId);

        if (subTasks == null || !subTasks.Any())
        {
          return NotFound("No subtasks found for the given parent task ID.");
        }

        return Ok(subTasks);
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"Internal server error: {ex.Message}");
      }
    }
  }
}
