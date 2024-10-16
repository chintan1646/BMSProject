using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace RosterTeamAPI.DATA
{
  public class TeamEmployeeDto
  {
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }

    public List<EmployeeGetDto> Employees { get; set; }

  }
}