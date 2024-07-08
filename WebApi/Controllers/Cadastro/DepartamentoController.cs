using Domain.Interfaces.Cadastro;
using Domain.Interfaces.IServices.Cadastro;
using Entities.Models.Cadastro;
using Entities.Models.Filtro.Cadastro;
using Entities.Notification;
using Infra.Repositories.Cadastro;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using WebApi.Controllers.Generic;

namespace WebApi.Controllers.Cadastro
{
    [Route("[controller]")]
    [ApiController]
    public class DepartamentoController : MainController
    {
        private readonly DepartamentoInterface _departamentoInterface;
        private readonly DepartamenteIService _departamenteIService;

        public DepartamentoController(DepartamentoInterface departamentoInterface, 
                                      DepartamenteIService departamenteIService, 
                                      INotificador notificador) : base(notificador)
        {
            _departamentoInterface = departamentoInterface;
            _departamenteIService  = departamenteIService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Departamento>> GetAll([FromQuery] DepartamentoFiltro filtro)
        {
            return Ok(_departamentoInterface.Filtrar(filtro));
        }

        [HttpGet("{id}")]
        public ActionResult<Departamento> Get(int id)
        {
            var departamento = _departamentoInterface.Get(id);
            if (departamento != null)
                return Ok(departamento);
            return NotFound();
        }

        [HttpPost]
        public ActionResult<Departamento> Insert(Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                _departamenteIService.Insert(departamento);
                return CustomResponse(departamento, "Departamento cadastrado com sucesso!", HttpStatusCode.Created);
            }
            return ValidarModelBinding();
        }

        [HttpPut]
        public ActionResult<Departamento> Update(Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                _departamenteIService.Update(departamento);
                return CustomResponse(departamento, "Departamento alterada com sucesso!", HttpStatusCode.OK);
            }
            return ValidarModelBinding();
        }

        [HttpDelete]
        public ActionResult<Departamento> Delete(Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                _departamenteIService.Delete(departamento);
                return CustomResponse(departamento, "Departamento excluído com sucesso!", HttpStatusCode.OK);
            }
            return ValidarModelBinding();
        }
    }
}
