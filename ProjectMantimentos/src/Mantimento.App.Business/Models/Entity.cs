using System;

namespace Mantimentos.App.Business.Models
{
    /// <summary>
    /// Classe generica que será responsavel por unicamente gerar o Guid para todas as classes que herdarem da mesma.
    /// </summary>
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
    }
}
