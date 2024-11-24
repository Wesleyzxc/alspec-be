namespace AlspecBackend.Api
{
    using System.Text.Json.Serialization;
    using AlspecBackend.Entities;
    using AlspecBackend.Repository;
    using AlspecBackend.Repository.JobDao;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        private readonly IWebHostEnvironment hostingEnvironment;

        private readonly IWebHostEnvironment env;

        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment, IWebHostEnvironment env)
        {
            this.env = env;
            this.Configuration = configuration;
            this.hostingEnvironment = hostingEnvironment;

        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var corsOrigins = Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins", policy =>
                 {
                     policy.WithOrigins(corsOrigins)
                        .AllowCredentials()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                 });
            });


            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddConsole();
                builder.AddDebug();
            });

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
            ;
            services.AddDbContext<DataContext>();
            services.AddScoped<IRepository<Job>, JobRepository>();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseAuthorization();
            app.UseCors("AllowSpecificOrigins");


            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            }
            );

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            var context = new DataContext();
        }
    }
}