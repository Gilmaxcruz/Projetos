using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Controllers
{
    public class Teste : Controller
    {
        private readonly ILogger<Teste> _logger;
        public Teste(ILogger<Teste> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            _logger.LogError("Esse erroo ocorreu!");
            return View();
        }
    }
}
