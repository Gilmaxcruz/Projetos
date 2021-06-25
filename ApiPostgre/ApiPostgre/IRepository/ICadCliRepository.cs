using ApiPostgre.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPostgre.IRepository
{
   public interface ICadCliRepository
    {
        List<CadCli> GetCadClis();
        void PostCadCli(CadCli cadcli);
        void Deletecadcli(CadCli cliente);

        int GetUltCli();
        int GetUltRecnum();
        CadCli GetCliID(int cliente);

        void UpdateCli(CadCli cliente);

    }
}
