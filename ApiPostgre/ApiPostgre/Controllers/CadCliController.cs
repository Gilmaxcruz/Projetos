using ApiPostgre.IService;
using ApiPostgre.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPostgre.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CadCliController : ControllerBase
    {
        private ICadCliService _service;
        public CadCliController(ICadCliService service)
        {
            _service = service;
        }
        [HttpGet] //GET Fazer leitura
        public async Task<IActionResult> GetCadCli()
        {
            List<CadCli> lista = _service.GetCadClis();
            return Ok(lista);
        }

        [HttpPost] //POST Fazer inserção
        public async Task<IActionResult> PostCadCli([FromBody] CadCli cadcli)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _service.PostCadCli(cadcli);
            return Ok();
        }
        [HttpDelete("{cliente}")]
        public IActionResult Deletecadcli(int cliente)
        {
            _service.Deletecadcli(cliente);
            return Ok();
        }
        [HttpPut("{cliente}")]

        public async Task<IActionResult> PutCadCli([FromBody] CadCli cadcli, [FromRoute] int cliente)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            cadcli.cliente = cliente;
            _service.PutCadCli(cadcli);
            return Ok();
        }

    }

}
