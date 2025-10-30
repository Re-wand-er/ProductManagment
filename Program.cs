using Serilog;
using Serilog.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using ProductManagment.Application.Services;
using ProductManagment.Domain.Interfaces;
using ProductManagment.Infrastructure.Persistence;
using ProductManagment.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using ProductManagment.Application.Interfaces;
using ProductManagment.WebUI.Contracts;

namespace ProductManagment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // логгирование с использованием serilog
            //------------------------------------------------------------------------------------------------------------
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Error)
                .Enrich.FromLogContext()
                .WriteTo.File
                (
                    path: "log/log.log",
                    fileSizeLimitBytes: 5_000_000,
                    rollOnFileSizeLimit: true,
                    shared: true, // не уверен, что необходимо
                    outputTemplate: " {Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{SourceContext}{Exception}"
                )
                .CreateLogger();
            builder.Host.UseSerilog();
            //------------------------------------------------------------------------------------------------------------

            // Настройка аутентификации
            //------------------------------------------------------------------------------------------------------------
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(aut =>
                {
                    aut.LoginPath = "/Login/Login";
                    aut.AccessDeniedPath = "/Home/Index";
                });
            //------------------------------------------------------------------------------------------------------------

            // Добавлен для открытия страниц из нужной папки
            //------------------------------------------------------------------------------------------------------------
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

            // DI для репозиториев и сервисов
            //------------------------------------------------------------------------------------------------------------
            builder.Services.AddDbContext<DataBaseContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            //------------------------------------------------------------------------------------------------------------

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Чтобы API имело определенный порт отправки
            //------------------------------------------------------------------------------------------------------------
            builder.Services.AddHttpClient("ApiClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7248/"); 
            });
            builder.Services.AddScoped<ApiClient>(); // Для отправки json
            //------------------------------------------------------------------------------------------------------------


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            // Для статичных файлов wwwroot из папки WebUI
            //------------------------------------------------------------------------------------------------------------
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(builder.Environment.ContentRootPath, "WebUI", "wwwroot")),
                RequestPath = ""
            });
            //------------------------------------------------------------------------------------------------------------
           
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.Run();
        }
    }
}
