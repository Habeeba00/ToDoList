using System.ComponentModel.DataAnnotations;

namespace ToDoList.DTOs
{
    public class AddTaskDTO
    {
        [Required]
        [StringLength(100, ErrorMessage = "Title must be between 1 and 100 characters.", MinimumLength = 1)]
        public string Title { get; set; }

        public string Description { get; set; }

        [RegularExpression(@"^(Low|Medium|High)$", ErrorMessage = "PriorityLevel must be one of: Low, Medium, High.")]
        public string PriorityLevel { get; set; }

        [Required]
        public DateOnly CreationDate { get; set; }

        [Required]
        public DateOnly DueDate { get; set; }

        [Required]
        [RegularExpression(@"^(Pending|In Progress|Completed|incomplete)$", ErrorMessage = "Status must be one of: Pending, In Progress, Completed,incompleted.")]
        public string Status { get; set; }

        public string Notes { get; set; }
    }
}
