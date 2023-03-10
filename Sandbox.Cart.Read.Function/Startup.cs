using Cart.Read.Core.Contracts;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sandbox.Cart.Infra.Repository;
using Sandbox.Cart.Read.Function.EventIntegration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
[assembly: FunctionsStartup(typeof(Sandbox.Cart.Read.Function.Startup))]
namespace Sandbox.Cart.Read.Function
{
    public class Startup : FunctionsStartup
    {

        public override void Configure(IFunctionsHostBuilder builder)
        {
            var env = GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (string.IsNullOrEmpty(env))
            {
                env = "development";
            }

            var config = new ConfigurationBuilder()
                             .SetBasePath(Directory.GetCurrentDirectory())
                             .AddJsonFile($"appsettings.{env}.json", true)
                             .AddEnvironmentVariables()
                             .Build();
            builder.Services.AddLogging();
            builder.Services.AddSingleton(new CosmosDbSettings
            {
                DatabaseName = GetEnvironmentVariable("CosmosDbSettings__Database"),
                CollectionName = GetEnvironmentVariable("CosmosDbSettings__Collection"),
                ConnectionString = GetEnvironmentVariable("CosmosDbSettings__ConnectionString")
            });
            var handlerAssembly = AppDomain.CurrentDomain.Load("Cart.Read.Core");
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(handlerAssembly));
            builder.Services.AddScoped<ICartRepository, CartRepository>();
        }
        public string GetEnvironmentVariable(string name)
        {
            return Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
        }
    }
}
