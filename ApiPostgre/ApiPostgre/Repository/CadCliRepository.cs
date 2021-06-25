using ApiPostgre.Data;
using ApiPostgre.IRepository;
using ApiPostgre.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPostgre.Repository
{
    public class CadCliRepository : ICadCliRepository
    {
        private readonly DataDbContext _DB;
        public CadCliRepository(DataDbContext DB)
        {
            _DB = DB;
        }

        public List<CadCli> GetCadClis()
        {
            List<CadCli> lista = _DB.CadClis.ToList();
            return lista;

        }

        public void PostCadCli(CadCli cadcli)
        {
            _DB.CadClis.Add(cadcli);
            _DB.SaveChanges();
        }


        public void Deletecadcli(CadCli cliente)
        {
            _DB.CadClis.Remove(cliente);
            _DB.SaveChanges();
        }

        public int GetUltCli()
        {
            int? id = _DB.CadClis.OrderByDescending(x => x.cliente).Take(1).FirstOrDefault()?.cliente;
            return (int)(id!= null ? id :0);
        }

        public int GetUltRecnum()
        {
            int? idu = _DB.CadClis.OrderByDescending(x => x.recnum).Take(1).FirstOrDefault()?.recnum;
            return (int)(idu != null ? idu : 0);
        }

        public CadCli GetCliID(int cliente)
        {
            CadCli cadcli = _DB.CadClis.Where(x => x.cliente == cliente).FirstOrDefault();
            return cadcli;
        }

        public void UpdateCli(CadCli cliente)
        {
            _DB.Entry(cliente).State= EntityState.Modified; 
            _DB.SaveChanges();
        }
    }
}
