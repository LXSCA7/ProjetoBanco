using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using ProjetoBanco.Context;
using ProjetoBanco.Models;

namespace ProjetoBanco.Controllers
{
    public class ExtratoController
    {
        private readonly ExtratoContext _context;

        public ExtratoController(ExtratoContext context)
        {
            _context = context;
        }


        public void CadastrarTransferencia(Usuario user1, Usuario user2, decimal valorTransferencia, DateTime data)
        {
            Extrato extrato = new()
            {
                Tipo = "Transferência",
                ContaQueRealizou = user1.Id,
                valor = valorTransferencia,
                ContaQueRecebeu = user2.Id,
                DataRealizada = data
            };

            _context.Add(extrato);
            _context.SaveChanges();
        }

        public List<Extrato> ObterTransferencias(Usuario user)
        {
            return _context.Extrato.Where(e => e.ContaQueRealizou == user.Id || e.ContaQueRecebeu == user.Id).ToList();
        }
    }
}