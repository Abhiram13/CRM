using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.IO;

namespace CRM {
   public class Startup {
      public Startup(IConfiguration configuration) {
         Configuration = configuration;
      }

      public IConfiguration Configuration { get; }

      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services) {
         services.AddControllers();
         services.AddSwaggerGen(c => {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "CRM", Version = "v1" });
         });

         // If using IIS:
         services.Configure<IISServerOptions>(options => {
            options.AllowSynchronousIO = true;
         });
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
         if (env.IsDevelopment()) {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CRM v1"));
         }
         app.UseHttpsRedirection();
         app.UseRouting();
         app.UseAuthorization();
         app.UseEndpoints(endpoints => {
            endpoints.MapControllers();
            endpoints.MapGet("/", (HttpContext context) => {
               return context.Response.WriteAsync("Welcome to CRM");
            });

            endpoints.MapPost("/addEmployee", async (HttpContext context) => {
               await context.Response.WriteAsync(
                  await new Employee(context).Add()
               );
            });

            endpoints.MapGet("/employee/select", async (HttpContext context) => {
               await context.Response.WriteAsync(
                  JsonSerializer.Serialize<IEmployee[]>(
                     await new Employee(context).fetchAllEmployees()
                  )
               );
            });

            endpoints.MapGet("/employee/select/{id}", async (HttpContext context) => {
               string ID = (string)context.Request.RouteValues["id"];
               
               await context.Response.WriteAsync(
                  await new Employee(context).fetchEmployeeById(ID)
               );
            });
         });
      }
   }
}
