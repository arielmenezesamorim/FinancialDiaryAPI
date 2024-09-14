using Domain.Interfaces.IServices.Tesouraria;
using Domain.Interfaces.Tesouraria;
using Entities.Models.Filtro.Tesouraria;
using Entities.Models.Tesouraria;
using Entities.Notification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using WebApi.Controllers.Generic;

namespace WebApi.Controllers.Tesouraria
{
    [Route("[controller]")]
    [ApiController]
    public class ContaController : MainController
    {
        private readonly ContaInterface _contaInterface;
        private readonly ContaIService _contaIService;

        public ContaController(ContaInterface contaInterface,
                               ContaIService contaIService,
                               INotificador notificador) : base(notificador)
        {
            _contaInterface = contaInterface;
            _contaIService = contaIService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Conta>> GetAll([FromQuery] ContaFiltro filtro)
        {
            return Ok(_contaInterface.Filtrar(filtro));
        }

        [HttpGet("{id}")]
        public ActionResult<Conta> Get(int id)
        {
            var conta = _contaIService.Get(id);
            if (conta != null)
                return Ok(conta);
            return NotFound();
        }

        [HttpPost]
        public ActionResult<Conta> Insert(Conta conta)
        {
            if (ModelState.IsValid)
            {
                _contaIService.Insert(conta);
                return CustomResponse(conta, "Conta cadastrada com sucesso", HttpStatusCode.Created);
            }
            return ValidarModelBinding();
        }

        [HttpPut]
        public ActionResult<Conta> Update(Conta conta)
        {
            if (ModelState.IsValid)
            {
                _contaIService.Update(conta);
                return CustomResponse(conta, "Conta alterada com sucesso", HttpStatusCode.OK);
            }
            return ValidarModelBinding();
        }

        [HttpDelete]
        public ActionResult<Conta> Delete(Conta conta)
        {
            if (ModelState.IsValid)
            {
                _contaIService.Delete(conta);
                return CustomResponse(conta, "Conta excluída com sucesso", HttpStatusCode.OK);
            }
            return ValidarModelBinding();
        }
    }
}
