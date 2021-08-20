using Mantimentos.App.Business.Models;
using Mantimentos.App.Business.Interfaces;
using System;
using Mantimentos.App.Data.Context;

namespace Mantimentos.App.Data.Repository
{
    /// <summary>
    /// Repository  MovimentoRepository não possui implementações alem das herdadas de Repository 
    /// </summary>
    public class MovimentoRepository : Repository<Movimento>, IMovimentoRepository
    {
        public MovimentoRepository(MantimentoDbContext context) : base(context) { }

       
    }
}
