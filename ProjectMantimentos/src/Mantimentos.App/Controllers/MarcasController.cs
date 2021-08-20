using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mantimentos.App.Business.Models;
using Mantimentos.App.Business.Interfaces;
using System;
using AutoMapper;
using Mantimentos.App.ViewModels;
using System.Collections.Generic;

namespace Mantimentos.App.Controllers
{
    /// <summary>
    /// Controller Marcas
    /// Obs.: Todas as controller serão limpas implementando os conseitos do projeto com a criação das Services, de momento não existe a viabilidade de acordo com as diretrizes passadas. 
    /// </summary>
    public class MarcasController : ExtensionController
    {
        private readonly IMarcaRepository _MarcaRepository;
        private readonly IMapper _mapper;
        public MarcasController(IMarcaRepository MarcaRepository, IMapper mapper)
        {
            _MarcaRepository = MarcaRepository;
            _mapper = mapper;
        }

        //Retornando no index todos as marcas ja existentes.
        public async Task<IActionResult> Index()
        {
            return View( _mapper.Map<IEnumerable<MarcaViewModel>>(await _MarcaRepository.ObterTodos()));
        }
        public async Task<IActionResult> Details(Guid id)
        {
            MarcaViewModel marcaViewModel = await ObterMarcaId(id);
            if (marcaViewModel == null)
            {
                return NotFound();
            }
            return View(marcaViewModel);
        }
        //Create retornando PartialView por conta do Modal existente
        public IActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MarcaViewModel marcaViewModel)
        {
            if (!ModelState.IsValid) return View(marcaViewModel);
            Marca marca = _mapper.Map<Marca>(marcaViewModel);
            await _MarcaRepository.Adicionar(marca);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            MarcaViewModel marcaViewModel = await ObterMarcaId(id);
            if (marcaViewModel == null)
            {
                return NotFound();
            }
            return View(marcaViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, MarcaViewModel marcaViewModel)
        {
            if (id != marcaViewModel.Id) return NotFound();
            if (!ModelState.IsValid) return View(marcaViewModel);
            Marca marca = _mapper.Map<Marca>(marcaViewModel);
            await _MarcaRepository.Atualizar(marca);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            MarcaViewModel marcaViewModel = await ObterMarcaId(id);
            if (marcaViewModel == null)
            {
                return NotFound();
            }
            return View(marcaViewModel);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _MarcaRepository.Remover(id);
            return RedirectToAction("Index");
        }
        //Metodo ObterMarcaId criado para melhor visualização de codigo, metodo privado para reduzir necessidade de passar mapeamento de busca de dads=os por id
        private async Task<MarcaViewModel> ObterMarcaId(Guid id)
        {
            return _mapper.Map<MarcaViewModel>(await _MarcaRepository.ObterPorId(id));
        }
    }
}
