using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPostgre.Models // TAMBEM PODERIA ESTAR ENTIDADE NO LUGAR DE MODELS
{
    
    [Keyless]
    public class CadCli
    {
        //  [Key]
        public int recnum { get; set; }
        public int cliente { get; set; }
        public string nome { get; set; }
        public string cpf { get; set; }
    }
}
