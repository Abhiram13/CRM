using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.IO;
using Models;
using Models.ProductReportsRequestBody;
using Models.TransactionsRequestBody;
using Models.ZonalReportsResponseBody;
using Models.RevenuesRequestBody;

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

         services.AddCors(options => {
            options.AddPolicy(
                "Open",
                builder => builder.AllowAnyOrigin().AllowAnyHeader());
         });
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
         if (env.IsDevelopment()) {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CRM v1"));
         }
         // app.UseHttpsRedirection();
         app.UseRouting();
         app.UseCors("Open");
         app.UseAuthorization();
         app.UseEndpoints(endpoints => {
            endpoints.MapControllers();
            endpoints.MapGet("/", (HttpContext context) => {
               return context.Response.WriteAsync("Welcome to CRM");
            });

            endpoints.MapPost("/employee/add", async (HttpContext context) => {
               await context.Response.WriteAsync(
                  await new EmployeeController(context).Add()
               );
            });

            endpoints.MapGet("/employee/select", async (HttpContext context) => {
               await context.Response.WriteAsync(
                  JsonSerializer.Serialize<Employee[]>(
                     await new EmployeeController(context).fetchAllEmployees()
                  )
               );
            });

            endpoints.MapGet("/employee/select/{id}", async (HttpContext context) => {
               string ID = (string)context.Request.RouteValues["id"];

               await context.Response.WriteAsync(
                  await new EmployeeController(context).fetchEmployeeById(ID)
               );
            });

            endpoints.MapPost("/product/add", async (HttpContext context) => {
               await context.Response.WriteAsync(
                  await new Product(context).Add()
               );
            });

            endpoints.MapPost("/lifeinsurance/add", async (HttpContext context) => {
               await context.Response.WriteAsync(
                  await new Transaction<LifeInsuranceBody>(context, "life_insurance").Add()
               );
            });

            endpoints.MapPost("/branch/add", async (HttpContext context) => {
               await context.Response.WriteAsync(
                  await new Branch(context).Add()
               );
            });

            endpoints.MapGet("/branch/fetchall", async (HttpContext context) => {
               await context.Response.WriteAsync(
                  await new Branch(context).FetchAll()
               );
            });

            endpoints.MapPost("/designation/add", async (HttpContext context) => {
               await context.Response.WriteAsync(
                  await new Designation(context).Add()
               );
            });

            endpoints.MapGet("/designation/all", async (HttpContext context) => {
               await context.Response.WriteAsync(
                  await new Designation(context).FetchDesignations()
               );
            });

            endpoints.MapGet("/roles/all", async (HttpContext context) => {
               await context.Response.WriteAsync(
                  EmployeeController.Roles()
               );
            });

            endpoints.MapPost("/customer/add", async (HttpContext context) => {
               await context.Response.WriteAsync(
                  await new Customer(context).Add()
               );
            });

            endpoints.MapPost("/customer/search", async (HttpContext context) => {
               await context.Response.WriteAsync(
                  await new Customer(context).search(context)
               );
            });

            endpoints.MapGet("/customer/fetchall", async (HttpContext context) => {
               await context.Response.WriteAsync(
                  await new Database<CustomerModel>("customer").FetchAll()
               );
            });

            endpoints.MapPost("/fixeddeposit/add", async (HttpContext context) => {
               await context.Response.WriteAsync(
                  await new Transaction<FixedDepositBody>(context, "fixed_deposit").Add()
               );
            });

            endpoints.MapPost("/generalinsurance/add", async (HttpContext context) => {
               await context.Response.WriteAsync(
                  await new Transaction<GeneralInsuranceBody>(context, "general_insurance").Add()
               );
            });

            endpoints.MapPost("/mutualfunds/add", async (HttpContext context) => {
               await context.Response.WriteAsync(
                  await new Transaction<MutualFundsBody>(context, "mutual_funds").Add()
               );
            });

            endpoints.MapPost("/lifeinsurancerevenue/add", async (HttpContext context) => {
               await context.Response.WriteAsync(
                  await new Revenue<LifeInsuranceRevenue>(context, "life_insurance_revenue").Add()
               );
            });

            endpoints.MapPost("/generalinsurancerevenue/add", async (HttpContext context) => {
               await context.Response.WriteAsync(
                  await new Revenue<GeneralInsuranceRevenue>(context, "general_insurance_revenue").Add()
               );
            });

            endpoints.MapPost("/fixeddepositrevenue/add", async (HttpContext context) => {
               await context.Response.WriteAsync(
                  await new Revenue<FixedDepositRevenue>(context, "fixed_deposit_revenue").Add()
               );
            });

            endpoints.MapPost("/mutualfundsrevenue/add", async (HttpContext context) => {
               await context.Response.WriteAsync(
                  await new Revenue<MutualFundsRevenue>(context, "mutual_funds_revenue").Add()
               );
            });

            endpoints.MapPost("/lifeinsurancezonalreports/fetch", async (HttpContext context) => {
               await context.Response.WriteAsync(
                   await new FetchReports<LifeInsuranceBody, ZonalProduct>(context, "life_insurance").fetch(
                      LifeInsurance.Report
                   )
               );
            });

            endpoints.MapPost("/generalinsurancezonalreports/fetch", async (HttpContext context) => {
               await context.Response.WriteAsync(
                  await new FetchReports<GeneralInsuranceBody, ZonalProduct>(context, "general_insurance").fetch(
                     GeneralInsurance.Report
                  )
               );
            });

            endpoints.MapPost("/mutualfundszonalreports/fetch", async (HttpContext context) => {
               await context.Response.WriteAsync(
                  await new FetchReports<MutualFundsBody, ZonalProduct>(context, "mutual_funds").fetch(
                     MutualFunds.Report
                  )
               );
            });

            endpoints.MapPost("/fixeddepositzonalreports/fetch", async (HttpContext context) => {
               await context.Response.WriteAsync(
                  await new FetchReports<FixedDepositBody, ZonalProduct>(context, "fixed_deposit").fetch(
                     FixedDeposit.Report
                  )
               );
            });

            endpoints.MapPost("/lifeinsurancebranchreports/fetch", async (HttpContext context) => {
               await context.Response.WriteAsync(
                  await new FetchReports<LifeInsuranceBody, BranchProduct>(context, "life_insurance").fetch(
                     LifeInsurance.BranchReport
                  )
               );
            });

            endpoints.MapPost("/generalinsurancebranchreports/fetch", async (HttpContext context) => {
               await context.Response.WriteAsync(
                  await new FetchReports<GeneralInsuranceBody, BranchProduct>(context, "general_insurance").fetch(
                     GeneralInsurance.BranchReport
                  )
               );
            });

            endpoints.MapPost("/mutualfundsbranchreports/fetch", async (HttpContext context) => {
               await context.Response.WriteAsync(
                  await new FetchReports<MutualFundsBody, BranchProduct>(context, "mutual_funds").fetch(
                     MutualFunds.BranchReport
                  )
               );
            });

            endpoints.MapPost("/fixeddepositbranchreports/fetch", async (HttpContext context) => {
               await context.Response.WriteAsync(
                  await new FetchReports<FixedDepositBody, BranchProduct>(context, "fixed_deposit").fetch(
                     FixedDeposit.BranchReport
                  )
               );
            });

            endpoints.MapPost("/lifeinsurancermreports/fetch", async (HttpContext context) => {
               await context.Response.WriteAsync(
                  await new FetchReports<LifeInsuranceBody, RMProduct>(context, "life_insurance").fetch(
                     LifeInsurance.RMReport
                  )
               );
            });

            endpoints.MapPost("/generalinsurancermreports/fetch", async (HttpContext context) => {
               await context.Response.WriteAsync(
                  await new FetchReports<GeneralInsuranceBody, RMProduct>(context, "general_insurance").fetch(
                     GeneralInsurance.RMReport
                  )
               );
            });

            endpoints.MapPost("/mutualfundsrmreports/fetch", async (HttpContext context) => {
               await context.Response.WriteAsync(
                  await new FetchReports<MutualFundsBody, RMProduct>(context, "mutual_funds").fetch(
                     MutualFunds.RMReport
                  )
               );
            });

            endpoints.MapPost("/fixeddepositrmreports/fetch", async (HttpContext context) => {
               await context.Response.WriteAsync(
                  await new FetchReports<FixedDepositBody, RMProduct>(context, "fixed_deposit").fetch(
                     FixedDeposit.RMReport
                  )
               );
            });

            endpoints.MapPost("/zonalrevenuereports", async (HttpContext context) => {
               await context.Response.WriteAsync(
                  await new RevenueReports(context).report()
               );
            });
         });
      }
   }
}
