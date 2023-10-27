using Microsoft.EntityFrameworkCore;
using ScootersMc.Business.Interfaces;
using ScootersMc.Business.Models;
using ScootersMc.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScootersMc.Data.Repository
{
    public class ContatoEmergenciaRepository : Repository<ContatoEmergencia>, IContatoEmergenciaRepository
    {
        public ContatoEmergenciaRepository(MeuDbContext db) : base(db) { }

        public async Task<ContatoEmergencia> ObterContatoDeEmergenciaPorMembroPorId(Guid id)
        {
            return await _context.ContatosEmegencias.AsNoTracking()
                                                     .Include(m => m.MembroMcId)
                                                     .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<ContatoEmergencia>> ObterContatoDeEmergenciasPorMembro(Guid MembroId)
        {
            return await Buscar(c => c.MembroMcId == MembroId);
        }
    }
}
