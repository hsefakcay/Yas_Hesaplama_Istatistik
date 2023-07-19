using Microsoft.EntityFrameworkCore;
using WebDönemProjesi.Models;

namespace WebDönemProjesi.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<PersonInfo> People { get; set; }
    }
}
