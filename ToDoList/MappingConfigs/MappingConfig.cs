using AutoMapper;
using ToDoList.DTOs;
using ToDoList.Models;

namespace ToDoList.MappingConfigs
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<Tasks, AddTaskDTO>().ReverseMap();
            CreateMap<Tasks, DisplayTaskDTO>().ReverseMap();
        }
    }
}
