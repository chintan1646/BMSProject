using AutoMapper;
using RosterTeamAPI.DATA;
using RosterTeamAPI.DTOs;
using RosterTeamAPI.Models;
// using RosterTeamAPI.DTOs;

public class MappingProfile : Profile
{
  public MappingProfile()
  {
    CreateMap<Service, ServicePostDto>().ReverseMap();
    CreateMap<Service, ServiceGetDto>().ReverseMap();
    CreateMap<Employee, EmployeeGetDto>().ReverseMap();
    CreateMap<Team, TeamPostDto>().ReverseMap();
    CreateMap<Team, TeamEmployeeDto>().ReverseMap();
    CreateMap<EmployeePostDto, Employee>()
            .ForMember(dest => dest.TotalWorkHoursDay, opt => opt.MapFrom(src => 8))
            .ForMember(dest => dest.UsedWorkHoursDay, opt => opt.MapFrom(src => 0))
            .ForMember(dest => dest.TotalWorkHoursWeek, opt => opt.MapFrom(src => 40))
            .ForMember(dest => dest.UsedWorkHoursWeek, opt => opt.MapFrom(src => 0))
            .ForMember(dest => dest.OverTimeHours, opt => opt.MapFrom(src => 0))
            .ForMember(dest => dest.TeamEmployees, opt => opt.Ignore())
            .ForMember(dest => dest.SubTasks, opt => opt.Ignore());

  }
}
