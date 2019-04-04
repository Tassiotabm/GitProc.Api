using GitProc.Model.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace GitProc.Data
{
    public class DomainDbContext : DbContext
    {
        public DbSet<Advogado> Advogados { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Arquivos> Arquivos { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<ProcessoMaster> ProcessoMaster { get; set; }
        public DbSet<Processo> Processos { get; set; }
        public DbSet<ProcessoVersionado> ProcessoVersionados { get; set; }
        public DbSet<Escritorio> Escritorios { get; set; }

        public DomainDbContext(DbContextOptions<DomainDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
            modelBuilder.Entity<Order>().HasKey(b => b.OrderId);
            modelBuilder.Entity<OrderItem>().HasKey(b => b.OrderItemId);

            modelBuilder.Entity<Order>()
                .HasMany(x => x.OrderItemList)
                .WithOne(y => y.Order)
                .HasForeignKey(x => x.OrderId);

            //https://www.postgresql.org/docs/8.1/static/functions-datetime.html#FUNCTIONS-DATETIME-CURRENT

            modelBuilder.Entity<Order>()
               .Property(c => c.CreatedAt)
               .HasDefaultValueSql("current_timestamp");*/

        }
    }
}

// Add-Migration addUsuario -Context DomainDbContext
// ProjetoPadrao: Domain\GitProc.Migrations