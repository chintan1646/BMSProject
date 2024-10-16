using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace RosterTeamAPI.Models
{
  public class Team
  {
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public List<TeamEmployee> TeamEmployees { get; set; } = new List<TeamEmployee>();

  }
}

//post service .
//Delete service .
//get all service .
//add employee . 
//Delete employee .
//Get All employee .
//Get emplyee grp by team 

//overtime 
//task status
//assign task to employee .
//assign mutliple employee to mutiple team
