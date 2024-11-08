using CP3.Domain.Interfaces.Dtos;
using FluentValidation;

namespace CP3.Application.Dtos
{
    public class BarcoDto : IBarcoDto
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty; 
        public string Modelo { get; set; } = string.Empty; 
        public int Ano { get; set; }
        public double Tamanho { get; set; }

        public void Validate()
        {
            var validator = new BarcoDtoValidation();
            var validationResult = validator.Validate(this);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
        }
    }

    internal class BarcoDtoValidation : AbstractValidator<BarcoDto>
    {
        public BarcoDtoValidation()
        {
            RuleFor(barco => barco.Nome)
                .NotEmpty().WithMessage("O campo Nome é obrigatório.")
                .MaximumLength(100).WithMessage("O Nome deve ter no máximo 100 caracteres.");

            RuleFor(barco => barco.Modelo)
                .NotEmpty().WithMessage("O campo Modelo é obrigatório.")
                .MaximumLength(50).WithMessage("O Modelo deve ter no máximo 50 caracteres.");

            RuleFor(barco => barco.Ano)
                .InclusiveBetween(1900, 2050).WithMessage("O Ano deve estar entre 1900 e 2050.");

            RuleFor(barco => barco.Tamanho)
                .InclusiveBetween(1.0, 500.0).WithMessage("O Tamanho deve estar entre 1.0 e 500.0 metros.");
        }
    }
}
