using FluentValidation;
using Raizen.Cliente.Application.Contracts;

namespace Raizen.Cliente.Presentation.Validators
{
    public class ClienteModelValidator : AbstractValidator<ClienteModel>
    {
        public ClienteModelValidator() 
        {
            RuleFor(clienteModel => clienteModel.Nome).NotNull().NotEmpty().WithMessage("Por favor , informe o nome do cliente");

        }
    }
}
