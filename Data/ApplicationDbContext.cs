using ChallengeIntegra.Models;
using Microsoft.EntityFrameworkCore;

namespace ChallengeIntegra.Data
{

    /// Heredando de DbContext para manejar la base de datos
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Expone DbSet para la entidad Cliente
        public DbSet<Cliente> Clientes { get; set; }
    }
}
