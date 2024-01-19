using Raizen.Cliente.Domain.Repositories;

namespace Raizen.Cliente.Application.Services
{
    public class ClienteService : IClienteService
    {   
        public IClienteRepository Repository { get; }
        public ClienteService(IClienteRepository repository)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository)); ;
        }
        public async Task<IEnumerable<Domain.Entities.Cliente>> GetAll()
        {
            return await Repository.GetAll();
        }

        public async Task<IEnumerable<Domain.Entities.Cliente>> GetAll(Domain.Entities.Cliente cliente)
        {
            return await Repository.GetAll(cliente);
        }

        public async Task<Domain.Entities.Cliente> GetById(int id)
        {
            return await Repository.GetById(id);
        }

        public async Task<bool> Insert(Domain.Entities.Cliente cliente)
        {
            return await Repository.Insert(cliente);
        }

        public async Task<bool> Update(Domain.Entities.Cliente cliente)
        {
            return !await Repository.Update(cliente);
        }

        public async Task<bool> Delete(int id)
        {
            return await Repository.Delete(id);
        }
    }
}
