using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjetoBanco.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public string CPF { get; set; }
        public string Username { get; set; }
        public string Senha { get; set; }
        public decimal Saldo { get; set; }
        public string PerguntaDeSeguranca { get; set; }
    }
}