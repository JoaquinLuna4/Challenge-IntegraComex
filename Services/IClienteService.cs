using ChallengeIntegra.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeIntegra.Services
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> GetAllClientes();
        Task<IEnumerable<Cliente>> GetAllClientes(bool includeInactive);
        Task<Cliente> GetClienteById(int id);
        Task CreateCliente(Cliente cliente);
        Task UpdateCliente(Cliente cliente);
        Task DeleteCliente(int id);
        Task<bool> CuitExists(string cuit, int? currentId = null);
    }
}
