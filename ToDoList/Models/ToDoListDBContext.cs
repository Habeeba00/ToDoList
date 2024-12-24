using Microsoft.EntityFrameworkCore;

namespace ToDoList.Models
{
    public class ToDoListDBContext:DbContext

    {
        public ToDoListDBContext()
        {
            
        }
        public ToDoListDBContext(DbContextOptions<ToDoListDBContext>options):base(options)
        {    
        }
        public virtual DbSet<Tasks> Tasks { get; set; }
    }
}
