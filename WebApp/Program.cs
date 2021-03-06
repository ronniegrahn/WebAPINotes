using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyApp.ApplicationLogic;
using MyApp.Repository;
using MyApp.Repository.ApiClient;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddSingleton<IWebApiExecuter, WebApiExecuter>(sp => new WebApiExecuter("https://localhost:44377", new HttpClient()));

            builder.Services.AddTransient<IProjectScreenUseCases, ProjectScreenUseCases>();
            builder.Services.AddTransient<IProjectRepository, ProjectRepository>();
            builder.Services.AddTransient<INoteRepository, NoteRepository>();
            builder.Services.AddTransient<INotesScreenUseCases, NotesScreenUseCases>();
            builder.Services.AddTransient<INoteScreenUseCases, NoteScreenUseCases>();


            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
        }
    }
}
