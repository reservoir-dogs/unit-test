using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Web.Tests.Solution
{
    public class TestIntegration<TStartup, TDbContext> : WebApplicationFactory<TStartup>
      where TStartup : class
      where TDbContext : DbContext
    {
        private ServiceProvider serviceProviderHost;

        protected TDbContext DbContext { get; private set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<TDbContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                var keepAliveConnection = new SqliteConnection("DataSource=myshareddb;mode=memory;cache=shared");
                keepAliveConnection.Open();

                services.AddDbContext<TDbContext>((options, context) =>
                {
                    context.UseSqlite(keepAliveConnection.ConnectionString)
                        .EnableSensitiveDataLogging();
                });

                serviceProviderHost = services.BuildServiceProvider();

                DbContext = serviceProviderHost.GetRequiredService<TDbContext>();

                DbContext.Database.EnsureCreated();
            });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                if (serviceProviderHost != null)
                    serviceProviderHost.Dispose();
            }
        }
    }
}
