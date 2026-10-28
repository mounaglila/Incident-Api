using Microsoft.EntityFrameworkCore;

namespace Incident-api.Models
{
    public class IncidentsDbContext : DbContext
    {
        public IncidentsDbContext(DbContextOptions<IncidentsDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Incident> Incidents { get; set; }
    }
}

