using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScootersMc.Business.Models.Notificacoes
{
    public class Notificacao
    {
        public string _mensagem { get; }

        public Notificacao(string mensagem)
        {
            _mensagem = mensagem;
        }

        
    }
}
