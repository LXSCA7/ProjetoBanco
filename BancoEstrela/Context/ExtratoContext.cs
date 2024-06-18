using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjetoBanco.Models;
using Microsoft.Extensions.Configuration;

namespace ProjetoBanco.Context
{
    public class ExtratoContext : DbContext
    {
        
        
        public DbSet<Extrato> Extrato { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress;Initial Catalog=Informacoes;Integrated Security=true;TrustServerCertificate=True");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Extrato>()
                .HasOne<Usuario>()
                .WithMany()
                .HasForeignKey(t => t.ContaQueRealizou)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Extrato>()
                .HasOne<Usuario>()
                .WithMany()
                .HasForeignKey(t => t.ContaQueRecebeu)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
