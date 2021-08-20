using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mantimentos.App.Business.Models;
using Mantimentos.App.Business.Interfaces;
using AutoMapper;
using Mantimentos.App.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Mantimentos.App.Extensions;

namespace Mantimentos.App.Controllers
{/// <summary>
/// Controller Categorias
/// Obs.: Todas as controller serão limpas implementando os conseitos do projeto com a criação das Services, de momento não existe a viabilidade de acordo com as diretrizes passadas. 
/// </summary>
    [Authorize]
    public class CategoriasController : ExtensionController
    {
        
        private readonly ICategoriaRepository _CategoriaRepository;
        private readonly IMapper _mapper;
        public CategoriasController(ICategoriaRepository CategoriaRepository, IMapper mapper)
        {
            _CategoriaRepository = CategoriaRepository;
            _mapper = mapper;
        }
        [AllowAnonymous]
        [Route("lista-de-categorias")]
        //Carregado todas as categorias existentes no index.
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<CategoriaViewModel>>( await _CategoriaRepository.ObterTodos()));
        }
        [AllowAnonymous]
        [Route("detalhes-da-categoria/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            CategoriaViewModel categoriaViewModel = await ObterCategoriaId(id);
            if (categoriaViewModel == null)
            {
                return NotFound();
            }
            return View(categoriaViewModel);
        }
        [ClaimsAuthorize("Categoria","Adicionar")]
        [Route("nova-categoria")]
        public IActionResult Create()
        {
            return PartialView();
        }
        [ClaimsAuthorize("Categoria", "Adicionar")]
        [Route("nova-categoria")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( CategoriaViewModel categoriaViewModel)
        {
            if (!ModelState.IsValid) return View(categoriaViewModel);
            Categoria categoria = _mapper.Map<Categoria>(categoriaViewModel);
            await _CategoriaRepository.Adicionar(categoria);
            return RedirectToAction(nameof(Index));
        }
        [ClaimsAuthorize("Categoria", "Editar")]
        [Route("edicao-da-categoria/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            CategoriaViewModel categoriaViewModel = await ObterCategoriaId(id);
            if (categoriaViewModel == null)
            {
                return NotFound();
            }
            return View(categoriaViewModel);
        }
        [ClaimsAuthorize("Categoria", "Editar")]
        [Route("edicao-da-categoria/{id:guid}")]
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
        [ClaimsAuthorize("Categoria", "Excluir")]
        [Route("excluir-a-categoria/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            CategoriaViewModel categoriaViewModel = await ObterCategoriaId(id);
            if (categoriaViewModel == null)
            {
                return NotFound();
            }
            return View(categoriaViewModel);
        }
        [ClaimsAuthorize("Categoria", "Excluir")]
        [Route("excluir-a-categoria/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _CategoriaRepository.Remover(id);
            return RedirectToAction("Index");
        }
        [Route("obter-categoria-id/{id:guid}")]
        //Metodo ObterCategoriaId criado para melhor visualização de codigo, metodo privado para reduzir necessidade de passar mapeamento de busca de dados por id
        private async Task<CategoriaViewModel> ObterCategoriaId(Guid id)
        {
            return _mapper.Map<CategoriaViewModel>(await _CategoriaRepository.ObterPorId(id));
        }
    }
}
