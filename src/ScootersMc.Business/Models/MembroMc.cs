using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScootersMc.Business.Models
{
    public class MembroMc : Entity
    {
        public string Imagem { get; set; }

        public string Nome { get; set; }

        public string Cpf { get; set; }

        public string Email { get; set; }

        public Hierarquia Hierarquia { get; set; }

        public TipoSanguineo TipoSanguineo { get; set; }

        public DateTime DataNascimento { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime DataAlteracao { get; set; }

        public IEnumerable<ContatoEmergencia> ContatoEmergencia { get; set; }

        public int Idade { get; set; }

        public string Telefone { get; set; }

        public bool Ativo { get; set; }

        public Endereco Endereco { get; set; }
    }
}
