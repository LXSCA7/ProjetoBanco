using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoBanco.Models
{
    public class Usuario
    {
        private int Id { get; set; }
        private string Nome { get; set; }
        private string Sobrenome { get; set; }
        private string Username { get; set; }
        private string Senha { get; set; }
        private decimal Saldo { get; set; }
    }
}