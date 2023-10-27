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
using ScootersMc.Business.Models;
using ScootersMc.Data.Context;
using ScootersMc.Data.Repository;

namespace ScootersMc.App.Controllers
{
    public class MembrosMcController : BaseController
    {
        private readonly IMembroRepository _membrosRepository;
        private readonly IMapper _mapper;
        private readonly IContatoEmergenciaRepository _contatoEmergenciaRepository;

        public MembrosMcController(IMembroRepository membroRepository,
                                   IMapper mapper,
                                   IContatoEmergenciaRepository contato)
        {
            _membrosRepository = membroRepository;
            _mapper = mapper;
            _contatoEmergenciaRepository = contato;

        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<MembroMcViewModel>>(await _membrosRepository.ObterTodos()));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var MembroMcViewModel = ObterMembroContatosEndereco(id);

            if (MembroMcViewModel == null)
            {
                return NotFound();
            }
            return View(MembroMcViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( MembroMcViewModel membroMcViewModel)
        {
            
            if (!ModelState.IsValid) return NotFound();

            CalcularIdade(membroMcViewModel);

            var imgPrefixo = Guid.NewGuid() + "_";
            
            if (!await UpLoadArquivo(membroMcViewModel.ImagemUpload, imgPrefixo))
            {
                return View(membroMcViewModel);
            }

            membroMcViewModel.Imagem = imgPrefixo + membroMcViewModel.ImagemUpload.FileName;

            

            // Testar a implementação da imagem

            await _membrosRepository.Adicionar( _mapper.Map<MembroMc>(membroMcViewModel));

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var membromc = ObterMembroContatosEndereco(id);

            if(membromc == null) { return NotFound(); }

            return View(membromc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, MembroMcViewModel membroMcViewModel)
        {
            if (id != membroMcViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(membroMcViewModel);

            var membromc = _mapper.Map<MembroMc>(membroMcViewModel);

            await _membrosRepository.Atualizar(membromc);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var membromc = ObterMembroContatosEndereco(id);
            
            if(membromc == null) { return NotFound(); }


            return View(membromc);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var membromc = ObterMembroContatosEndereco(id);

            if(membromc == null) { return NotFound(); }


            await _membrosRepository.Remover(id);

            return RedirectToAction(nameof(Index));
        }


        
        private async Task<MembroMcViewModel> ObterMembroContatosEndereco(Guid id)
        {
            return _mapper.Map<MembroMcViewModel>(await _membrosRepository.ObterMembroContatosEndereco(id));
        }


        private async Task<bool> UpLoadArquivo(IFormFile arquivo , string imgPrefixo)
        {
            if(arquivo.Length <=0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", imgPrefixo + arquivo.FileName);

            if(System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com esse nome!");
            }

            using(var steam = new FileStream(path, FileMode.Create))
            {
                await arquivo.CopyToAsync(steam);
            }

            return true; 
        }

        protected MembroMcViewModel CalcularIdade(MembroMcViewModel MembroMc)
        {
            DateTime AnoNascimento = MembroMc.DataNascimento;
            DateTime AnoAtual = DateTime.Now;
            TimeSpan Diferenca = DateTime.Today - AnoNascimento;
            DateTime Idade = (new DateTime() + Diferenca).AddYears(-1).AddDays(-1);
            MembroMc.Idade = Idade.Year;

            return MembroMc;
        }


      
    }
}
