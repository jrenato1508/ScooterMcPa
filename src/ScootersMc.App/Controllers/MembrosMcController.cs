using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScootersMc.App.Data;
using ScootersMc.App.ViewModels;
using ScootersMc.Business.Interfaces;
using ScootersMc.Business.Interfaces.IServices;
using ScootersMc.Business.Models;
using ScootersMc.Data.Context;
using ScootersMc.Data.Repository;

namespace ScootersMc.App.Controllers
{
    [Route("Membros-moto-clube")]
    public class MembrosMcController : BaseController
    {
        private readonly IMembroRepository _membrosRepository;
        private readonly IMapper _mapper;
        private readonly IContatoEmergenciaRepository _contatoEmergenciaRepository;
        private readonly IMembroMcService _membroMcService;

        public MembrosMcController(IMembroRepository membroRepository,
                                   IMapper mapper,
                                   IContatoEmergenciaRepository contato,
                                   IMembroMcService membroMcService,
                                   INotificador notificador) :base(notificador)
        {
            _membrosRepository = membroRepository;
            _mapper = mapper;
            _contatoEmergenciaRepository = contato;
            _membroMcService = membroMcService;

        }

        [Route("lista-de-associados")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<MembroMcViewModel>>(await _membrosRepository.ObterTodos()));
        }

        [Route("Detalhes-do-associado")]
        public async Task<IActionResult> Details(Guid id)
        {
            var MembroMcViewModel = await ObterMembroContatosEndereco(id);

            if (MembroMcViewModel == null)
            {
                return NotFound();
            }
            return View(MembroMcViewModel);
        }

        [Route("Adicionar-novo-associado")]
        public IActionResult Create()
        {
            return View();
        }

        [Route("Adicionar-novo-associado")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MembroMcViewModel membroMcViewModel)
        {

            if (!ModelState.IsValid) return NotFound();

            CalcularIdade(membroMcViewModel);

            var imgPrefixo = Guid.NewGuid() + "_";

            if (!await UpLoadArquivo(membroMcViewModel.ImagemUpload, imgPrefixo))
            {
                return View(membroMcViewModel);
            }

            membroMcViewModel.Imagem = imgPrefixo + membroMcViewModel.ImagemUpload.FileName;

            /* Testar a implementação da imagem - OK
             * Testar a Edição dos Filiados - OK
             * Criei uma razoExtension para formatar telefone e cpf -OK
             * Testar a Exclusão dos Afiliados(Implementar o Service Primeiro) - OK
             * Foi feito alguns ajustes das paginas Layout index e Detalhes além de ajustar algumas actions na controller 
             * Criar a os metodos para notificar os erros encontrados durante a validação(create,edit) - OK
             * criar um mecanismo para exibir os erros encontrado durante a validação da entida para o usuário.Vamos utilizar um ViewComponent
             * para mostrar esses erros.(item 35) -OK Falta testar
              */


            await _membroMcService.Adicionar(_mapper.Map<MembroMc>(membroMcViewModel));

            if(!OperacaoValida()) return View(membroMcViewModel);

            return RedirectToAction("Index");
        }

        [Route("Editar-associado")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var membromc = await ObterMembroContatosEndereco(id);

            if (membromc == null) { return NotFound(); }

            return View(membromc);
        }

        [Route("Editar-associado")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, MembroMcViewModel membroMcViewModel)
        {
            if (id != membroMcViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(membroMcViewModel);

            if (membroMcViewModel.ImagemUpload != null)
            {
                var imgPrefico = Guid.NewGuid() + "_";
                if (await UpLoadArquivo(membroMcViewModel.ImagemUpload, imgPrefico))
                {
                    return View(membroMcViewModel);
                }
            }

            await AtualizarMembroMc(id, membroMcViewModel);

            CalcularIdade(membroMcViewModel);

            var membromc = _mapper.Map<MembroMc>(membroMcViewModel);

            await _membroMcService.Atualizar(membromc);

            if(!OperacaoValida()) return View(membroMcViewModel);

            return RedirectToAction("Index");
        }

        [Route("Excluir-associado")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var membromc = await ObterMembroContatosEndereco(id);

            if (membromc == null) { return NotFound(); }


            return View(membromc);
        }

        [Route("Excluir-associado")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var membromc = ObterMembroContatosEndereco(id);

            if (membromc == null) { return NotFound(); }


            await _membroMcService.Remover(id);

            if (!OperacaoValida()) return View(membromc);

            return RedirectToAction(nameof(Index));
        }



        private async Task<MembroMcViewModel> ObterMembroContatosEndereco(Guid id)
        {
            return _mapper.Map<MembroMcViewModel>(await _membrosRepository.ObterMembroContatosEndereco(id));
        }


        private async Task<bool> UpLoadArquivo(IFormFile arquivo, string imgPrefixo)
        {
            if (arquivo.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", imgPrefixo + arquivo.FileName);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com esse nome!");
            }

            using (var steam = new FileStream(path, FileMode.Create))
            {
                await arquivo.CopyToAsync(steam);
            }

            return true;
        }

        private MembroMcViewModel CalcularIdade(MembroMcViewModel MembroMc)
        {
            DateTime AnoNascimento = MembroMc.DataNascimento;
            TimeSpan Diferenca = DateTime.Today - AnoNascimento;
            DateTime Idade = (new DateTime() + Diferenca).AddYears(-1).AddDays(-1);
            MembroMc.Idade = Idade.Year;

            return MembroMc;
        }

        private async Task<MembroMcViewModel> AtualizarMembroMc(Guid id, MembroMcViewModel membromc)
        {
            var membroAtualizacao = await ObterMembroContatosEndereco(id);
            membromc.Imagem = membroAtualizacao.Imagem;

            membroAtualizacao.Nome = membromc.Nome;
            membroAtualizacao.Cpf = membromc.Cpf;
            membroAtualizacao.Email = membromc.Email;
            membroAtualizacao.Hierarquia = membromc.Hierarquia;
            membroAtualizacao.TipoSanguineo = membromc.TipoSanguineo;
            membroAtualizacao.DataNascimento = membromc.DataNascimento;
            membroAtualizacao.Telefone = membromc.Telefone;
            membroAtualizacao.Ativo = membromc.Ativo;

            return membroAtualizacao;
        }

    }
}
