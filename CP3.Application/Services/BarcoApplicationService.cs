using CP3.Domain.Entities;
using CP3.Domain.Interfaces;
using CP3.Domain.Interfaces.Dtos;

namespace CP3.Application.Services
{
    public class BarcoApplicationService : IBarcoApplicationService
    {
        private readonly IBarcoRepository _repository;

        public BarcoApplicationService(IBarcoRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<BarcoEntity> ObterTodosBarcos()
        {
            return _repository.ObterTodos();
        }

        public BarcoEntity ObterBarcoPorId(int id)
        {
            var barco = _repository.ObterPorId(id);
            if (barco == null)
            {
                throw new ArgumentException($"Barco com o ID {id} não foi encontrado.", nameof(id));
            }

            return barco;
        }

        public BarcoEntity AdicionarBarco(IBarcoDto entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Os dados do barco não podem ser nulos.");
            }

            var novoBarco = new BarcoEntity
            {
                Nome = entity.Nome ?? throw new ArgumentNullException(nameof(entity.Nome), "Nome não pode ser nulo."),
                Modelo = entity.Modelo ?? throw new ArgumentNullException(nameof(entity.Modelo), "Modelo não pode ser nulo."),
                Ano = entity.Ano,
                Tamanho = entity.Tamanho
            };

            return _repository.Adicionar(novoBarco);
        }

        public BarcoEntity EditarBarco(int id, IBarcoDto entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Os dados do barco não podem ser nulos.");
            }

            var barcoExistente = _repository.ObterPorId(id);
            if (barcoExistente == null)
            {
                throw new ArgumentException($"Barco com o ID {id} não foi encontrado.", nameof(id));
            }

            barcoExistente.Nome = entity.Nome ?? throw new ArgumentNullException(nameof(entity.Nome), "Nome não pode ser nulo.");
            barcoExistente.Modelo = entity.Modelo ?? throw new ArgumentNullException(nameof(entity.Modelo), "Modelo não pode ser nulo.");
            barcoExistente.Ano = entity.Ano;
            barcoExistente.Tamanho = entity.Tamanho;

            return _repository.Editar(barcoExistente);
        }

        public BarcoEntity RemoverBarco(int id)
        {
            var barco = _repository.ObterPorId(id);
            if (barco == null)
            {
                throw new InvalidOperationException($"Não foi possível remover: Barco com o ID {id} não foi encontrado.");
            }

            return _repository.Remover(id);
        }
    }
}
