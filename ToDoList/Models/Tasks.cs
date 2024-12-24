using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class Tasks
    {
        [Key]
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PriorityLevel { get; set; }
        //public int TaskCount { get; set; }
        public DateOnly CreationDate { get; set; }
        public DateOnly DueDate { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }


    }
}
