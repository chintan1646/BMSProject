using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RosterTeamAPI.DATA
{
  public class EmployeeGetDto
  {
    

    [Required]
    public string Name { get; set; }

    [Url]
    public string ProfilePicture { get; set; }
    
    [Required]
    public double UsedWorkHoursWeek { get; set; }

  }
}


