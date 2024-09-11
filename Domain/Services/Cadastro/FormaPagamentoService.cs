using Domain.Interfaces.Cadastro;
using Domain.Interfaces.IServices.Cadastro;
using Entities.Models.Cadastro;
using Entities.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Cadastro
{
    public class FormaPagamentoService : FormaPagamentoIService
    {
        private readonly FormaPagamentoInterface _formaPagamentoInterface;
        private readonly INotificador _notificador;

        public FormaPagamentoService(FormaPagamentoInterface formaPagamentoInterface, INotificador notificador)
        {
            _formaPagamentoInterface = formaPagamentoInterface;
            _notificador = notificador;
        }

        public FormaPagamento Get(int id)
        {
            return _formaPagamentoInterface.Get(id);
        }

        public IEnumerable<FormaPagamento> GetAll()
        {
            return _formaPagamentoInterface.GetAll();
        }

        public FormaPagamento Insert(FormaPagamento formaPagamento)
        {
            if (TestarFormaPagamento(formaPagamento, "I"))
                return _formaPagamentoInterface.Insert(formaPagamento);
            return formaPagamento;

        }

        public FormaPagamento Update(FormaPagamento formaPagamento)
        {
            if (TestarFormaPagamento(formaPagamento, "U"))
                return _formaPagamentoInterface.Update(formaPagamento);
            return formaPagamento;
        }

        public void Delete(FormaPagamento formaPagamento)
        {
            _formaPagamentoInterface.Delete(formaPagamento);
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

        private bool TestarFormaPagamento(FormaPagamento formaPagamento, string operacao)
        {
            try
            {
                if (formaPagamento.cadtbformapagamento_sigla == null)
                    Notificar("Sigla é obrigatório");
                if (formaPagamento.cadtbformapagamento_descricao == null)
                    Notificar("Descrição é obrigatório");
                if (formaPagamento.cadtbformapagamento_fkseqdoc == null || formaPagamento.cadtbformapagamento_fkseqdoc == 0)
                    Notificar("Tipo de Documento é obrigatório");
                if (formaPagamento.cadtbformapagamento_fpagamento < 0 || formaPagamento.cadtbformapagamento_fpagamento > 16)
                    Notificar("Forma de pagamento é obrigatório");

                var formaPagto = _formaPagamentoInterface.GetFormaPagamento(formaPagamento.cadtbformapagamento_sigla, formaPagamento.cadtbformapagamento_descricao);
                if (operacao == "I" && formaPagto != null)
                    Notificar("Forma de pagamento já cadastrada");
                
                if (operacao == "U")
                {
                    var formapagmto = _formaPagamentoInterface.Get(formaPagamento.cadtbformapagamento_pkseq);
                    if (formapagmto.cadtbformapagamento_pkseq != formaPagamento.cadtbformapagamento_pkseq && formapagmto.cadtbformapagamento_sigla == formaPagamento.cadtbformapagamento_sigla)
                        Notificar("Essa sigla já foi cadastrada");
                    if (formapagmto.cadtbformapagamento_pkseq != formaPagamento.cadtbformapagamento_pkseq && formapagmto.cadtbformapagamento_descricao == formaPagamento.cadtbformapagamento_descricao)
                        Notificar("Essa forma de pagamento já foi cadastrada");
                }
                return !TemNotificacao();
            }
            catch (Exception e)
            {
                Notificar("Erro ao inserir a Forma de Pagamento. " + e);
                return false;
            }
        }
    }
}
