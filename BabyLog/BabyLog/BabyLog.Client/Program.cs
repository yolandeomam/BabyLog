using BabyLog.Client.Services;
using BabyLog.Client.ViewModels;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BabyLog.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder =
                WebAssemblyHostBuilder.CreateDefault(args);

            // HttpClient for internal API calls
            builder.Services.AddScoped(sp =>
                new HttpClient
                {
                    BaseAddress =
                        new Uri(builder.HostEnvironment.BaseAddress)
                });

            // Register services
            builder.Services.AddScoped<ChildApiService>();
            builder.Services.AddScoped<CustomerApiService>();
            builder.Services.AddScoped<TokenStorageService>();

            // Register ViewModels
            builder.Services.AddScoped<ChildViewModel>();

            await builder.Build().RunAsync();
        }
    }
}