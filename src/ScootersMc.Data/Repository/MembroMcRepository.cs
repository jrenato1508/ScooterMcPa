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
    public class MembrosRepository : Repository<MembroMc>, IMembroRepository
    {
        public MembrosRepository(MeuDbContext db) : base(db) { }

        public async Task<MembroMc> ObterMembroEndereco(Guid id)
        {
            return await _context.MembrosMc.AsNoTracking()
                                         .Include(x => x.Endereco)
                                         .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<MembroMc> ObterMembroContatosEndereco(Guid id)
        {
            return await _context.MembrosMc.AsNoTracking()
                                            .Include(x => x.ContatoEmergencia)
                                            .Include(x => x.Endereco)
                                            .FirstOrDefaultAsync(x => x.Id == id);
        }

    }
}
