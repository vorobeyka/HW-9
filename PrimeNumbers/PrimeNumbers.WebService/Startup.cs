using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using PrimeNumbers.WebService.Services;
using System.Net;
using Microsoft.Extensions.Logging;

namespace PrimeNumbers.WebService
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IPrimeNumbersService, PrimeNumbersService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    var logger = context.RequestServices.GetService<ILogger<Startup>>();

                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    await context.Response.WriteAsync("Web service 'Prime numbers' by Andrey Basystyi");
                    logger.LogInformation("root was requested");
                });

                endpoints.MapGet("/primes", async context =>
                {
                    var logger = context.RequestServices.GetService<ILogger<Startup>>();
                    logger.LogInformation("primes from range was requested");

                    if (int.TryParse(context.Request.Query["from"].FirstOrDefault(), out var from)
                        && int.TryParse(context.Request.Query["to"].FirstOrDefault(), out var to))
                    {
                        logger.LogInformation($"sets range {from}:{to}");

                        context.Response.StatusCode = (int)HttpStatusCode.OK;

                        var primeNumbers = context.RequestServices.GetService<IPrimeNumbersService>();
                        var items = await primeNumbers.FromRange(from, to);
                        var response = "{" + string.Join(',', items) + "}";
                        await context.Response.WriteAsync(response);
                    }
                    else
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    }
                    logger.LogInformation($"status code: {context.Response.StatusCode}");
                });

                endpoints.MapGet("/primes/{id:int}", async context =>
                {
                    var logger = context.RequestServices.GetService<ILogger<Startup>>();

                    var primeNumbers = context.RequestServices.GetService<IPrimeNumbersService>();
                    var number = int.Parse((string)context.Request.RouteValues["id"]);
                    logger.LogInformation($"prime number for {number} was requested");

                    var isPrime = await primeNumbers.IsPrimeNumber(number);
                    logger.LogInformation($"is prime number {number}: {isPrime}");
                    if (isPrime)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.OK;
                    }
                    else
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    }
                    logger.LogInformation($"status code: {context.Response.StatusCode}");
                });
            });
        }
    }
}
