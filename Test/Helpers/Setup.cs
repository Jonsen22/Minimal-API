using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using minimal_api;
using minimalApi.Domínio.Entidades;
using minimalApi.Infraestrutura.Interfaces;
using minimalAPI.Infraestrutura.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Mocks;

namespace Test.Helpers
{
    public class Setup
    {
        public const string PORT = "5097";
        public static TestContext testContext = default!;
        public static WebApplicationFactory<Startup> http = default!;
        public static HttpClient client = default!;

        public static void ClassInit(TestContext testContext)
        {
            Setup.testContext = testContext;
            Setup.http = new WebApplicationFactory<Startup>();

            Setup.http = Setup.http.WithWebHostBuilder(builder =>
            {
                builder.UseSetting("http_port", Setup.PORT).UseEnvironment("Testing");

                builder.ConfigureServices(services =>
                {
                    
                    services.AddScoped<IAdministradorServico, AdministradorServicoMock>();

                    //var conexao = "Data Source=localhost,8002;Initial Catalog=MinimalAPItest;User ID=sa;Password=password@12345#;TrustServerCertificate=true";
                    //services.AddDbContext<DbContexto>(options =>
                    //{

                    //    options.UseSqlServer(conexao);
                    //});
                });
            });

            Setup.client = Setup.http.CreateClient();
        }

        public static void ClassCleanup()
        {
            Setup.http.Dispose();
        }


    }
}
