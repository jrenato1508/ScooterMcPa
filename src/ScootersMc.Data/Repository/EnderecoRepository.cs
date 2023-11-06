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
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(MeuDbContext db) : base(db) { }

        public async Task<Endereco> ObeterMembroEnderecoPorMembro(Guid id)
        {
            return await _context.Enderecos.AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.MembroMcId == id);
        }
    }
}
