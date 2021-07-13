using DataStore.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace WebAPIprojects
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            this._env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (_env.IsDevelopment())
            {
                services.AddDbContext<NotesContext>(options =>
                {
                    options.UseInMemoryDatabase("Notes");
                });
            }

            services.AddControllers();

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                //options.ApiVersionReader = new HeaderApiVersionReader("X-API-Version");
            });

            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "My Web API v1", Version = "version 1" });
                options.SwaggerDoc("v2", new OpenApiInfo { Title = "My Web API v2", Version = "version 2" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, NotesContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                //Configure OpenAPI
                app.UseSwagger();
                app.UseSwaggerUI(
                    options =>
                    {
                        options.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1");
                        options.SwaggerEndpoint("/swagger/v2/swagger.json", "WebAPI v2");
                    });
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
