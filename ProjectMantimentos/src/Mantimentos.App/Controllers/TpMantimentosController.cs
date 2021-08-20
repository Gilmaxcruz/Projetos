using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mantimentos.App.Business.Models;
using Mantimentos.App.Business.Interfaces;
using System;
using AutoMapper;
using Mantimentos.App.ViewModels;
using System.Collections.Generic;
using X.PagedList;
using System.Linq;

namespace Mantimentos.App.Controllers
{
    /// <summary>
    /// Controller TpMantimentos
    /// Obs.: Todas as controller serão limpas implementando os conseitos do projeto com a criação das Services, de momento não existe a viabilidade de acordo com as diretrizes passadas. 
    /// </summary>
    public class TpMantimentosController : ExtensionController
    {
        private readonly ITpMantimentoRepository _TpMantimentoRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        public TpMantimentosController(ITpMantimentoRepository TpMantimentoRepository,
                                        IMapper mapper,
                                        ICategoriaRepository categoriaRepository)
        {
            _TpMantimentoRepository = TpMantimentoRepository;
            _mapper = mapper;
            _categoriaRepository = categoriaRepository;
        }
        public async Task<IActionResult> Index(TpMantimentoFilterViewModel tpMantimentoFilterViewModel)
        {
            //Já na index criado a utilização de paglist,
            //nesse ponto foi encontrado um desafio que era retornar toda a listagem de tpMantimentos Categoria de acordo com cada TpMantimento.
            //Foi feito um foreach para popular De acordo com cada Tipo de mantimento suas categorias. 

            const int itensPorPagina = 5;
            int numeroPagina = (tpMantimentoFilterViewModel.pag ?? 1);

            ViewBag.Nome = tpMantimentoFilterViewModel.Nome;

            TpMantimento tpfilter = new()
            {
                Nome = tpMantimentoFilterViewModel.Nome
            };

            List<TpMantimento> mantimentos = await  _TpMantimentoRepository.ObterTDados(tpfilter);
            List<TpMantimentoViewModel> query2 =  _mapper.Map<List<TpMantimentoViewModel>>(mantimentos);
            foreach (var item in query2)
            {
                TpMantimento t = mantimentos.Where(c => c.Id == item.Id).FirstOrDefault();

                List<TpMantimentoCategoriaViewModel> t2 = t.TpMantimentoCategoria.Select(x=> new TpMantimentoCategoriaViewModel()
                {
                    CategoriaId = x.CategoriaId
                }).ToList();
                    
                foreach (var item2 in t2)
                {
                    item2.Categoria = _mapper.Map<CategoriaViewModel>(await _categoriaRepository.ObterPorId(item2.CategoriaId));
                }
                item.TpMantimentoCategoriaViewModels = t2;
            }
            IPagedList<TpMantimentoViewModel> query = query2.ToPagedList(numeroPagina, itensPorPagina);
            return View(query);
        }
        public async Task<IActionResult> Details(Guid id)
        {
            TpMantimentoViewModel tpMantimentoViewModel = await ObterTpMantimentoId(id);
            if (tpMantimentoViewModel == null)
            {
                return NotFound();
            }
            return View(tpMantimentoViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            TpMantimentoViewModel TpmantimentoViewModel = new();
            TpmantimentoViewModel.Categorias = await PopularCategorias();

            return PartialView(TpmantimentoViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Criar(TpMantimentoViewModel tpMantimentoViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!ModelState.IsValid) return View(tpMantimentoViewModel);
                    TpMantimento tpMantimento = _mapper.Map<TpMantimento>(tpMantimentoViewModel);
                    tpMantimento.Id = Guid.NewGuid();

                    List<TpMantimentoCategoria> categorias = new();
                    foreach (var item in tpMantimentoViewModel.CategoriaIds)
                    {
                        Categoria categoria = await _categoriaRepository.ObterPorId(item);
                        TpMantimentoCategoria tpcategoria = new()
                        {
                            Id = Guid.NewGuid(),
                            CategoriaId = item,
                            MantimentoTpId = tpMantimento.Id,
                        };
                        categorias.Add(tpcategoria);
                    }
                    tpMantimento.TpMantimentoCategoria.AddRange(categorias);
                    await _TpMantimentoRepository.Adicionar(tpMantimento);
                    return RedirectToAction(nameof(Index));
                }
                return View(tpMantimentoViewModel);
            }
             catch (Exception ex)
            {
                throw;
            }
         
        }
        public async Task<IActionResult> Edit(Guid id)
        {

            TpMantimentoViewModel tpMantimentoViewModel = await ObterTpMantimentoId(id);
            if (tpMantimentoViewModel == null)
            {
                return NotFound();
            }
            tpMantimentoViewModel.Categorias = await PopularCategorias();
            return View(tpMantimentoViewModel);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(TpMantimentoViewModel tpMantimentoViewModel)
        {
            //ja no editar é necessario desconstruir as categorias ja relacionadas em TpMantiemntoCategoria e permitir recriar.
            //Sendo assim uma vez que a edição passou teve a alteração é removido o que existia relacionado de tpMantimentos e recriado com base na edição.
            //Essa parte é necessario ser melhorada e será quando possivel pois perde a essecia do Solid uma vez que não tem sentindo ter mais de uma ação.
            try
            {
                if (ModelState.IsValid)
                {
                    if (!ModelState.IsValid) return View(tpMantimentoViewModel);
                    _TpMantimentoRepository.RemoverTpMantimentoCategoriaById(tpMantimentoViewModel.Id);
                    TpMantimento tpMantimento = _mapper.Map<TpMantimento>(tpMantimentoViewModel);

                    await _TpMantimentoRepository.Atualizar(tpMantimento);
                    foreach (var item in tpMantimentoViewModel.CategoriaIds)
                    {
                        Categoria categoria = await _categoriaRepository.ObterPorId(item);
                        TpMantimentoCategoria tpcategoria = new()
                        {
                            Id = Guid.NewGuid(),
                            CategoriaId = item,
                            MantimentoTpId = tpMantimento.Id,
                        };
                        _TpMantimentoRepository.AdicionarTpMantimentoCategoria(tpcategoria);
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(tpMantimentoViewModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            TpMantimentoViewModel tpMantimentoViewModel = await ObterTpMantimentoId(id);
            if (tpMantimentoViewModel == null)
            {
                return NotFound();
            }
            return View(tpMantimentoViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _TpMantimentoRepository.Remover(id);
            return RedirectToAction("Index");
        }
        //Criado o metodo ObterTpMantimentoId para não ter necessidade de passar sempre o mapeamento no codigo, deixando mais Clean.
        private async Task<TpMantimentoViewModel> ObterTpMantimentoId(Guid id)
        {
            return _mapper.Map<TpMantimentoViewModel>(await _TpMantimentoRepository.ObterPorId(id));
        }
        //Criado o metodo PopularCategorias para coletarmos a lista das categorias que seram utilizadas para criação e edição.
        private async Task<List<CategoriaViewModel>> PopularCategorias()
        {
            List<CategoriaViewModel> categoria = _mapper.Map<List<CategoriaViewModel>>(await _categoriaRepository.ObterTodos());
            return categoria;
        }
    }
}
