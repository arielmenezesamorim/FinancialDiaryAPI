using Domain.Interfaces.IServices.Tesouraria;
using Domain.Interfaces.Tesouraria;
using Entities.Models.Tesouraria;
using Entities.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Tesouraria
{
    public class TipoMovimentoService : TipoMovimentoIService
    {
        private readonly TipoMovimentoInterface _tipoMovimentoInterface;
        private readonly INotificador _notificador;

        public TipoMovimentoService(TipoMovimentoInterface tipoMovimentoInterface, INotificador notificador)
        {
            _tipoMovimentoInterface = tipoMovimentoInterface;
            _notificador = notificador;
        }

        public TipoMovimento Get(int id)
        {
            return _tipoMovimentoInterface.Get(id);
        }

        public IEnumerable<TipoMovimento> GetAll()
        {
            return _tipoMovimentoInterface.GetAll();
        }

        public TipoMovimento Insert(TipoMovimento TipoMovimento)
        {
            if (TestarTipoMovimento(TipoMovimento))
                return _tipoMovimentoInterface.Insert(TipoMovimento);
            return TipoMovimento;
        }

        public TipoMovimento Update(TipoMovimento TipoMovimento)
        {
            if (TestarTipoMovimento(TipoMovimento))
                return _tipoMovimentoInterface.Update(TipoMovimento);
            return TipoMovimento;
        }

        public void Delete(TipoMovimento TipoMovimento)
        {
            _tipoMovimentoInterface.Delete(TipoMovimento);
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

        private bool TestarTipoMovimento(TipoMovimento tipoMovimento)
        {
            try
            {
                if (tipoMovimento.cxatbtmv_tipo < 0)
                    Notificar("Tipo deve ser informado");
                if (tipoMovimento.cxatbtmv_descricao == null)
                    Notificar("Descrição é obrigatória");
                if (tipoMovimento.cxatbtmv_indicador < 0)
                    Notificar("Indicador deve ser informado");

                return !TemNotificacao();
            }
            catch (Exception e)
            {
                Notificar("Erro salvar tipo de movimento. " + e);
                return false;
            }
        }
    }
}
