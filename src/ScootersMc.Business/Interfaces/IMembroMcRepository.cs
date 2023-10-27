using ScootersMc.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScootersMc.Business.Interfaces
{
    public interface IMembroRepository : IRepository<MembroMc>
    {
        Task<MembroMc> ObterMembroEndereco(Guid id);

        Task<MembroMc> ObterMembroContatosEndereco(Guid id);
    }
}
