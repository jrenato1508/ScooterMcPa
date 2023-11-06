using ScootersMc.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScootersMc.Business.Models.Notificacoes
{
    public class Notificador : INotificador
    {
        private List<Notificacao> _notificadorList;


        public Notificador()
        {
            _notificadorList = new List<Notificacao>();
        }


        public void Handle(Notificacao notificacao)
        {
            _notificadorList.Add(notificacao);
        }

        public List<Notificacao> ObterNotificacoes()
        {
            return _notificadorList;
        }

        public bool TemNotificacao()
        {
            throw new NotImplementedException();
        }
    }
}
