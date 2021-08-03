using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpeiderappAPI.Models;

namespace SpeiderappAPI
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) //load base settings
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true) //load local settings
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true) //load environment settings
                .AddJsonFile($"appsettings.{env.EnvironmentName}.local.json", optional: true) //load local environment settings
                .AddKeyPerFile(System.Environment.GetEnvironmentVariable("SPEIDERAPP__SECRETS_DIRECTORY") ?? "/run/secrets") //load secrets
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddMvc()
            //     .AddJsonOptions(
            //         options => options.JsonSerializerOptions.ReferenceHandler = Newtonsoft.Json.ReferenceLoopHandling.
            //     );

            services.AddDbContext<DBContext>(opt =>
            {
                var connectionString = Configuration.GetConnectionString("DefaultConnection") + Configuration.GetValue<string>("SPEIDERAPP:DB_PASS", "");
                if (connectionString is null)
                    opt.UseInMemoryDatabase(
                        Configuration.GetValue<string>("DefaultInMemoryDatabaseName"));
                else
                    opt.UseNpgsql(connectionString);
                opt.EnableSensitiveDataLogging(Configuration.GetValue("EnableSensitiveDataLogging", false));
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
