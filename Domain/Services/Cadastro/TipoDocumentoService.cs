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
    public class TipoDocumentoService : TipoDocumentoIService
    {
        private readonly TipoDocumentoInterface _tipoDocumentoInterface;
        private readonly INotificador _notificador;

        public TipoDocumentoService(TipoDocumentoInterface tipoDocumentoInterface,
                                    INotificador notificador)
        {
            _tipoDocumentoInterface = tipoDocumentoInterface;
            _notificador = notificador;
        }

        public TipoDocumento Get(int id)
        {
            return _tipoDocumentoInterface.Get(id);
        }

        public IEnumerable<TipoDocumento> GetAll()
        {
            return _tipoDocumentoInterface.GetAll();
        }

        public TipoDocumento Insert(TipoDocumento tipoDocumento)
        {
            if (TestarTipoDocumento(tipoDocumento, "I"))
                return _tipoDocumentoInterface.Insert(tipoDocumento);
            return tipoDocumento;
        }

        public TipoDocumento Update(TipoDocumento tipoDocumento)
        {
            if (TestarTipoDocumento(tipoDocumento, "U"))
                return _tipoDocumentoInterface.Update(tipoDocumento);
            return tipoDocumento;
        }

        public void Delete(TipoDocumento tipoDocumento)
        {
            _tipoDocumentoInterface.Delete(tipoDocumento);
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

        private bool TestarTipoDocumento(TipoDocumento tipoDocumento, string operacao)
        {
            try
            {
                if (operacao.Equals("I"))
                {
                    var tipoDocumentoAux = _tipoDocumentoInterface.Get(tipoDocumento.cadtbtipodocumento_sigla);

                    if (tipoDocumentoAux != null)
                        Notificar("Já existe tipo de documento com essa sigla.");
                }
                return !TemNotificacao();
            }
            catch (Exception e)
            {
                Notificar("Erro ao inserir o tipo de documento. " + e);
                return false;
            }
        }
    }
}
