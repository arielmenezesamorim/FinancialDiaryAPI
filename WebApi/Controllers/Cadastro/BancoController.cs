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
    public class BancoController : MainController
    {
        private readonly BancoInterface _bancoInterface;
        private readonly BancoIService _bancoIService;

        public BancoController(BancoInterface bancoInterface,
                               BancoIService bancoIService,
                               INotificador notificador) : base(notificador)
        {
            _bancoInterface = bancoInterface;
            _bancoIService = bancoIService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Banco>> GetAll([FromQuery] BancoFiltro filtro)
        {
            return Ok(_bancoInterface.Filtrar(filtro));
        }

        [HttpGet("{id}")]
        public ActionResult<Banco> Get(int id)
        {
            var banco = _bancoInterface.Get(id);
            if (banco != null)
                return Ok(banco);
            return NotFound();
        }

        [HttpPost]
        public ActionResult<Banco> Insert(Banco banco)
        {
            if (ModelState.IsValid)
            {
                _bancoIService.Insert(banco);
                return CustomResponse(banco, "Banco cadastrado com sucesso", HttpStatusCode.Created);
            }
            return ValidarModelBinding();
        }

        [HttpPut]
        public ActionResult<Banco> Update(Banco banco)
        {
            if (ModelState.IsValid)
            {
                _bancoIService.Update(banco);
                return CustomResponse(banco, "Banco alterado com sucesso", HttpStatusCode.OK);
            }
            return ValidarModelBinding();
        }

        [HttpDelete]
        public ActionResult<Banco> Delete(Banco banco)
        {
            _bancoIService.Delete(banco);
            return CustomResponse(banco, "Banco excluído com sucesso", HttpStatusCode.OK);
        }
    }
}
