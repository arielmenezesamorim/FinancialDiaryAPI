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
    public class UnidadeFederativaService : UnidadeFederativaIService
    {
        private readonly UnidadeFederativaInterface _unidadeFederativaInterface;
        private readonly INotificador _notificador;

        public UnidadeFederativaService(UnidadeFederativaInterface unidadeFederativaInterface,
                                        INotificador notificador)
        {
            _unidadeFederativaInterface = unidadeFederativaInterface;
            _notificador = notificador;
        }

        public UnidadeFederativa Get(int id)
        {
            return _unidadeFederativaInterface.Get(id);
        }

        public IEnumerable<UnidadeFederativa> GetAll()
        {
            return _unidadeFederativaInterface.GetAll();
        }

        public UnidadeFederativa Insert(UnidadeFederativa unidadeFederativa)
        {
            if (TestarUnidadeFederativa(unidadeFederativa, "I"))
                return _unidadeFederativaInterface.Insert(unidadeFederativa);
            return unidadeFederativa;
        }

        public UnidadeFederativa Update(UnidadeFederativa unidadeFederativa)
        {
            if (TestarUnidadeFederativa(unidadeFederativa, "U"))
                return _unidadeFederativaInterface.Update(unidadeFederativa);
            return unidadeFederativa;
        }

        public void Delete(UnidadeFederativa unidadeFederativa)
        {
            _unidadeFederativaInterface.Delete(unidadeFederativa);
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

        private bool TestarUnidadeFederativa(UnidadeFederativa unidadeFederativa, string operacao)
        {
            try
            {
                if (operacao.Equals("I"))
                {
                    if (_unidadeFederativaInterface.Get(unidadeFederativa.cadtbunfederativa_pksigla) != null)
                        Notificar("Unidade federativa já cadastrada");
                }

                return !TemNotificacao();
            }
            catch (Exception e)
            {
                Notificar("Erro ao testar tabela de Unidade Federativa. " + e);
                return false;
            }
        }
    }
}
