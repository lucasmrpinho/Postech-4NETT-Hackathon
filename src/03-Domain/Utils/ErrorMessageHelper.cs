using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosTech.Hackathon.Pacientes.Domain.Utils
{
    public static class ErrorMessageHelper
    {
        public const string PACIENTE001 = "PACIENTE001";
        public const string PACIENTE002 = "PACIENTE002";

        public static Dictionary<string, string> ErrorMessage = new Dictionary<string, string>
        {
            { PACIENTE001, "E-mail inválido." },
            { PACIENTE002, "CPF inválido." },
        };

    }
}
