using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace RosterTeamAPI.DATA
{
  public class TeamPostDto
  {
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }

  }
}
