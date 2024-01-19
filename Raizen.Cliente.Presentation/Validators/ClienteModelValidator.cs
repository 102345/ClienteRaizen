using FluentValidation;
using Raizen.Cliente.Application.Contracts;

namespace Raizen.Cliente.Presentation.Validators
{
    public class ClienteModelValidator : AbstractValidator<ClienteModel>
    {
        public ClienteModelValidator() 
        {
            RuleFor(clienteModel => clienteModel.Nome).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(clienteModel => clienteModel.DataNascimento).NotEmpty();
            RuleFor(clienteModel => clienteModel.Email).NotEmpty().EmailAddress().MaximumLength(50);
            RuleFor(clienteModel => clienteModel.Logradouro).NotEmpty().MaximumLength(150);
            RuleFor(clienteModel => clienteModel.Bairro).NotEmpty().MaximumLength(100);
            RuleFor(clienteModel => clienteModel.Cidade).NotEmpty().MaximumLength(100);
            RuleFor(clienteModel => clienteModel.UF).NotEmpty().MaximumLength(2);
            RuleFor(clienteModel => clienteModel.CEP).NotEmpty().MaximumLength(15);
        }
    }
}
