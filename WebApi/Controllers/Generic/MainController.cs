using Entities.Notification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace WebApi.Controllers.Generic
{
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly INotificador _notificador;

        public MainController(INotificador notificador)
        {
            this._notificador = notificador;
        }

        protected void Notificar(string mensagem)
        {
            this._notificador.Adicionar(new Notificacao(mensagem));
        }

        protected List<Notificacao> Notificacoes()
        {
            return this._notificador.ObterNotificacoes();
        }

        protected bool TemNotificacoes()
        {
            return this._notificador.TemNotificacao();
        }

        protected void ValidarModelState()
        {
            if (ModelState.IsValid)
                return;
            var erros = ModelState.Values.SelectMany(e => e.Errors);
            foreach (var error in erros)
            {
                var mensagem = "";
                if (error.Exception == null)
                    mensagem = error.ErrorMessage;
                else
                    mensagem = error.Exception.Message;

                Notificar(mensagem);
            }
        }

        protected ActionResult ValidarModelBinding()
        {
            ValidarModelState();
            return CustomResponse(null, null, HttpStatusCode.BadRequest);
        }

        protected ActionResult CustomResponse(object entidade = null, string mensagem = null, HttpStatusCode statusCode = 0)
        {

            if (!this._notificador.TemNotificacao())
            {
                object response = new
                {
                    status = statusCode,
                    sucesso = true,
                    entidade = entidade,
                    mensagem = mensagem
                };

                if (statusCode == HttpStatusCode.Created)//201
                {
                    return Created("", response);
                }

                return Ok(response);
            }
            else
            {
                return BadRequest(
                    new
                    {
                        status = HttpStatusCode.BadRequest,
                        sucesso = false,
                        errors = Notificacoes()
                    });
            }
        }
    }
}
