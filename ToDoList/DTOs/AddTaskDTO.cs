﻿using System.ComponentModel.DataAnnotations;

namespace ToDoList.DTOs
{
    public class AddTaskDTO
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string PriorityLevel { get; set; }
        public DateOnly CreationDate { get; set; }
        public DateOnly DueDate { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
    }
}