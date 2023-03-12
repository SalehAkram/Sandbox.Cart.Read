using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Sandbox.Cart.Infra
{
    public static class SqlExtension
    {
        public static void AddSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("Sql:ConnectionString").Value;
            if (string.IsNullOrEmpty(connectionString))
            {
                return;
            }

            var password = configuration.GetSection("Sql:Password").Value;
            connectionString = !string.IsNullOrEmpty(password) ? connectionString.Replace("{your_password}", password) : connectionString;
            services.AddDbContext<CRDbContext>((_, options) => options.UseSqlServer(connectionString));


         
        }
    }
}
