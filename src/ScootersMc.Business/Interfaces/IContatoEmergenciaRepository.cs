using ScootersMc.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScootersMc.Business.Interfaces
{
    public interface IContatoEmergenciaRepository : IRepository<ContatoEmergencia>
    {
        Task<ContatoEmergencia> ObterContatoDeEmergenciasPorMembro(Guid MembroId);

        // Task<IEnumerable<ContatoEmergencia>> ObterContatosDeEmergenciasEMembros();

        Task<ContatoEmergencia> ObterContatoDeEmergenciaPorMembroPorId(Guid id);
    }
}
