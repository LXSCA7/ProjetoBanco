using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using ProjetoBanco.Context;

namespace ProjetoBanco.Models
{
    public class Verificacao
    {
        public static bool VerificaNascimento(DateTime data)
        {
            DateTime anoAtual = DateTime.Now;
            if (anoAtual.Year - data.Year < 18)
                return false;

            return true;
        }

        public static bool UsuarioExiste(string nomeDeUsuario)
        {
            using (var db = new UsuariosContext())
            {
                return db.Usuarios.Any(u => u.Username == nomeDeUsuario);
            }
        }

        public static bool UsernameAprovado(string nomeDeUsuario)
        {
            foreach (char c in nomeDeUsuario)
            {
                if (c == ' ')
                    return false;
            }

            return true;
        }

        public static bool SenhaAprovada(string senha)
        {
            bool caracterEspecial = false;
            bool numeros = false;
            bool letraMin = false;
            bool letraMai = false;
            if (senha.Length < 8)
                return false;

            foreach (char c in senha)
            {
                if (c == ' ')
                    return false;

                if (c == '!' || c == '@' || c == '#' || c == '$' || c == '%' || c == '&' || c == '.' || c == ',')
                    caracterEspecial = true;
                if (char.IsNumber(c))
                    numeros = true;
                if (char.IsUpper(c))
                    letraMai = true;
                if (char.IsLower(c))
                    letraMin = true;
            }

            if (!caracterEspecial || !numeros || !letraMai || !letraMin)
            {
                return false;
            }

            return true;
        }
    
    }
}