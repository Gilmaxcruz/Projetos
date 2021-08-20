using Microsoft.EntityFrameworkCore;
using StartFull.Business.Interface;
using StartFull.Business.Models;
using StartFull.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartFull.Data.Repository
{
   public  class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(MeuDbContext context) : base(context) { }
        
        public async Task<Produto> ObterProdutosFornecedor(Guid id)
        {
            //faz o inner join com a tabela e retorna
            return await Db.Produtos.AsNoTracking().Include(f => f.Fornecedor)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Produto>> ObterProdutosFornecedores()
        {
            //faz o inner join mas dessa vez ordena por nome e tras todos.
            return await Db.Produtos.AsNoTracking().Include(f => f.Fornecedor)
                .OrderBy(p => p.Nome).ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId)
        {
            //utilizado o metodo buscar generico vindo de Repository
            return await Buscar(p => p.FornecedorId == fornecedorId);
        }
    }
}
