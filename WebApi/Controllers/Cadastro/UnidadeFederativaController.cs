using Domain.Interfaces.Cadastro;
using Domain.Interfaces.IServices.Cadastro;
using Domain.Services.Cadastro;
using Entities.Models.Cadastro;
using Entities.Models.Filtro.Cadastro;
using Entities.Notification;
using Infra.Repositories.Cadastro;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using WebApi.Controllers.Generic;

namespace WebApi.Controllers.Cadastro
{
    [Route("[controller]")]
    [ApiController]
    public class UnidadeFederativaController : MainController
    {
        private readonly UnidadeFederativaInterface _unidadeFederativaInterface;
        private readonly UnidadeFederativaIService _unidadeFederativaIService;

        public UnidadeFederativaController(UnidadeFederativaInterface unidadeFederativaInterface,
                                           UnidadeFederativaIService unidadeFederativaIService,
                                           INotificador notificador) : base(notificador)
        {
            _unidadeFederativaInterface = unidadeFederativaInterface;
            _unidadeFederativaIService  = unidadeFederativaIService;
        }

        [HttpGet("{sigla}")]
        public ActionResult<UnidadeFederativa> Get(string sigla)
        {
            var unidadeFederativa = _unidadeFederativaInterface.Get(sigla);
            if (unidadeFederativa != null)
                return Ok(unidadeFederativa);
            return NotFound();
        }

        [HttpGet]
        public ActionResult<IEnumerable<UnidadeFederativa>> GetAll([FromQuery] UnidadeFederativaFiltro filtro)
        {
            return Ok(_unidadeFederativaInterface.Filtrar(filtro));
        }

        [HttpPost]
        public ActionResult Insert(UnidadeFederativa unidadeFederativa)
        {
            if (ModelState.IsValid)
            {
                _unidadeFederativaIService.Insert(unidadeFederativa);
                return CustomResponse(unidadeFederativa, "Unidade Federativa cadastrado com sucesso!", HttpStatusCode.Created);
            }
            return ValidarModelBinding();
        }

        [HttpPut]
        public ActionResult Update(UnidadeFederativa unidadeFederativa)
        {
            if (ModelState.IsValid)
            {
                _unidadeFederativaIService.Update(unidadeFederativa);
                return CustomResponse(unidadeFederativa, "Unidade Federativa alterado com sucesso!", HttpStatusCode.OK);
            }
            return ValidarModelBinding();
        }

        [HttpDelete]
        public ActionResult Delete(UnidadeFederativa unidadeFederativa)
        {
            _unidadeFederativaIService.Delete(unidadeFederativa);
            return CustomResponse(unidadeFederativa, "Unidade Federativa excluído com sucesso!", HttpStatusCode.OK);
        }
    }
}
