using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
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
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public Usuario ObterPorId(int? id)
        {
            if (!id.HasValue)
                return null;
                
            return _context.Usuarios.SingleOrDefault(u => u.Id == id);
        }
        public void AddSaldo(Usuario user, int id,decimal valor)
        {
            var serAdicionado = _context.Usuarios.Find(id);
            serAdicionado.Saldo += valor;

            _context.Update(serAdicionado);
            _context.SaveChanges();
        }

        public bool SenhaCorreta(string username, string senha)
        {
            var usuarioLogin = _context.Usuarios.SingleOrDefault(u => u.Username == username);
            if (usuarioLogin.Senha == senha)
                return true;

            return false;
        }

        public Usuario RealizaLogin(string username, string senha)
        {
            var user = _context.Usuarios.SingleOrDefault(u => u.Username == username && u.Senha == senha);
            return user;
        }

        public void TransferirEntreContas(Usuario user1, Usuario user2, decimal valor)
        {
            if (user1.Saldo < valor)
                throw new Exception("Erro: saldo do usuario menor que valor.");
            user1.Saldo -= valor;
            user2.Saldo += valor;

            _context.Update(user1);
            _context.Update(user2);
            _context.SaveChanges();
        }
    
        public void MudarSenha(Usuario user, string senha)
        {
            user.Senha = senha;
            
            _context.Update(user);
            _context.SaveChanges();
        }

        public void RemoverConta(Usuario user)
        {
            _context.Remove(user);
            _context.SaveChanges();
        }

        public void Sacar(Usuario user, decimal valor)
        {
            user.Saldo -= valor;

            _context.Update(user);
            _context.SaveChanges();
        }

        public void AtualizarCPF(Usuario user, string CPF)
        {
            user.CPF = CPF;

            _context.Update(user);
            _context.SaveChanges();
        }

        public void AtualizarUsername(Usuario user, string username)
        {
            user.Username = username;

            _context.Update(user);
            _context.SaveChanges();
        }
    }
}