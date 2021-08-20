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
    /// Controller Mantimentos
    /// Obs.: Todas as controller serão limpas implementando os conseitos do projeto com a criação das Services, de momento não existe a viabilidade de acordo com as diretrizes passadas. 
    /// </summary>

    public class MantimentosController : ExtensionController
    {
        private readonly IMantimentoRepository _MantimentoRepository;
        private readonly IMarcaRepository _marcaRepository;
        private readonly IUnidadeMedidaRepository _unidadeMedidaRepository;
        private readonly ITpMantimentoRepository _tpMantimentoRepository;
        private readonly IMapper _mapper;
        public MantimentosController(IMantimentoRepository MantimentoRepository,
                                     IMapper mapper,
                                     IMarcaRepository marcaRepository,
                                     IUnidadeMedidaRepository unidadeMedidaRepository,
                                     ITpMantimentoRepository tpMantimentoRepository)
        {
            _MantimentoRepository = MantimentoRepository;
            _mapper = mapper;
            _marcaRepository = marcaRepository;
            _unidadeMedidaRepository = unidadeMedidaRepository;
            _tpMantimentoRepository = tpMantimentoRepository;
        }

        public async Task<IActionResult> Index(MantimentoFilterViewModel mantimentoFilterViewModel)
        {
            //Criado a instancia para podermos receber e consultar os mesmos 
            Mantimento mantimento = new()
            {
                TpMantimento = new() 
                { 
                    Nome = mantimentoFilterViewModel.NomeTpMantimento
                },
                Marca = new()
                {
                    Nome = mantimentoFilterViewModel.NomeMarca
                },
                UnidadeMedida = new()
                {
                    Unidade = mantimentoFilterViewModel.NomeUnidade
                }
            };

            ViewBag.NomeTpMantimento = mantimentoFilterViewModel.NomeTpMantimento;
            ViewBag.NomeMarca = mantimentoFilterViewModel.NomeMarca;
            ViewBag.NomeUnidadeMedida = mantimentoFilterViewModel.NomeUnidade;
            return View(_mapper.Map<IEnumerable<MantimentoViewModel>>( await _MantimentoRepository.ObterTDados(mantimento)));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            MantimentoViewModel mantimentoViewModel = await ObterMantimentoId(id);
            if (mantimentoViewModel == null)
            {
                return NotFound();
            }
            return View(mantimentoViewModel);
        }

        public async Task<IActionResult> Create()
        {
            //Obterndo a listagem para no momento da criação do mantimento selecionar os mesmos
            MantimentoViewModel mantimento = new MantimentoViewModel();
            mantimento.Marcas = _mapper.Map<List<MarcaViewModel>>(await _marcaRepository.ObterTodos());
            mantimento.TpMantimentos = _mapper.Map<List<TpMantimentoViewModel>>(await _tpMantimentoRepository.ObterTodos());
            mantimento.UnidadeMedidas = _mapper.Map<List<UnidadeMedidaViewModel>>(await _unidadeMedidaRepository.GetUnidade());

            return PartialView(mantimento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MantimentoViewModel mantimentoViewModel)
        {
            //Passados dados manualmente padrões de criação porque a ideia é que sejam carregados de acordo com as movimentações.
            //A imagem não foi passado uma vez que não será anexado nesse projeto de momento, fica para quando estiver um time melhor.
            if (!ModelState.IsValid) return View(mantimentoViewModel);
            Mantimento mantimento = _mapper.Map<Mantimento>(mantimentoViewModel);
            mantimento.Estoque = 0;
            mantimento.ConteudoAtual = "0";
            mantimento.Imagem = "imagem";
            await _MantimentoRepository.Adicionar(mantimento);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            //Mesma situação do create, recebendo os dados para ser possivel seleção durante a edição
            MantimentoViewModel mantimentoViewModel = await ObterMantimentoId(id);
            mantimentoViewModel.Marcas = _mapper.Map<List<MarcaViewModel>>(await _marcaRepository.ObterTodos());
            mantimentoViewModel.TpMantimentos = _mapper.Map<List<TpMantimentoViewModel>>(await _tpMantimentoRepository.ObterTodos());
            mantimentoViewModel.UnidadeMedidas = _mapper.Map<List<UnidadeMedidaViewModel>>(await _unidadeMedidaRepository.GetUnidade());
            if (mantimentoViewModel == null)
            {
                return NotFound();
            }
            return View(mantimentoViewModel);
        }
       
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,  MantimentoViewModel mantimentoViewModel)
        {
            if (id != mantimentoViewModel.Id) return NotFound();
            if (!ModelState.IsValid) return View(mantimentoViewModel);
            Mantimento mantimento =  _mapper.Map<Mantimento>(mantimentoViewModel);
            mantimento.Imagem = "imagem";

            await _MantimentoRepository.Atualizar(mantimento);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            MantimentoViewModel mantimentoViewModel = await ObterMantimentoId(id);
            if (mantimentoViewModel == null)
            {
                return NotFound();
            }
            return View(mantimentoViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _MantimentoRepository.Remover(id);
            return RedirectToAction("Index");
        }
        //Metodo ObterMantimentoId criado para melhor visualização de codigo, metodo privado para reduzir necessidade de passar mapeamento de busca de dados por id
        private async Task<MantimentoViewModel> ObterMantimentoId(Guid id)
        {
            return _mapper.Map<MantimentoViewModel>(await _MantimentoRepository.ObterPorId(id));
        }
    }
}
