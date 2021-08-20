using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mantimentos.App.Business.Models;
using Mantimentos.App.Data.Context;
using Mantimentos.App.Business.Interfaces;
using AutoMapper;
using Mantimentos.App.ViewModels;

namespace Mantimentos.App.Controllers
{
    /// <summary>
    /// Controler Mantimentos
    /// Obs.: Todas as controller serão limpas implementando os conseitos do projeto com a criação das Services, de momento não existe a viabilidade de acordo com as diretrizes passadas. 
    /// </summary>
    public class UnidadeMedidasController : ExtensionController
    {
        private readonly IUnidadeMedidaRepository _UnidadeMedidaRepository;
        private readonly IMapper _mapper;

        public UnidadeMedidasController(IUnidadeMedidaRepository UnidadeMedidaRepository, IMapper mapper)
        {
            _UnidadeMedidaRepository = UnidadeMedidaRepository;
            _mapper = mapper;
        }

        //Atravez do Index ja estamos mapeando e retornando as unidades existentes no banco para ser possivel preencher nossa view
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<UnidadeMedidaViewModel>>(await _UnidadeMedidaRepository.GetUnidade()));
        }
        //Na Detalhes astraves do metodo ObterUnidadeSigla estamos resgatando os dados do repository e retornando a nossa viewModel
        public async Task<IActionResult> Details(string sigla)
        {
            UnidadeMedidaViewModel unidadeMedidaViewModel = await ObterUnidadeSigla(sigla);
            if (unidadeMedidaViewModel == null)
            {
                return NotFound();
            }
            return View(unidadeMedidaViewModel);
        }
        //Chamada do Create, Necessariamente ser PartialView pois estamos trabalhando com um Modal.
        public IActionResult Create()
        {
            return PartialView();
        }
        //Criando, nesse momento é esta sendo  resgatado a viewModel e validando,
        //efetuado o mapper e estornado para uma instancia de unidade preenchemendo a mesma e passado-a mesma
        //para ser salva atrasver do metodo PostUnidade
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UnidadeMedidaViewModel unidadeMedidaViewModel)
        {
            if (!ModelState.IsValid) return View(unidadeMedidaViewModel);
            UnidadeMedida unidadeMedida = _mapper.Map<UnidadeMedida>(unidadeMedidaViewModel);
           _UnidadeMedidaRepository.PostUnidade(unidadeMedida);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(string sigla)
        {
            UnidadeMedidaViewModel unidadeMedidaViewModel = await ObterUnidadeSigla(sigla);
            if (unidadeMedidaViewModel == null)
            {
                return NotFound();
            }
            return View(unidadeMedidaViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string sigla, UnidadeMedidaViewModel unidadeMedidaViewModel)
        {
            if (sigla != unidadeMedidaViewModel.Sigla) return NotFound();
            if (!ModelState.IsValid) return View(unidadeMedidaViewModel);
            UnidadeMedida unidadeMedida = _mapper.Map<UnidadeMedida>(unidadeMedidaViewModel);
            _UnidadeMedidaRepository.PutUnidade(unidadeMedida);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(string sigla)
        {
            UnidadeMedidaViewModel unidadeMedidaViewModel = await ObterUnidadeSigla(sigla);
            if (unidadeMedidaViewModel == null)
            {
                return NotFound();
            }
            return View(unidadeMedidaViewModel);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string sigla)
        {
            _UnidadeMedidaRepository.DeleteUnidade(sigla);
            return RedirectToAction("Index");
        }
        private async Task<UnidadeMedidaViewModel> ObterUnidadeSigla(string sigla)
        {
            return _mapper.Map<UnidadeMedidaViewModel>(await _UnidadeMedidaRepository.GetUnidadeID(sigla));
        }
    }
}
