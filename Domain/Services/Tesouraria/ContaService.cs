using Domain.Interfaces.Cadastro;
using Domain.Interfaces.IServices.Tesouraria;
using Domain.Interfaces.Tesouraria;
using Entities.Models.Cadastro;
using Entities.Models.Tesouraria;
using Entities.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Tesouraria
{
    public class ContaService : ContaIService
    {
        private readonly ContaInterface _contaInterface;
        private readonly INotificador _notificador;

        public ContaService(ContaInterface contaInterface, INotificador notificador)
        {
            _contaInterface = contaInterface;
            _notificador = notificador;
        }

        public Conta Get(int id)
        {
            return _contaInterface.Get(id);
        }

        public IEnumerable<Conta> GetAll()
        {
            return _contaInterface.GetAll();
        }

        public Conta Insert(Conta conta)
        {
            if (TestarConta(conta, "I"))
                return _contaInterface.Insert(conta);
            return conta;
        }

        public Conta Update(Conta conta)
        {
            if (TestarConta(conta, "U"))
                return _contaInterface.Update(conta);
            return conta;
        }

        public void Delete(Conta conta)
        {
            _contaInterface.Delete(conta);
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

        private bool TestarConta(Conta conta, string operacao)
        {
            try
            {
                var contas = _contaInterface.GetConta(conta.testbconta_nroconta);
                if (contas != null && operacao == "I")
                    Notificar("Essa conta já está cadastrada");

                return !TemNotificacao();
            }
            catch (Exception e)
            {
                Notificar("Erro ao inserir a conta. " + e);
                return false;
            }
        }
    }
}
