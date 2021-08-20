using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.UI.Site.Models
{
    public class Aluno
    {
        
        public Aluno ()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id{ get; set; }

       
        public string nome{ get; set; }
        public string email{ get; set; }
        public DateTime DataNascimento{ get; set; }

    }
}
