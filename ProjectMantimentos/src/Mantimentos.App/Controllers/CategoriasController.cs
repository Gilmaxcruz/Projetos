using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mantimentos.App.Business.Models;
using Mantimentos.App.Business.Interfaces;
using AutoMapper;
using Mantimentos.App.ViewModels;

namespace Mantimentos.App.Controllers
{/// <summary>
/// Controller Categorias
/// Obs.: Todas as controller serão limpas implementando os conseitos do projeto com a criação das Services, de momento não existe a viabilidade de acordo com as diretrizes passadas. 
/// </summary>
    public class CategoriasController : ExtensionController
    {

        private readonly ICategoriaRepository _CategoriaRepository;
        private readonly IMapper _mapper;
        public CategoriasController(ICategoriaRepository CategoriaRepository, IMapper mapper)
        {
            _CategoriaRepository = CategoriaRepository;
            _mapper = mapper;
        }
        //Carregado todas as categorias existentes no index.
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<CategoriaViewModel>>( await _CategoriaRepository.ObterTodos()));
        }
        public async Task<IActionResult> Details(Guid id)
        {
            CategoriaViewModel categoriaViewModel = await ObterCategoriaId(id);
            if (categoriaViewModel == null)
            {
                return NotFound();
            }
            return View(categoriaViewModel);
        }
        public IActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( CategoriaViewModel categoriaViewModel)
        {
            if (!ModelState.IsValid) return View(categoriaViewModel);
            Categoria categoria = _mapper.Map<Categoria>(categoriaViewModel);
            await _CategoriaRepository.Adicionar(categoria);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            CategoriaViewModel categoriaViewModel = await ObterCategoriaId(id);
            if (categoriaViewModel == null)
            {
                return NotFound();
            }
            return View(categoriaViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CategoriaViewModel categoriaViewModel)
        {
            if (id != categoriaViewModel.Id) return NotFound();
            if (!ModelState.IsValid) return View(categoriaViewModel);
            Categoria categoria = _mapper.Map<Categoria>(categoriaViewModel);
            await _CategoriaRepository.Atualizar(categoria);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            CategoriaViewModel categoriaViewModel = await ObterCategoriaId(id);
            if (categoriaViewModel == null)
            {
                return NotFound();
            }
            return View(categoriaViewModel);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _CategoriaRepository.Remover(id);
            return RedirectToAction("Index");
        }
        //Metodo ObterCategoriaId criado para melhor visualização de codigo, metodo privado para reduzir necessidade de passar mapeamento de busca de dados por id
        private async Task<CategoriaViewModel> ObterCategoriaId(Guid id)
        {
            return _mapper.Map<CategoriaViewModel>(await _CategoriaRepository.ObterPorId(id));
        }
    }
}
