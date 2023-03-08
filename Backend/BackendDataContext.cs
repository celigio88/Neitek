using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend
{
    public class BackendDataContext : DbContext
    {
        public BackendDataContext(DbContextOptions<BackendDataContext> options) : base(options) { }

        public DbSet<Metas> dsMetas => Set<Metas>();
        public DbSet<Tareas> dsTareas => Set<Tareas>();
    }
}