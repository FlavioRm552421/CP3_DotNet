using CP3.Application.Dtos;
using CP3.Application.Services;
using CP3.Domain.Entities;
using CP3.Domain.Interfaces;
using CP3.Domain.Interfaces.Dtos;
using Moq;
using Xunit;

namespace CP3.Tests
{
    public class BarcoApplicationServiceTests
    {
        private readonly Mock<IBarcoRepository> _repositoryMock;
        private readonly BarcoApplicationService _barcoService;

        public BarcoApplicationServiceTests()
        {
            _repositoryMock = new Mock<IBarcoRepository>();
            _barcoService = new BarcoApplicationService(_repositoryMock.Object);
        }

        [Fact]
        public void ObterTodosBarcos_DeveRetornarListaDeBarcos()
        {
            
            var barcos = new List<BarcoEntity>
            {
                new BarcoEntity { Id = 1, Nome = "Barco 1", Modelo = "Modelo 1", Ano = 2020, Tamanho = 25.0 },
                new BarcoEntity { Id = 2, Nome = "Barco 2", Modelo = "Modelo 2", Ano = 2021, Tamanho = 30.0 }
            };

            _repositoryMock.Setup(r => r.ObterTodos()).Returns(barcos);

            
            var resultado = _barcoService.ObterTodosBarcos();

           
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
        }

        [Fact]
        public void ObterBarcoPorId_DeveRetornarBarcoQuandoIdExistir()
        {
            
            var barco = new BarcoEntity { Id = 1, Nome = "Barco 1", Modelo = "Modelo 1", Ano = 2020, Tamanho = 25.0 };

            _repositoryMock.Setup(r => r.ObterPorId(1)).Returns(barco);

            
            var resultado = _barcoService.ObterBarcoPorId(1);

            
            Assert.NotNull(resultado);
            Assert.Equal("Barco 1", resultado.Nome);
        }

        [Fact]
        public void AdicionarBarco_DeveChamarMetodoAdicionar()
        {
            
            var barcoDto = new BarcoDto
            {
                Nome = "Novo Barco",
                Modelo = "Modelo Novo",
                Ano = 2023,
                Tamanho = 28.0
            };

            var novoBarco = new BarcoEntity
            {
                Id = 2,
                Nome = "Novo Barco",
                Modelo = "Modelo Novo",
                Ano = 2023,
                Tamanho = 28.0
            };

            _repositoryMock.Setup(r => r.Adicionar(It.IsAny<BarcoEntity>())).Returns(novoBarco);

           
            var resultado = _barcoService.AdicionarBarco(barcoDto);

            
            Assert.NotNull(resultado);
            Assert.Equal("Novo Barco", resultado.Nome);
            _repositoryMock.Verify(r => r.Adicionar(It.IsAny<BarcoEntity>()), Times.Once);
        }

        [Fact]
        public void EditarBarco_DeveChamarMetodoEditar()
        {
            
            var barcoDto = new BarcoDto
            {
                Nome = "Barco Atualizado",
                Modelo = "Modelo Atualizado",
                Ano = 2021,
                Tamanho = 30.5
            };

            var barcoExistente = new BarcoEntity
            {
                Id = 1,
                Nome = "Barco Antigo",
                Modelo = "Modelo Antigo",
                Ano = 2020,
                Tamanho = 25.0
            };

            _repositoryMock.Setup(r => r.ObterPorId(1)).Returns(barcoExistente);
            _repositoryMock.Setup(r => r.Editar(It.IsAny<BarcoEntity>())).Returns(barcoExistente);

            
            var resultado = _barcoService.EditarBarco(1, barcoDto);

            
            Assert.NotNull(resultado);
            Assert.Equal("Barco Atualizado", resultado.Nome);
            _repositoryMock.Verify(r => r.Editar(It.IsAny<BarcoEntity>()), Times.Once);
        }

        [Fact]
        public void RemoverBarco_DeveChamarMetodoRemover()
        {
           
            var barco = new BarcoEntity
            {
                Id = 1,
                Nome = "Barco Teste",
                Modelo = "Modelo Teste",
                Ano = 2020,
                Tamanho = 20.0
            };

            _repositoryMock.Setup(r => r.ObterPorId(1)).Returns(barco);
            _repositoryMock.Setup(r => r.Remover(1)).Returns(barco);

           
            var resultado = _barcoService.RemoverBarco(1);

           
            Assert.NotNull(resultado);
            Assert.Equal(1, resultado.Id);
            _repositoryMock.Verify(r => r.Remover(1), Times.Once);
        }
    }
}
