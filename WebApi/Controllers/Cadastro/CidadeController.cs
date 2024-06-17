using Domain.Interfaces.Cadastro;
using Domain.Interfaces.IServices.Cadastro;
using Entities.Models.Cadastro;
using Entities.Models.Filtro.Cadastro;
using Entities.Notification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using WebApi.Controllers.Generic;

namespace WebApi.Controllers.Cadastro
{
    [Route("[controller]")]
    [ApiController]
    public class CidadeController : MainController
    {
        private readonly CidadeInterface _cidadeInterface;
        private readonly CidadeIService _cidadeIService;

        public CidadeController(CidadeInterface cidadeInterface, 
                                CidadeIService cidadeIService,
                                INotificador notificador) : base(notificador)
        {
            _cidadeInterface = cidadeInterface;
            _cidadeIService = cidadeIService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Cidade>> GetAll([FromQuery] CidadeFiltro filtro)
        {
            return Ok(_cidadeInterface.Filtrar(filtro));
        }

        [HttpGet("{id}")]
        public ActionResult<Cidade> Get(int id)
        {
            var cidade = _cidadeInterface.GetCidade(id);
            if (cidade != null)
                return Ok(cidade);

            return NotFound();
        }

        [HttpGet]
        [Route("GetAllPorUf/{sigla}")]
        public ActionResult<IEnumerable<Cidade>> GetAllBySigla(string sigla)
        {
            var cidades = _cidadeInterface.GetAllPorUf(sigla);
            return Ok(cidades);
        }

        [HttpPost]
        public ActionResult<Cidade> Insert(Cidade cidade)
        {
            if (ModelState.IsValid)
            {
                _cidadeIService.Insert(cidade);
                return CustomResponse(cidade, "Cidade cadastrada com sucesso!", HttpStatusCode.Created);
            }

            return ValidarModelBinding();
        }

        [HttpPut]
        public ActionResult<Cidade> Update(Cidade cidade)
        {
            if (ModelState.IsValid)
            {
                _cidadeIService.Update(cidade);
                return CustomResponse(cidade, "Cidade alterada com sucesso!", HttpStatusCode.OK);
            }

            return ValidarModelBinding();
        }

        [HttpDelete]
        public ActionResult<Cidade> Delete(Cidade cidade)
        {
            _cidadeIService.Delete(cidade);
            return CustomResponse(cidade, "Cidade excluída com sucesso!", HttpStatusCode.OK);
        }
    }
}
