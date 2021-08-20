using Identity.Extensions;
using Identity.Models;
using KissLog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger _logger; //Esse log é do Kisslog using KissLog;
        public HomeController(ILogger logger)
        {
            _logger = logger;
        }

        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger) //esse log é do extesion da microsoft
        //{
        //    _logger = logger;
        //}
        [AllowAnonymous]
        public IActionResult Index()
        {
            _logger.Debug("Hello world from .NET Core 5.x!");
            return View();
        }


        public IActionResult Privacy()
        {
            throw new Exception("Erro");
            return View();
        }

        [Authorize(Roles = "ADMIN, GESTOR")]//papeis
        public IActionResult Secret()
        {
            return View();
        }


        [Authorize(Policy = "PodeExcluir")]//permissao, podemos criar nas clainValue, uma permissao geral
                                           //separando cada parametro que ela pode conter por virgula: permissaoExcluir,permissaoLer,permissaoEditar
        public IActionResult SecretClain()
        {
            return View("SecretClain");
        }

        [Authorize(Policy = "PodeEscrever")]
        public IActionResult SecretClainGravar()
        {
            return View("SecretClainGravar");
        }

        [ClainsAuthorizeAttribute("Produtos", "Ler")]
        public IActionResult SecretClainCustom()
        {
            return View("SecretClainGravar");
        }


       
        [Route("erro/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var modelErro = new ErrorViewModel();
            if (id == 500)
            {
                modelErro.Mensagem = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
                modelErro.Titulo = "Ocorreu um error!";
                modelErro.ErroCode = id;
            }
            else if (id == 404)
            {
                modelErro.Mensagem = "A página que está procurando não existe! <br /> Em caso de dúvidas entre em contato com nosso suporte";
                modelErro.Titulo = "Ops! Página não encontrada.";
                modelErro.ErroCode = id;
            }
            else if (id == 403)
            {
                modelErro.Mensagem = "Você não tem permissão para fazer isto.";
                modelErro.Titulo = "Acesso Negado";
                modelErro.ErroCode = id;
            }
            else return StatusCode(404);

            return View("Error", modelErro);
        }
    }
}
