using ELOWARE_Backend.Objects;
using Microsoft.EntityFrameworkCore;

namespace ELOWARE_Backend
{
    public class ElowareContext : DbContext
    {
        public ElowareContext(DbContextOptions<ElowareContext> options) : base(options)
        {
        }

        public DbSet<Person> Personen { get; set; }
    }
}
