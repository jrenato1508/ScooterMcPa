using Microsoft.EntityFrameworkCore;
using ScootersMc.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScootersMc.Data.Context
{
    public class MeuDbContext : DbContext
    {
        public MeuDbContext(DbContextOptions options):base(options)
        {
                
        }

        public DbSet<MembroMc> MembrosMc { get; set; }

        public DbSet<Endereco> Enderecos { get; set; }

        public DbSet<ContatoEmergencia> ContatosEmegencias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeuDbContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("Varchar(100)");


            base.OnModelCreating(modelBuilder);
        }
    }
}
