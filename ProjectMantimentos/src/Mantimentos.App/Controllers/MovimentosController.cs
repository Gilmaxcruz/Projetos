using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mantimentos.App.Business.Models;
using Mantimentos.App.Business.Interfaces;
using AutoMapper;
using Mantimentos.App.ViewModels;
using System.Linq;

namespace Mantimentos.App.Controllers
{/// <summary>
/// Controller Movimentos.
/// Obs.: Não foi implementado edição dos movimentos nem mesmo esclusão pois de momento não é interessante. Caso necessario seria implementado com maior viabilidade
/// Obs.: Todas as controller serão limpas implementando os conseitos do projeto com a criação das Services, de momento não existe a viabilidade de acordo com as diretrizes passadas. 
/// </summary>
    public class MovimentosController : ExtensionController
    {
        private readonly IMovimentoRepository _MovimentoRepository;
        private readonly IMantimentoRepository _mantimentoRepository;
        private readonly IMapper _mapper;
        public MovimentosController(IMovimentoRepository MovimentoRepository,
            IMapper mapper,
            IMantimentoRepository mantimentoRepository)
        {
            _MovimentoRepository = MovimentoRepository;
            _mapper = mapper;
            _mantimentoRepository = mantimentoRepository;
        }
        //Recebido na index todos os movimentos ja criados.
        //Alem disso como nosso mantimento é de uma forma composta exemplo temos LEITE que é o Tipo de Mantimento, e esse produto é diferenciado com a marca.
        //Foi necessario retornar algo mais viavel ao usuario, sendo assim foi criado em um Foreach o item.nome que retorna essa concatenação.
        //Metodo  de concatenação criado tambem para o Create.
        public async Task<IActionResult> Index()
        {
            List<MovimentoViewModel> movimentoViewModels = _mapper.Map<List<MovimentoViewModel>>(await _MovimentoRepository.ObterTodos());

            List<Mantimento> mantimentos = (await _mantimentoRepository.ObterNome()).ToList();
            foreach (var item in movimentoViewModels)
            {
                var teste = mantimentos.Where(f => f.Id == item.MantimentoId).FirstOrDefault();
                item.Nome = $"{teste.TpMantimento.Nome}-{teste.Marca.Nome}";
            }

            return View(movimentoViewModels);
        }
        public async Task<IActionResult> Details(Guid id)
        {
            MovimentoViewModel movimentoViewModel = await ObterMovimentoId(id);
            if (movimentoViewModel == null)
            {
                return NotFound();
            }
            return View(movimentoViewModel);
        }
        public async Task<IActionResult> Create()
        {
            MovimentoViewModel movimentoViewModel = new();
            movimentoViewModel.MovimentoSelectViewModel = (await _mantimentoRepository.ObterNome()).ToList().Select(f => new MovimentoSelectViewModel()
            {
                IdMantimento = f.Id,
                Nome = $"{f.TpMantimento.Nome}-{f.Marca.Nome}"
            }).ToList();
            return PartialView(movimentoViewModel);
        }

        //Criado metodo AtualizarMantimentoPorId para poder ser possivel a movimentação do estoque e recalculos necessarios.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovimentoViewModel movimentoViewModel)
        {
            if (!ModelState.IsValid) return View(movimentoViewModel);
            Movimento movimento = _mapper.Map<Movimento>(movimentoViewModel);
            await _MovimentoRepository.Adicionar(movimento);
            _mantimentoRepository.AtualizarMantimentoPorId(movimentoViewModel.MantimentoId, movimentoViewModel.Quantidade, movimentoViewModel.TipoMovimento);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            MovimentoViewModel movimentoViewModel = await ObterMovimentoId(id);
            if (movimentoViewModel == null)
            {
                return NotFound();
            }
            return View(movimentoViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, MovimentoViewModel movimentoViewModel)
        {
            if (id != movimentoViewModel.Id) return NotFound();
            if (!ModelState.IsValid) return View(movimentoViewModel);
            Movimento movimento = _mapper.Map<Movimento>(movimentoViewModel);
            await _MovimentoRepository.Atualizar(movimento);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            MovimentoViewModel movimentoViewModel = await ObterMovimentoId(id);
            if (movimentoViewModel == null)
            {
                return NotFound();
            }
            return View(movimentoViewModel);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _MovimentoRepository.Remover(id);
            return RedirectToAction("Index");
        }
        //Metodo ObterMovimentoId criado para melhor visualização de codigo, metodo privado para reduzir necessidade de passar mapeamento de busca de dados por id
        private async Task<MovimentoViewModel> ObterMovimentoId(Guid id)
        {
            return _mapper.Map<MovimentoViewModel>(await _MovimentoRepository.ObterPorId(id));
        }
    }
}
