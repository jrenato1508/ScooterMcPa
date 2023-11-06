using ScootersMc.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScootersMc.Business.Interfaces.IServices
{
    public interface IMembroMcService : IDisposable
    {
        Task Adicionar(MembroMc membromc);

        Task Atualizar(MembroMc membromc);

        Task Remover(Guid id);

        Task AtualizarEndereco(Endereco endereco);
    }
}
