using Microsoft.AspNetCore.Mvc;
using ScootersMc.Business.Interfaces;

namespace ScootersMc.App.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly INotificador _notificador;
        public BaseController(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }
    }
}
