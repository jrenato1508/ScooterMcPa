using ScootersMc.Business.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ScootersMc.App.ViewModels
{
    public class MembroMcViewModel
    {
        [Key]
        public Guid Id { get; set; }

        
        public string Imagem { get; set; }

        [DisplayName("Imagem do Produto")]
        public IFormFile ImagemUpload { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(14, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 11)]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "Enderco de email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public HierarquiaViewModel Hierarquia { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Tipo Sanguineo")]
        public TipoSanguineoViewModel TipoSanguineo { get; set; }

        [DisplayName("Data de Nascimento")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime DataNascimento { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataAlteracao { get; set; }

        public int Idade { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Phone(ErrorMessage = "Numero de telefone inválido")]
        public string Telefone { get; set; }

        [DisplayName("Ativo?")]
        public bool Ativo { get; set; }

        /* Entity FrameWorks Relations */
        public ContatoEmergenciaViewModel ContatoEmergencia { get; set; }

        public EnderecoViewModel Endereco { get; set; }
    }
}
