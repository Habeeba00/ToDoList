
using Microsoft.EntityFrameworkCore;
using ToDoList.MappingConfigs;
using ToDoList.Models;

namespace ToDoList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                // Add Swagger configurations
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "ToDoList API",
                    Version = "v1",
                    Description = "API for managing the To do list  application.",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Habiba Mohamed",
                        Email = "habiibamohamed259@gmail.com",
                    }
                });
                options.EnableAnnotations();
            });
            builder.Services.AddDbContext<ToDoListDBContext>
               (op => op.UseSqlServer(builder.Configuration.GetConnectionString("conn"))
               );
           builder.Services.AddAutoMapper(typeof(MappingConfig));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
