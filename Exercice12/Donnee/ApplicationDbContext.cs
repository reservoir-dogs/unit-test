using Microsoft.EntityFrameworkCore;
using Modele;

namespace Donnee
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Dossier> Dossiers { get; set; }
    }
}
