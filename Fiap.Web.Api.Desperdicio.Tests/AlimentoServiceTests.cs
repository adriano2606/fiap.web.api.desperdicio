using Fiap.Web.Api.Desperdicio.Data.Repository;
using Fiap.Web.Api.Desperdicio.Services;
using Fiap.Web.Api.Desperdicio.Models;
using Xunit;
using Moq;
using System.Threading.Tasks;

namespace Fiap.Web.Api.Desperdicio.Tests
{
    public class AlimentoServiceTests
    {
        [Fact] 
        public async Task AddAlimento_DeveChamarAddAsyncDoRepositorio_e_Completar()
        {

            var mockRepo = new Mock<IAlimentoRepository>();

            mockRepo.Setup(repo => repo.AddAsync(It.IsAny<Alimento>()))
                    .Returns(Task.CompletedTask);


            var service = new AlimentoService(mockRepo.Object);

            var novoAlimento = new Alimento
            {
                Nome = "Alimento Teste",
                QuantidadeKg = 1.0m,
                DataValidade = System.DateTime.Now.AddDays(1),
            };


            await service.AddAsync(novoAlimento);


            mockRepo.Verify(repo => repo.AddAsync(It.IsAny<Alimento>()), Times.Once);


        }
    }
}
