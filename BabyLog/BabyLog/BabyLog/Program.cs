using ApexCharts;
using BabyLog.Client.Services;
using BabyLog.Client.ViewModels;
using BabyLog.Components;
using BabyLog.Data;
using BabyLog.Repositories;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace BabyLog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add Razor components
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents();

            // Register ApexCharts
            builder.Services.AddApexCharts();

            // Add controllers for API endpoints
            builder.Services.AddControllers();

            // Register database context
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions => sqlOptions.EnableRetryOnFailure()
                ));

            // Register repositories
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IChildRepository, ChildRepository>();
            builder.Services.AddScoped<ISleepRepository, SleepRepository>();

            // Register HttpClient for BabyLog internal API calls
            builder.Services.AddScoped(sp =>
            {
                var navigationManager = sp.GetRequiredService<NavigationManager>();

                return new HttpClient
                {
                    BaseAddress = new Uri(navigationManager.BaseUri)
                };
            });

            // Store JWT token received from BabyFællesskab
            builder.Services.AddScoped<TokenStorageService>();

            // Register API services inside BabyLog
            builder.Services.AddScoped<ChildApiService>();
            builder.Services.AddScoped<SleepApiService>();

            // Register API service for BabyFællesskab API
            builder.Services.AddHttpClient<CustomerApiService>(client =>
            {
                client.BaseAddress = new Uri(
                    builder.Configuration["BabyFaellesskabApi:BaseUrl"]!
                );
            });

            // Register ViewModels
            builder.Services.AddScoped<ChildViewModel>();
            builder.Services.AddScoped<SleepViewModel>();

            var app = builder.Build();

            // Configure HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAntiforgery();

            // Map Razor components
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

            // Map controller-based API endpoints
            app.MapControllers();

            app.Run();
        }
    }
}