using AutoMapper;
using Mantimentos.App.Business.Interfaces;
using Mantimentos.App.Business.JWT;
using Mantimentos.App.Business.Models;
using Mantimentos.App.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Mantimentos.App.Controllers
{/// <summary>
 /// Controller Home
 /// Obs.: Todas as controller serão limpas implementando os conseitos do projeto com a criação das Services, de momento não existe a viabilidade de acordo com as diretrizes passadas. 
 /// </summary>

    public class HomeController : Controller
    {

        private readonly IMantimentoRepository _MantimentoRepository;
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        

        public HomeController(ILogger<HomeController> logger, 
                             IMantimentoRepository MantimentoRepository,
                             IConfiguration configuration)
        {
            _logger = logger;
            _MantimentoRepository = MantimentoRepository;
            _configuration = configuration;
        }
        //Ja na index estamos preenchendo nossas viewbag para retornar o valor a tela, assim podemos utilizar esses valores para a logica do Javascript
        public  async Task<IActionResult> Index()
        {
            List<Mantimento> mantimentos = await _MantimentoRepository.ObterTDados(new Mantimento());
            ViewBag.qtd = mantimentos.Where(x=>x.Estoque > 0 ).ToList().Count();
            ViewBag.qtdc = mantimentos.Where(x=>x.Estoque <= 0 && x.TpMantimento.Obrigatorio).ToList().Count();

            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
        //No metodo  GetToken vamos precisar stanciar nossa classe criada e atravez do BuildToken executar a geração ta criptação.
        public ActionResult GetToken()
        {
            ConfigJWT configJWT = new ConfigJWT(_configuration);
            string token =  configJWT.BuildToken();

            return Content(token);
        }

        public ActionResult ValidToken(string token)
        {
            ConfigJWT configJWT = new ConfigJWT(_configuration);
            (string retorno, string error) = configJWT.ValidateToken(token);
            //!string.IsNullOrWhiteSpace(error)?retorno:error
            return Content(error);
        }
    }
}
