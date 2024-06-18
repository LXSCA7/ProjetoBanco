using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoBanco.Models
{
    public class Extrato
    {
        public int Id { get; set; } // PK
        public string Tipo { get; set; } // Saque ou Transferencia
        public int ContaQueRealizou { get; set; } // FK
        public decimal valor { get; set; }
        public int? ContaQueRecebeu { get; set; } // FK NULLABLE

        public DateTime DataRealizada { get; set; }

        /* ao inserir o usuario vai poder escolher as transacoes 
         * FEITAS e as RECEBIDAS.
         *
         * ele NÃO poderá ver as transacoes de outros usuarios
         */ 
    }
}