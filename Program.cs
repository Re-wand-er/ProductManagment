using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using ProductManagment.Domain.Interfaces;
using ProductManagment.Infrastructure.Persistence;
using ProductManagment.Infrastructure.Persistence.Repositories;

namespace ProductManagment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Добавлен для открытия страниц из нужной папки
            // т.к. WebUI не представляет собой отдельного проекта 
            builder.Services.AddControllersWithViews()
                .AddRazorOptions(options =>
                {
                    options.ViewLocationFormats.Clear();
                    options.ViewLocationFormats.Add("/WebUI/Views/{1}/{0}.cshtml");
                    options.ViewLocationFormats.Add("/WebUI/Views/Home/{0}.cshtml");
                    options.ViewLocationFormats.Add("/WebUI/Views/Shared/{0}.cshtml");
                    options.ViewLocationFormats.Add("/WebUI/Views/Shared/{0}.cshtml");
                });

            //------------------------------------------------------------------------------------------------------------
            builder.Services.AddDbContext<DataBaseContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            //------------------------------------------------------------------------------------------------------------

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            // Аналогично использование wwwroot из папки WebUI
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(builder.Environment.ContentRootPath, "WebUI", "wwwroot")),
                RequestPath = ""
            });

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
