using ApiPostgre.IRepository;
using ApiPostgre.IService;
using ApiPostgre.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPostgre.Service
{
    public class CadCliService : ICadCliService
    {
        private ICadCliRepository _repository;
        public CadCliService(ICadCliRepository repository)
        {
            _repository = repository;
        }
        public List<CadCli> GetCadClis()
        {
            List<CadCli> lista = _repository.GetCadClis();
            return lista;
        }

        public void PostCadCli(CadCli cadcli)
        {
            cadcli.recnum = _repository.GetUltRecnum() + 1;
            cadcli.cliente = _repository.GetUltCli() + 1;
            cadcli.nome = cadcli.nome.ToUpper();
            _repository.PostCadCli(cadcli);
        }

        public void Deletecadcli(int cliente)
        {
            CadCli cadcli = _repository.GetCliID(cliente);
            _repository.Deletecadcli(cadcli);
                
        }

        public void PutCadCli(CadCli cadcli)
        {
            CadCli cadcli2 = _repository.GetCliID(cadcli.cliente);
            cadcli2.nome = cadcli.nome;
            cadcli2.cpf = cadcli.cpf;
            _repository.UpdateCli(cadcli2);

        }
    }
}
