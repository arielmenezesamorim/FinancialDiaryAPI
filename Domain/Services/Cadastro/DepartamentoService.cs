using Domain.Interfaces.Cadastro;
using Domain.Interfaces.IServices.Cadastro;
using Entities.Models.Cadastro;
using Entities.Models.Filtro.Cadastro;
using Entities.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Cadastro
{
    public class DepartamentoService : DepartamenteIService
    {
        private readonly DepartamentoInterface _departamentoInterface;
        private readonly INotificador _notificador;

        public DepartamentoService(DepartamentoInterface departamentoInterface,
                                   INotificador notificador)
        {
            _departamentoInterface = departamentoInterface;
            _notificador = notificador;
        }

        public Departamento Get(int id)
        {
            return _departamentoInterface.Get(id);
        }

        public IEnumerable<Departamento> GetAll()
        {
            return _departamentoInterface.GetAll();
        }

        public Departamento Insert(Departamento departamento)
        {
            if (TestarDepartamento(departamento, "I"))
                return _departamentoInterface.Insert(departamento);
            return departamento;
        }

        public Departamento Update(Departamento departamento)
        {
            if (TestarDepartamento(departamento, "U"))
                return _departamentoInterface.Insert(departamento);
            return departamento;
        }

        public void Delete(Departamento departamento)
        {
            _departamentoInterface.Delete(departamento);
        }

        protected bool TemNotificacao()
        {
            return this._notificador.TemNotificacao();
        }

        protected void Notificar(string mensagem)
        {
            this._notificador.Adicionar(new Notificacao(mensagem));
        }

        protected void LimparNotificacoes()
        {
            this._notificador.LimparNotificacoes();
        }

        protected List<Notificacao> Notificacoes()
        {
            return this._notificador.ObterNotificacoes();
        }

        protected void AdicionarNotificacoes(List<string> mensagens)
        {
            foreach (var mensagem in mensagens)
                Notificar(mensagem);
        }

        private bool TestarDepartamento(Departamento departamento, string operacao)
        {
            try
            {
                if (operacao.Equals("I"))
                {
                    var departamentoSigla = _departamentoInterface.Get(departamento.cadtbdepartamento_sigla);

                    if (departamentoSigla != null)
                        Notificar("Já existe um departamento cadastrado com essa sigla.");
                }

                return !TemNotificacao();
            }
            catch (Exception e)
            {
                Notificar("Erro ao testar departamento. " + e);
                return false;
            }
        }
    }
}
