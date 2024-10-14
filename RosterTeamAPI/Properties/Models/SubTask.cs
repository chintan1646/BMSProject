using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RosterTeamAPI.Models
{
  public class SubTask
  {
    public int Id { get; set; }
    public string ServiceToBePerformed { get; set; }
    [Required]
    public double TotalHoursScheduled { get; set; }
    
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }
    public int ParentTaskId { get; set; }
    public Service ParentTask { get; set; }
    public string Status { get; set; }
  }
}
//40
//5
