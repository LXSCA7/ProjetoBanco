using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoBanco.Context;
using ProjetoBanco.Models;

namespace ProjetoBanco.Controllers
{
    public class UsuarioController
    {
        private readonly UsuariosContext _context;

        public UsuarioController(UsuariosContext context)
        {
            _context = context;
        }

        public void CriarConta(Usuario usuario)
        {
            // , string nome, string sobrenome, DateTime dataNascimento, string user, string senha
            // usuario = new Usuario
            // {
            //     Nome = nome,
            //     Sobrenome = sobrenome,
            //     DataDeNascimento = dataNascimento,
            //     Username = user,
            //     Senha = senha
            // };

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public void AddSaldo(Usuario user, int id,decimal valor)
        {
            var serAdicionado = _context.Usuarios.Find(id);
            serAdicionado.Saldo += valor;

            _context.Update(serAdicionado);
            _context.SaveChanges();
        }
    }
}