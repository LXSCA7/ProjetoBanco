using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjetoBanco.Models;
using Microsoft.Extensions.Configuration;

namespace ProjetoBanco.Context
{
    public class UsuariosContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("\\ProjetoBanco\\appsettings.Development.json")
                .Build();

                string conexao = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer("conexao");
            }
        }
    }
}