using ContactMaintenance.Atrributes;
using ContactMaintenance.Provider;
using ContactMaintenance.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;

namespace ContactMaintenance
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddMvc(
                options =>
                {
                    options.Filters.Add(typeof(LoggerAttributes));                   
                }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            var connection = Configuration.GetConnectionString("ContactDB");
            services.AddDbContext<ContactContext>(opt => opt.UseSqlServer(connection));
            services.AddDbContext<LogContext>(opt => opt.UseSqlServer(connection));
            services.AddTransient<IContactRepository, ContactRepository>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Contacts Maintenance API",
                    Description = "API to perform CRUD operations",
                    //TermsOfService = "None",
                    //Contact = new Contact() { Name = "Talking Dotnet", Email = "contact@talkingdotnet.com", Url = "www.talkingdotnet.com" }
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
            ContactContext context, LogContext logContext, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddContext(LogLevel.Information, Configuration.GetConnectionString("ContactDB"));
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            context.Database.Migrate();
            logContext.Database.Migrate();
        }
    }
}
