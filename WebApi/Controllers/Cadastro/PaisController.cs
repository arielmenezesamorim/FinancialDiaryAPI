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
    public class PaisController : MainController
    {
        private readonly PaisIService _paisIService;
        private readonly PaisInterface _paisInterface;

        public PaisController(PaisIService paisIService,
                              PaisInterface paisInterface,
                              INotificador notificador) : base(notificador)
        {
            _paisIService = paisIService;
            _paisInterface = paisInterface;
        }

        [HttpGet("{sigla}")]
        public ActionResult<Pais> Get(string sigla)
        {
            var pais = _paisInterface.Get(sigla);
            if (pais != null)
                return Ok(pais);
            return NotFound();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Pais>> GetAll([FromQuery] PaisFiltro filtro)
        {
            return Ok(_paisInterface.Filtrar(filtro));
        }

        [HttpPost]
        public ActionResult Insert(Pais pais)
        {
            if (ModelState.IsValid)
            {
                _paisIService.Insert(pais);
                return CustomResponse(pais, "País cadastrado com sucesso!", HttpStatusCode.Created);
            }
            return ValidarModelBinding();
        }

        [HttpPut]
        public ActionResult Update(Pais pais)
        {
            if (ModelState.IsValid)
            {
                _paisIService.Update(pais);
                return CustomResponse(pais, "País alterado com sucesso!", HttpStatusCode.OK);
            }
            return ValidarModelBinding();
        }

        [HttpDelete]
        public ActionResult Delete(Pais pais)
        {
            _paisIService.Delete(pais);
            return CustomResponse(pais, "País excluído com sucesso!", HttpStatusCode.OK);
        }
    }
}
