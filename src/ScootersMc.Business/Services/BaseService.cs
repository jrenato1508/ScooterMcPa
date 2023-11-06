using FluentValidation;
using FluentValidation.Results;
using ScootersMc.Business.Interfaces;
using ScootersMc.Business.Models;
using ScootersMc.Business.Models.Notificacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScootersMc.Business.Services
{
    public abstract class BaseService
    {
        private readonly INotificador _notificador;
        public BaseService(INotificador notificador)
        {
            _notificador = notificador;
        }
        #region  OBS ExecutarValidacao
        /*
          ExecutarValidacao é um metodo generico  onde ele irá receber dois parametos TV e TE e validar nossas entidades e retonar booleano.
          TV => É representa um objeto generico de Validação(ex: FornecedorValidation), por isso o V(validation). Que poderá ser qualquer
          uma de nossas classes que foram criadas para validar nossas entidades, eles estão dentro de Models(DevIO.Bussines) => Validations
          TE => É representa um objeto generico de Entidade(ex:Fornecedor)
          where TV : AbstractValidator<TE> Onde TV é do tipo AbstractValidator<TE>(de TE)
          where TE : Entity =>  Onde TE ele tem que ser do tipo entity

         */
        #endregion
        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            #region OBS
            /*
             Caso o validator for false, ele irá retornar uma um objeto que contem uma lista contendo o(s) erro(s) encontrado(s). Chamamos
             o metodo Notificar e passamos o validator como parametro
             */
            #endregion
            Notificar(validator);

            return false;
        }


        #region  OBS Notificar
        /*
           O Notificar recebe um objeto contendo uma lista com os erros encontrados, nesse metodo nós iremos varrer essa lista com o
           foreach e para cada mensagem de  erro encontrado nós iremos chamar um outro metodo notificar para e passar a mensagem de erro.
         */
        #endregion
        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {  /*Vai chamar esse metodo passando uma mensagem por vez da lista que ele receber.*/
                Notificar(error.ErrorMessage);
            }
        }

        #region  OBS Notificar
        /*
          Para cada mensagem de erro recebida nesse metodo Notificar, nós iremos chamar o metodo Handle da class notificador e passar a mensagem
          de erro exibida para que seja adicionada em uma lista para ser propagada para a camada de apresentação
         */
        #endregion
        protected void Notificar(string mensagem)
        { // Propagar esse erro até a cama de apresentação
            _notificador.Handle(new Notificacao(mensagem));
        }
    }

    

}
