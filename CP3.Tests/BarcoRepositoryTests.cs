using CP3.Data.AppData;
using CP3.Data.Repositories;
using CP3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CP3.Tests
{
    public class BarcoRepositoryTests
    {
        private readonly DbContextOptions<ApplicationContext> _options;
        private readonly ApplicationContext _context;
        private readonly BarcoRepository _barcoRepository;

        public BarcoRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;
            _context = new ApplicationContext(_options);
            _barcoRepository = new BarcoRepository(_context);

            
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        [Fact]
        public void ObterTodos_DeveRetornarTodosOsBarcos()
        {
            _context.Barco.AddRange(
                new BarcoEntity { Nome = "Barco 1", Modelo = "Modelo A", Ano = 2020, Tamanho = 30.0 },
                new BarcoEntity { Nome = "Barco 2", Modelo = "Modelo B", Ano = 2021, Tamanho = 40.0 }
            );
            _context.SaveChanges();

            var result = _barcoRepository.ObterTodos();

            Assert.NotNull(result); 
            Assert.Equal(2, result.Count());
        }
    }
}
