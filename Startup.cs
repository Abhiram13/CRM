using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.IO;
using MongoDB.Driver;

namespace CRM
{
   public sealed class Demo
   {
      public string name { get; set; }

      [HttpGet]
      [Route("/api")]
      public void method()
      {
         Console.WriteLine("Routing");
      }
   }

   public static class Mongo
   {
      public static string url = "";
      public static MongoClient client = new MongoClient($"mongodb+srv://abhiramdb:abhiram13@crm-cluster.i47fm.mongodb.net/CRM-Cluster?retryWrites=true&w=majority");
      public static IMongoDatabase database = client.GetDatabase("CRM");
   }

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
         services.AddControllers();
         services.AddSwaggerGen(c =>
         {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "CRM", Version = "v1" });
         });

         // If using IIS:
         services.Configure<IISServerOptions>(options =>
         {
            options.AllowSynchronousIO = true;
         });
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CRM v1"));
         }
         app.UseHttpsRedirection();
         app.UseRouting();
         app.UseAuthorization();
         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllers();
            endpoints.MapGet("/", (HttpContext context) =>
            {
               return context.Response.WriteAsync("ajhdajh");
            });            

            endpoints.MapPost("/post", async (HttpContext context) =>
            {
               StreamReader reader = new StreamReader(context.Request.Body);
               Task<string> str = reader.ReadToEndAsync();
               Demo demo = JsonSerializer.Deserialize<Demo>(await str);
               await context.Response.WriteAsync(demo.name);
            });

            endpoints.MapGet("/get/{id}", (HttpContext context) =>
            {
               return context.Response.WriteAsync(context.Request.RouteValues["id"].ToString());
            });

            endpoints.MapPost("/addEmployee", async (HttpContext context) =>
            {
               // StreamReader reader = new StreamReader(context.Request.Body);
               // Task<string> str = reader.ReadToEndAsync();
               // IEmployee employee = JsonSerializer.Deserialize<IEmployee>(await str);
               // new Database<IEmployee>("employee").Insert(await JSON.Deserilise<IEmployee>(context));
               new Employee(context).Check();
               await context.Response.WriteAsync("Added");
            });

            endpoints.MapGet("/all", async (HttpContext context) =>
            {
               await context.Response.WriteAsync(await new Database<IEmployee>("employee").FetchAll());
            });
         });      
      }
   }
}
