using HW2.DAL.Abstract;
using HW2.DAL.Concrete;
using HW2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace HW2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Register the DbContext with Dependency Injection
            builder.Services.AddDbContext<GenreAssignmentDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("GenreAssignmentConnection")));

            // Register the repository services
            builder.Services.AddScoped<IShowRepository, ShowRepository>();
            builder.Services.AddScoped<IActorRepository, ActorRepository>();

            // Add Swagger services
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "My API",
                    Description = "An ASP.NET Core Web API"
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
