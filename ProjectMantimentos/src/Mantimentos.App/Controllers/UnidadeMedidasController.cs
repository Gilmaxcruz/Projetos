using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mantimentos.App.Business.Models;
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
        [Route("lista-de-unidade-de-medida")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<UnidadeMedidaViewModel>>(await _UnidadeMedidaRepository.GetUnidade()));
        }
        //Na Detalhes astraves do metodo ObterUnidadeSigla estamos resgatando os dados do repository e retornando a nossa viewModel
        [Route("detalhes-de-unidade-de-medida/{sigla}")]
        public async Task<IActionResult> Details(string sigla)
        {
            UnidadeMedidaViewModel unidadeMedidaViewModel = await ObterUnidadeSigla(sigla);
            if (unidadeMedidaViewModel == null)
            {
                return NotFound();
            }
            return View(unidadeMedidaViewModel);
        }
        //Chamada do Create, Necessariamente ser PartialView pois estamos trabalhando com um Modal,
        //Mas para retornar os erros validados pelo fluent retornarmos para view.
        [Route("nova-unidade-de-medida")]
        public IActionResult Create()
        {
            return View();
        }
        //Criando, nesse momento é esta sendo  resgatado a viewModel e validando,
        //efetuado o mapper e estornado para uma instancia de unidade preenchemendo a mesma e passado-a mesma
        //para ser salva atrasver do metodo PostUnidade
        [Route("nova-unidade-de-medida")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UnidadeMedidaViewModel unidadeMedidaViewModel)
        {

            if (ModelState.IsValid)
            {
               
                UnidadeMedida unidadeMedida = _mapper.Map<UnidadeMedida>(unidadeMedidaViewModel);
                _UnidadeMedidaRepository.PostUnidade(unidadeMedida);
                return RedirectToAction(nameof(Index));
            }

            return View(unidadeMedidaViewModel);
            // if (!ModelState.IsValid) return View(unidadeMedidaViewModel);
            // UnidadeMedida unidadeMedida = _mapper.Map<UnidadeMedida>(unidadeMedidaViewModel);
            //_UnidadeMedidaRepository.PostUnidade(unidadeMedida);
            // return RedirectToAction(nameof(Index));
        }
        [Route("edicao-de-unidade-de-medida/{sigla}")]
        public async Task<IActionResult> Edit(string sigla)
        {
            UnidadeMedidaViewModel unidadeMedidaViewModel = await ObterUnidadeSigla(sigla);
            if (unidadeMedidaViewModel == null)
            {
                return NotFound();
            }
            return View(unidadeMedidaViewModel);
        }
        [Route("edicao-de-unidade-de-medida/{sigla}")]
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
        [Route("exclusao-de-unidade-de-medida/{sigla}")]
        public async Task<IActionResult> Delete(string sigla)
        {
            UnidadeMedidaViewModel unidadeMedidaViewModel = await ObterUnidadeSigla(sigla);
            if (unidadeMedidaViewModel == null)
            {
                return NotFound();
            }
            return View(unidadeMedidaViewModel);
        }
        [Route("exclusao-de-unidade-de-medida/{sigla}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string sigla)
        {
            _UnidadeMedidaRepository.DeleteUnidade(sigla);
            return RedirectToAction("Index");
        }
        [Route("obter-unidade-de-medida-sigla/{sigla}")]
        private async Task<UnidadeMedidaViewModel> ObterUnidadeSigla(string sigla)
        {
            return _mapper.Map<UnidadeMedidaViewModel>(await _UnidadeMedidaRepository.GetUnidadeID(sigla));
        }
    }
}
