using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScootersMc.Business.Models
{
    public class ContatoEmergencia : Entity
    {
        public Guid MembroMcId { get; set; }
        public string Nome { get; set; }

        public string Telefone { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime DataAlteracao { get; set; }


        /*relacionamento do EntityFramework*/
        public MembroMc MembroMc { get; set; }
    }
}
