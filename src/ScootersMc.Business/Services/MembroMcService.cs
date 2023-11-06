using ScootersMc.Business.Interfaces;
using ScootersMc.Business.Interfaces.IServices;
using ScootersMc.Business.Models;
using ScootersMc.Business.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScootersMc.Business.Services
{
    public class MembroMcService : BaseService, IMembroMcService
    {
        private readonly IMembroRepository _membroRepository;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IContatoEmergenciaRepository _contatoEmergenciaRepository;

        public MembroMcService(IMembroRepository mebro,
                               IEnderecoRepository enderco,
                               IContatoEmergenciaRepository contato,
                               INotificador notificador): base(notificador)
        {
            _membroRepository = mebro;
            _enderecoRepository = enderco;
            _contatoEmergenciaRepository = contato;
        }

        public async Task Adicionar(MembroMc membromc)
        {
            if (!ExecutarValidacao(new MembroMcValidation(), membromc)
                || !ExecutarValidacao(new EnderecoValidation(), membromc.Endereco)) return;

            if(_membroRepository.Buscar(m => m.Cpf == membromc.Cpf).Result.Any())
            {
                Notificar("Já existe um fornecedor com este documento infomado.");
                return;
            }

            await _membroRepository.Adicionar(membromc);
        }

        public async Task Atualizar(MembroMc membromc)
        {
            if (!ExecutarValidacao(new MembroMcValidation(), membromc)) return;
            
            if(_membroRepository.Buscar(m => m.Cpf == membromc.Cpf && m.Id != membromc.Id).Result.Any())
            {
                Notificar("Já existe um fornecedor com este documento infomado.");
                return;
            }

            await _membroRepository.Atualizar(membromc);
        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            if(!ExecutarValidacao(new EnderecoValidation(), endereco)) return;

            await _enderecoRepository.Atualizar(endereco);
        }


        public async Task Remover(Guid id)
        {
            var enderoco = await _enderecoRepository.ObeterMembroEnderecoPorMembro(id);
            var contato = await _contatoEmergenciaRepository.ObterContatoDeEmergenciasPorMembro(id);

            if (enderoco != null && contato != null)
            {
                await _enderecoRepository.Remover(enderoco.Id);
                await _contatoEmergenciaRepository.Remover(contato.Id);
            }

            await _membroRepository.Remover(id);
        }


        public void Dispose()
        {
            _membroRepository?.Dispose();
            _enderecoRepository?.Dispose();
        }
    }
}
