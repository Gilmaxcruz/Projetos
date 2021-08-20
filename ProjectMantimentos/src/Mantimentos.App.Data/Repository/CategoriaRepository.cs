using Mantimentos.App.Business.Models;
using Mantimentos.App.Business.Interfaces;
using System;
using Mantimentos.App.Data.Context;

namespace Mantimentos.App.Data.Repository
{
    /// <summary>
    /// Repository  CategoriaRepository não possui implementações alem das herdadas de Repository 
    /// </summary>
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(MantimentoDbContext context) : base(context) { }
    }
}
