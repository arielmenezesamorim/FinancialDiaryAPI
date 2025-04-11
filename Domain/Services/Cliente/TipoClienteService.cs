using Domain.Interfaces.Cliente;
using Domain.Interfaces.IServices.Cliente;
using Entities.Models.Cliente;
using Entities.Models.Tesouraria;
using Entities.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Cliente
{
    public class TipoClienteService : TipoClienteIService
    {
        private readonly TipoClienteInterface _tipoClienteInterface;
        private readonly INotificador _notificador;

        public TipoClienteService(TipoClienteInterface tipoClienteInterface, INotificador notificador)
        {
            _tipoClienteInterface = tipoClienteInterface;
            _notificador = notificador;
        }

        public TipoCliente Get(int id)
        {
            return _tipoClienteInterface.Get(id);
        }

        public IEnumerable<TipoCliente> GetAll()
        {
            return _tipoClienteInterface.GetAll();
        }

        public TipoCliente Insert(TipoCliente tipoCliente)
        {
            if (TestarTipoCliente(tipoCliente))
                return _tipoClienteInterface.Insert(tipoCliente);
            return tipoCliente;
        }

        public TipoCliente Update(TipoCliente tipoCliente)
        {
            if (TestarTipoCliente(tipoCliente))
                return _tipoClienteInterface.Update(tipoCliente);
            return tipoCliente;
        }

        public void Delete(TipoCliente tipoCliente)
        {
            _tipoClienteInterface.Delete(tipoCliente);
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

        private bool TestarTipoCliente(TipoCliente tipoCliente)
        {
            try
            {
                if (tipoCliente.rectbtipocliente_descricao == null)
                    Notificar("Descrição é obrigatória");

                return !TemNotificacao();
            }
            catch (Exception e)
            {
                Notificar("Erro salvar tipo de cliente. " + e);
                return false;
            }
        }
    }
}
