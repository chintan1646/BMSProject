using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RosterTeamAPI.Models
{
  public class Employee
  {
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Url]
    public string ProfilePicture { get; set; }
    // public string PhoneNo { get; set; }
    [Required]
    public double TotalWorkHoursDay { get; set; }
    [Required]
    public double UsedWorkHoursDay { get; set; } 
    [Required]
    public double TotalWorkHoursWeek { get; set; }
    [Required]
    public double UsedWorkHoursWeek { get; set; }
   
    public double? OverTimeHours { get; set; }
    public List<SubTask> SubTasks { get; set; } = new List<SubTask>();

    public List<TeamEmployee> TeamEmployees { get; set; } = new List<TeamEmployee>();
  }
}


//30   0
//30   5