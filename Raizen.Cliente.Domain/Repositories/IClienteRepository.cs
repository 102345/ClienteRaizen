namespace Raizen.Cliente.Domain.Repositories
{
    public interface IClienteRepository
    {
        Task<bool> Insert(Entities.Cliente cliente);

        Task<bool> Update(Entities.Cliente cliente);

        Task<bool> Delete(int id);

        Task<IEnumerable<Entities.Cliente>> GetAll();

        Task<IEnumerable<Entities.Cliente>> GetAll(Entities.Cliente cliente);

        Task<Entities.Cliente> GetById(int id);


    }
}
