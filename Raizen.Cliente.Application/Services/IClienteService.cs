namespace Raizen.Cliente.Application.Services
{
    public interface IClienteService
    {
        Task<bool> Insert(Domain.Entities.Cliente cliente);

        Task<bool> Update(Domain.Entities.Cliente cliente);

        Task<bool> Delete(int id);

        Task<IEnumerable<Domain.Entities.Cliente>> GetAll();

        Task<IEnumerable<Domain.Entities.Cliente>> GetAll(Domain.Entities.Cliente cliente);

        Task<Domain.Entities.Cliente> GetById(int id);
    }
}
