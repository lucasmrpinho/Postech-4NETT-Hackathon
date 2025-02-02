using MassTransit;
using PosTech.GrupoOito.Hackathon.PacienteManagement.Events;
using PosTech.Hackathon.Pacientes.Domain.Interfaces;

namespace PosTech.Hackathon.Pacientes.Infrastructure.Publications
{
    public class SavePacientePublisher : ISavePacientePublisher
    {
        private readonly IBus _bus;

        public SavePacientePublisher(IBus bus)
        {
            _bus = bus;
        }
        public async Task<bool> PublishAsync(CreatePacienteEvent request)
        {
            await _bus.Publish(request, ctx =>
            {
                ctx.SetRoutingKey("CreatePacienteEvent");
            });
            return true;
        }
    }
}
