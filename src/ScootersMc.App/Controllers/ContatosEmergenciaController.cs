using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScootersMc.App.Data;
using ScootersMc.App.ViewModels;
using ScootersMc.Business.Interfaces;
using ScootersMc.Business.Models;

namespace ScootersMc.App.Controllers
{
    public class ContatosEmergenciaController : BaseController
    {
        private readonly IContatoEmergenciaRepository _contatoEmergenciaRepository;
        private readonly IMapper _mapper;


        public ContatosEmergenciaController(IContatoEmergenciaRepository contato,
                                            IMapper mapper)
        {
            _contatoEmergenciaRepository = contato;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ContatoEmergenciaViewModel>>
                (await _contatoEmergenciaRepository.ObterTodos()));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var Contato = await ObterContatoEmergencial(id);
            
            if (Contato == null) return NotFound();

            return View(Contato);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContatoEmergenciaViewModel contatoEmergenciaViewModel)
        {
            if (!ModelState.IsValid) return NotFound();

            var contato = _mapper.Map<ContatoEmergencia>(contatoEmergenciaViewModel);
            
            await _contatoEmergenciaRepository.Adicionar(contato);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var contato = await ObterContatoEmergencial(id);
            if (contato == null) return NotFound();

            return View(contato);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,  ContatoEmergenciaViewModel contatoEmergenciaViewModel)
        {
            if (id != contatoEmergenciaViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return NotFound();

            var contato = _mapper.Map<ContatoEmergencia>(contatoEmergenciaViewModel);

            await _contatoEmergenciaRepository.Atualizar(contato);

            return RedirectToAction("Index");
            
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var contato = await ObterContatoEmergencial(id);

            if (contato == null) return NotFound();

            return View(contato);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var contato = ObterContatoEmergencial(id);

            if (contato == null) return NotFound();

            await _contatoEmergenciaRepository.Remover(id);

            return RedirectToAction(nameof(Index));
        }


        private async Task<ContatoEmergenciaViewModel>ObterContatoEmergencial(Guid id)
        {
            return _mapper.Map<ContatoEmergenciaViewModel>(await _contatoEmergenciaRepository
                                                                  .ObterContatoDeEmergenciasPorMembro(id));
        }

        private async Task<ContatoEmergenciaViewModel>ObterContatoEmergencialMembro(Guid id)
        {
            return _mapper.Map<ContatoEmergenciaViewModel>(await _contatoEmergenciaRepository
                                                                 .ObterContatoDeEmergenciaPorMembroPorId(id));
        }
    }

}
 