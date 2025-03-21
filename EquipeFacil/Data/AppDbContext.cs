using EquipeFacil.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipeFacil.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Colaborador> Colaboradores { get; set; }
    }
}