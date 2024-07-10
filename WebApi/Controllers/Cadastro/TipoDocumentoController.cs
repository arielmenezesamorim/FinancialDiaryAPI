using Domain.Interfaces.Cadastro;
using Domain.Interfaces.IServices.Cadastro;
using Entities.Models.Cadastro;
using Entities.Models.Filtro.Cadastro;
using Entities.Notification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using WebApi.Controllers.Generic;

namespace WebApi.Controllers.Cadastro
{
    [Route("[controller]")]
    [ApiController]
    public class TipoDocumentoController : MainController
    {
        private readonly TipoDocumentoInterface _tipoDocumentoInterface;
        private readonly TipoDocumentoIService _tipoDocumentoIService;

        public TipoDocumentoController(TipoDocumentoInterface tipoDocumentoInterface, 
                                       TipoDocumentoIService tipoDocumentoIService,
                                       INotificador notificador) : base(notificador)
        {            
            _tipoDocumentoInterface = tipoDocumentoInterface;
            _tipoDocumentoIService = tipoDocumentoIService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TipoDocumento>> GetAll([FromQuery] TipoDocumentoFiltro filtro)
        {
            return Ok(_tipoDocumentoInterface.Filtrar(filtro));
        }

        [HttpGet("{id}")]
        public ActionResult<TipoDocumento> Get(int id)
        {
            var tipoDocumento = _tipoDocumentoInterface.Get(id);
            if (tipoDocumento != null)
                return Ok(tipoDocumento);
            return NotFound();
        }

        [HttpPost]
        public ActionResult<TipoDocumento> Insert(TipoDocumento tipoDocumento)
        {
            if (ModelState.IsValid)
            {
                _tipoDocumentoIService.Insert(tipoDocumento);
                return CustomResponse(tipoDocumento, "Tipo de documento cadastro com sucesso!", HttpStatusCode.Created);
            }
            return ValidarModelBinding();
        }

        [HttpPut]
        public ActionResult<TipoDocumento> Update(TipoDocumento tipoDocumento)
        {
            if (ModelState.IsValid)
            {
                _tipoDocumentoIService.Update(tipoDocumento);
                return CustomResponse(tipoDocumento, "Tipo de documento alterado com sucesso!", HttpStatusCode.OK);
            }
            return ValidarModelBinding();
        }

        [HttpDelete]
        public ActionResult<TipoDocumento> Delete(TipoDocumento tipoDocumento)
        {
            if (ModelState.IsValid)
            {
                _tipoDocumentoIService.Delete(tipoDocumento);
                return CustomResponse(tipoDocumento, "Tipo de documento excluído com sucesso!", HttpStatusCode.OK);
            }
            return ValidarModelBinding();
        }
    }
}
