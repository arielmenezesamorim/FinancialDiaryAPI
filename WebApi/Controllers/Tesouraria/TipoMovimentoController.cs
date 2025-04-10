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
    public class TipoMovimentoController : MainController
    {
        private readonly TipoMovimentoInterface _tipoMovimentoInterface;
        private readonly TipoMovimentoIService _tipoMovimentoIService;

        public TipoMovimentoController(TipoMovimentoInterface tipoMovimentoInterface, 
                                       TipoMovimentoIService tipoMovimentoIService,
                                       INotificador notificador) : base(notificador)
        {
            _tipoMovimentoInterface = tipoMovimentoInterface;
            _tipoMovimentoIService = tipoMovimentoIService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TipoMovimento>> GetAll([FromQuery] TipoMovimentoFiltro filtro)
        {
            return Ok(_tipoMovimentoInterface.Filtrar(filtro));
        }

        [HttpGet("{id}")]
        public ActionResult<TipoMovimento> Get(int id)
        {
            var tipoMovimento = _tipoMovimentoInterface.Get(id);
            if (tipoMovimento != null)
                return Ok(tipoMovimento);
            return NotFound();
        }

        [HttpPost]
        public ActionResult<TipoMovimento> Insert(TipoMovimento tipoMovimento)
        {
            if (ModelState.IsValid)
            {
                _tipoMovimentoInterface.Insert(tipoMovimento);
                return CustomResponse(tipoMovimento, "Tipo de movimento cadastro com sucesso", HttpStatusCode.Created);
            }
            return ValidarModelBinding();
        }

        [HttpPut]
        public ActionResult<TipoMovimento> Update(TipoMovimento tipoMovimento)
        {
            if (ModelState.IsValid)
            {
                _tipoMovimentoInterface.Update(tipoMovimento);
                return CustomResponse(tipoMovimento, "Tipo de movimento alterado com sucesso", HttpStatusCode.OK);
            }
            return ValidarModelBinding();
        }

        [HttpDelete]
        public ActionResult<TipoMovimento> Delete(TipoMovimento tipoMovimento)
        {
            _tipoMovimentoInterface.Delete(tipoMovimento);
            return CustomResponse(tipoMovimento, "Tipo de movimento excluído com sucesso", HttpStatusCode.OK);
        }
    }
}
