using ApiPostgre.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPostgre.IService
{
   public interface ICadCliService
    {
        List<CadCli> GetCadClis();
        void PostCadCli(CadCli cadcli);
        void  Deletecadcli(int  cliente);

        void PutCadCli(CadCli cadcli);

    }

}
