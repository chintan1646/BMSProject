
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RosterTeamAPI.DTOs
{
  public class ServiceGetDto
  {
 
    public int Id { get; set; }
    public double TotalHoursScheduled { get; set; }
    [Required]
    public string ServiceToBePerformed { get; set; }
    [Required]
    public double HoursAssigned { get; set; }

  }

}