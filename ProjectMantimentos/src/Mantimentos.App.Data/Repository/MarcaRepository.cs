using Mantimentos.App.Business.Models;
using Mantimentos.App.Business.Interfaces;
using System;
using Mantimentos.App.Data.Context;

namespace Mantimentos.App.Data.Repository
{
    /// <summary>
    /// Repository  MarcaRepository não possui implementações alem das herdadas de Repository 
    /// </summary>
    public class MarcaRepository : Repository<Marca>, IMarcaRepository
    {
        public MarcaRepository(MantimentoDbContext context) : base(context) { }
    }
}
