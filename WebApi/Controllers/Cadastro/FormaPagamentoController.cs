using Domain.Interfaces.Cadastro;
using Domain.Interfaces.IServices.Cadastro;
using Entities.Models.Cadastro;
using Entities.Models.Filtro.Cadastro;
using Entities.Notification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using WebApi.Controllers.Generic;

namespace WebApi.Controllers.Cadastro
{
    [Route("[controller]")]
    [ApiController]
    public class FormaPagamentoController : MainController
    {
        private readonly FormaPagamentoInterface _formaPagamentoInterface;
        private readonly FormaPagamentoIService _formaPagamentoIService;

        public FormaPagamentoController(FormaPagamentoInterface formaPagamentoInterface,
                                        FormaPagamentoIService formaPagamentoIService,
                                        INotificador notificador) : base(notificador)
        {
            _formaPagamentoInterface = formaPagamentoInterface;
            _formaPagamentoIService = formaPagamentoIService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<FormaPagamento>> GetAll([FromQuery] FormaPagamentoFiltro filtro)
        {
            return Ok(_formaPagamentoInterface.Filtrar(filtro));
        }

        [HttpGet("{id}")]
        public ActionResult<FormaPagamento> Get(int id)
        {
            var formaPagamento = _formaPagamentoInterface.Get(id);
            if (formaPagamento != null)
                return Ok(formaPagamento);
            return NotFound();
        }

        [HttpPost]
        public ActionResult<FormaPagamento> Insert(FormaPagamento formaPagamento)
        {
            if (ModelState.IsValid)
            {
                _formaPagamentoIService.Insert(formaPagamento);
                return CustomResponse(formaPagamento, "Forma de pagamento cadastra com sucesso", HttpStatusCode.Created);
            }
            return ValidarModelBinding();
        }

        [HttpPut]
        public ActionResult<FormaPagamento> Update(FormaPagamento formaPagamento)
        {
            if (ModelState.IsValid)
            {
                _formaPagamentoIService.Update(formaPagamento);
                return CustomResponse(formaPagamento, "Forma de pagamento alterada com sucesso", HttpStatusCode.OK);
            }
            return ValidarModelBinding();
        }

        [HttpDelete]
        public ActionResult<FormaPagamento> Delete(FormaPagamento formaPagamento)
        {
            _formaPagamentoIService.Delete(formaPagamento);
            return CustomResponse(formaPagamento, "Forma de pagamento excluída com sucesso", HttpStatusCode.OK);
        }
    }
}
