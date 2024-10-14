using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RosterTeamAPI.Models
{
  public class Service
  {
    public int Id { get; set; }
    public string? EmployeeVendorName { get; set; }
    public string? CompanyName { get; set; }
    [Required]
    public string ClientName { get; set; }
    [Required]
    public string ServiceToBePerformed { get; set; }
    [Required]
    public double TotalHoursScheduled { get; set; } //40 
    public double? HoursAssigned { get; set; } //5
    [Required]
    public string Description { get; set; }
    
    [Required]
    public string RepeatEvery { get; set; }
    [Required]
    public DateTime RepeatUntil { get; set; }
    public List<SubTask> SubTasks { get; set; } = new List<SubTask>();
  
  }

}
