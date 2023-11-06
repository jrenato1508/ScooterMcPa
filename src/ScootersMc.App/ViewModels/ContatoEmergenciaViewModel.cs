using Microsoft.AspNetCore.Mvc;
using ScootersMc.Business.Models;
using System.ComponentModel.DataAnnotations;

namespace ScootersMc.App.ViewModels
{
    public class ContatoEmergenciaViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Phone(ErrorMessage = "Numero de telefone informado é invalido")]
        public string Telefone { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataAlteracao { get; set; }


        /*relacionamento do EntityFramework*/
        [HiddenInput]
        public Guid MembroMcId { get; set; }

        public MembroMcViewModel Membro { get; set; }
        public IEnumerable<MembroMcViewModel> Membros { get; set; }

    }
}
