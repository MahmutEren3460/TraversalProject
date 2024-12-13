using Microsoft.EntityFrameworkCore;

namespace SignalRApiForSQL.DAL
{
    public class Context:DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<Visitor>? Visitors { get; set; }
    }
}
