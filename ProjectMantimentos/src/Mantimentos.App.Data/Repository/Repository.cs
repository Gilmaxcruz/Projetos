using Mantimentos.App.Business.Interfaces;
using Mantimentos.App.Business.Models;
using Mantimentos.App.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mantimentos.App.Data.Repository
{
    /// <summary>
    /// Repository generico onde contem todos metodos necessarios 
    /// </summary>
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly MantimentoDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(MantimentoDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        //Criado virtual para que caso eu precise fazer a sobrecarga no metodo em outro Repository seja possivel.
        //metodo utilizado para buscar lista da entidade e possibilitando passar operação Lambda!
        public virtual async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> ObterPorId(Guid id)
        {
            return await  DbSet.FindAsync(id);
        }

        public virtual async Task<List<TEntity>> ObterTodos()
        {
            return await DbSet.ToListAsync();
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
            //Criado a instancia para que não seja necessario ir ao banco, assim conseguimos instanciar sem pegar todo o corpo.
            DbSet.Remove(new TEntity { Id = id });
            await SaveChages();
        }

        public async Task<int> SaveChages()
        {
            // Caso necessario fazer algum tratamento Try cath dessa forma consigo minimizar toda essa tarefa em apenas um metodo.
            return await Db.SaveChangesAsync();
        }
        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}
