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
    public class PaisService : PaisIService
    {
        private readonly PaisInterface _paisInterface;
        private readonly INotificador _notificador;

        public PaisService(PaisInterface paisInterface,
                           INotificador notificador)
        {
            _paisInterface = paisInterface;
            _notificador = notificador;
        }

        public Pais Get(int id)
        {
            return _paisInterface.Get(id);
        }

        public IEnumerable<Pais> GetAll()
        {
            return _paisInterface.GetAll();
        }

        public Pais Insert(Pais pais)
        {
            if (TestarPais(pais, "I"))
                _paisInterface.Insert(pais);
            return pais;
        }

        public Pais Update(Pais pais)
        {
            if (TestarPais(pais, "U"))
                _paisInterface.Update(pais);
            return pais;
        }

        public void Delete(Pais pais)
        {
            _paisInterface.Delete(pais);
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

        private bool TestarPais(Pais pais, string operacao)
        {
            try
            {
                if (pais.cadtbpais_codbacen == 0)
                    Notificar("Código do bacen é obrigatório");

                if (operacao.Equals("I"))
                {
                    if (_paisInterface.Get(pais.cadtbpais_pksigla) != null)
                        Notificar("País já cadastrado");
                }

                return !TemNotificacao();
            }
            catch (Exception e)
            {
                Notificar("Erro ao salvar o País. " + e);
                return false;
            }
        }
    }
}
