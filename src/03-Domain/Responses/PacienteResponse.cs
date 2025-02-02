using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosTech.Hackathon.Pacientes.Domain.Responses
{
    public class PacienteResponse
    {
        public PacienteResponse(string messege)
        {
            Message = messege;
        }
        public string Message { get; set; }
    }
}
