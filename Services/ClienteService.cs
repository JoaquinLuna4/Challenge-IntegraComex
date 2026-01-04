using ChallengeIntegra.Data;
using ChallengeIntegra.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeIntegra.Services
{
    public class ClienteService : IClienteService
    {
        private readonly ApplicationDbContext _context;

        public ClienteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> GetAllClientes()
        {
            return await GetAllClientes(false); // Llama por defecto solo activos
        }

        public async Task<IEnumerable<Cliente>> GetAllClientes(bool includeInactive)
        {
            if (includeInactive)
            {
                return await _context.Clientes.ToListAsync(); // Devuelve todos, activos e inactivos
            }
            else
            {
                return await _context.Clientes.Where(c => c.Activo).ToListAsync(); // Solo activos
            }
        }

        public async Task<Cliente> GetClienteById(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task CreateCliente(Cliente cliente)
        {
            cliente.Activo = true;
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCliente(Cliente cliente)
        {
            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                cliente.Activo = false;
                _context.Entry(cliente).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        //Para validar duplicados de CUIT
        // Modificamos el método para aceptar un ID actual opcional para el modo de edición
        public async Task<bool> CuitExists(string cuit, int? currentId = null)
        {
            var query = _context.Clientes.Where(c => c.CUIT == cuit);

            // Si se proporciona un ID actual (estamos en modo de edición),
            // excluimos a ese cliente de la búsqueda.
            if (currentId.HasValue)
            {
                query = query.Where(c => c.Id != currentId.Value);
            }

            // AnyAsync devuelve true si encuentra al menos un elemento que cumpla la condición.
            return await query.AnyAsync();
        }
    }
}




