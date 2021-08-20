using Microsoft.EntityFrameworkCore;
using StartFull.Business.Interface;
using StartFull.Business.Models;
using StartFull.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StartFull.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity,new () //Na declaração ja definimos que recebe o filho Entity e o new é apenas para que possamos instanciar para que seja permitirdo fazer isso
    {
        // declaramos o contexto puxando a classe que conecta ao banco, assim todo aquele que herdar o Repository
        //tera acesso ao Dbset do MeuDbContext, caso seja necessario fazer um Try não precisa fazer um por um é so vir aqui.
        protected readonly MeuDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(MeuDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public virtual async Task Adicionar(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChages();
        }

        public virtual async Task Atualizar(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChages();
        }

        public virtual async Task Remover(Guid id)
        {
            var entity = new TEntity { Id = id };// por isso precisamos liberar no where o new para que seja possivel instancia durante a utilização.
            DbSet.Remove(entity);//assim com essa instancia ele compara os ID e consegue encontrar para fazer a remoção
            await SaveChages();
        }
        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            //se nao for gravar ou modificar utilizamos o AsNotracking para que se por acaso salvar algo não de problema e isso tras ganho de performace.
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> ObterPorId(Guid id)
        {
            return await DbSet.FindAsync(id);//Find Encontrar
        }

        public virtual async Task<List<TEntity>> ObterTodos()
        {
            return await DbSet.ToListAsync();//To list listar
        }

        

        public virtual async  Task<int> SaveChages()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db?.Dispose(); //Descartar significado
        }
    }
}
