using BookingManagementSystem.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp", policy =>
                {
                    policy.WithOrigins("http://localhost:4200", "http://localhost:4201")
                           .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            builder.Services.AddOpenApi();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<BookingContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("BookingContext"))
            );

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAngularApp");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
