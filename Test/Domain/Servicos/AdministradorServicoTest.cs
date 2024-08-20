using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using minimalApi.Domínio.Entidades;
using minimalApi.Domínio.Servicos;
using minimalAPI.Infraestrutura.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Test.Domain.Servicos
{
    [TestClass]
    public class AdministradorServicoTest
    {
        private DbContexto CriarContextoDeTeste()
        {
            var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var path = Path.GetFullPath(Path.Combine(assemblyPath ?? "", "..", "..", ".."));

            var builder = new ConfigurationBuilder()
                .SetBasePath(path ?? Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            var configuration = builder.Build();


            return new DbContexto(configuration);
        }


        [TestMethod]
        public void TestandoSalvarAdministrador()
        {
            //Arrange
            var context = CriarContextoDeTeste();
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");


            var administradorServico = new AdministradorServico(context);
            var adm = new Administrador();
            adm.Email = "teste@teste.com";
            adm.Senha = "teste";
            adm.Perfil = "Adm";

            //Act
            administradorServico.Incluir(adm);


            //Assert
            Assert.AreEqual(1, administradorServico.Todos(1).Count());
        }

        [TestMethod]
        public void TestandoBuscaPorIdAdministrador()
        {
            //Arrange
            var context = CriarContextoDeTeste();
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");


            var administradorServico = new AdministradorServico(context);
            var adm = new Administrador();
            adm.Email = "teste@teste.com";
            adm.Senha = "teste";
            adm.Perfil = "Adm";

            //Act
            administradorServico.Incluir(adm);
            var admRes = administradorServico.BuscaPorId(adm.Id);


            //Assert
            Assert.AreEqual(1, admRes.Id);
        }
    }
}
