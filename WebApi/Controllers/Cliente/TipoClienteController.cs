using Domain.Interfaces.Cliente;
using Domain.Interfaces.IServices.Cliente;
using Domain.Interfaces.Tesouraria;
using Entities.Models.Cliente;
using Entities.Models.Filtro.Cliente;
using Entities.Models.Filtro.Tesouraria;
using Entities.Models.Tesouraria;
using Entities.Notification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using WebApi.Controllers.Generic;

namespace WebApi.Controllers.Cliente
{
    [Route("[controller]")]
    [ApiController]
    public class TipoClienteController : MainController
    {
        private readonly TipoClienteInterface _tipoClienteInterface;
        private readonly TipoClienteIService _tipoClienteIService;

        public TipoClienteController(TipoClienteInterface tipoClienteInterface, 
                                     TipoClienteIService tipoClienteIService,
                                     INotificador notificador) : base(notificador)
        {
            _tipoClienteInterface = tipoClienteInterface;
            _tipoClienteIService = tipoClienteIService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TipoCliente>> GetAll([FromQuery] TipoClienteFiltro filtro)
        {
            return Ok(_tipoClienteInterface.Filtrar(filtro));
        }

        [HttpGet("{id}")]
        public ActionResult<TipoCliente> Get(int id)
        {
            var TipoCliente = _tipoClienteInterface.Get(id);
            if (TipoCliente != null)
                return Ok(TipoCliente);
            return NotFound();
        }

        [HttpPost]
        public ActionResult<TipoCliente> Insert(TipoCliente TipoCliente)
        {
            if (ModelState.IsValid)
            {
                _tipoClienteInterface.Insert(TipoCliente);
                return CustomResponse(TipoCliente, "Tipo de movimento cadastro com sucesso", HttpStatusCode.Created);
            }
            return ValidarModelBinding();
        }

        [HttpPut]
        public ActionResult<TipoCliente> Update(TipoCliente TipoCliente)
        {
            if (ModelState.IsValid)
            {
                _tipoClienteInterface.Update(TipoCliente);
                return CustomResponse(TipoCliente, "Tipo de movimento alterado com sucesso", HttpStatusCode.OK);
            }
            return ValidarModelBinding();
        }

        [HttpDelete]
        public ActionResult<TipoCliente> Delete(TipoCliente TipoCliente)
        {
            _tipoClienteInterface.Delete(TipoCliente);
            return CustomResponse(TipoCliente, "Tipo de movimento excluído com sucesso", HttpStatusCode.OK);
        }
    }
}
