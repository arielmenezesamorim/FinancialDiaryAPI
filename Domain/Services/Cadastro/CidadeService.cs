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
    public class CidadeService : CidadeIService
    {
        private readonly CidadeInterface _cidadeInterface;
        private readonly PaisInterface _paisInterface;
        private readonly UnidadeFederativaInterface _unidadeFederativaInterface;
        private readonly INotificador _notificador;

        public CidadeService(CidadeInterface cidadeInterface, PaisInterface paisInterface, UnidadeFederativaInterface unidadeFederativaInterface, INotificador notificador)
        {
            _cidadeInterface = cidadeInterface;
            _paisInterface = paisInterface;
            _unidadeFederativaInterface = unidadeFederativaInterface;
            _notificador = notificador;
        }

        public Cidade Get(int id)
        {
            return _cidadeInterface.Get(id);
        }

        public IEnumerable<Cidade> GetAll()
        {
            return _cidadeInterface.GetAll();
        }

        public Cidade Insert(Cidade cidade)
        {
            if (TestarCidade(cidade))
                return _cidadeInterface.Insert(cidade);
            return cidade;
        }

        public Cidade Update(Cidade cidade)
        {
            if (TestarCidade(cidade))
                return _cidadeInterface.Update(cidade);
            return cidade;
        }

        public void Delete(Cidade cidade)
        {
            _cidadeInterface.Delete(cidade);
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

        private bool TestarCidade(Cidade cidade)
        {
            try
            {
                var pais = _paisInterface.Get(cidade.cadtbcidade_fksiglapais); 
                if (pais == null)
                    Notificar("País não cadastrado.");
                else
                {                    
                    if (cidade.cadtbcidade_codmunicipio == 0)
                        Notificar("Código do município é obrigatório.");

                    if ((cidade.cadtbcidade_fksiglauf == null) || (cidade.cadtbcidade_fksiglauf.Trim().Equals("")))
                        Notificar("Unidade federativa deve ser informada.");
                    else
                    {
                        var unidadeFederativa = _unidadeFederativaInterface.Get(cidade.cadtbcidade_fksiglauf);
                        if (unidadeFederativa == null)
                            Notificar("UF (Unidade Federativa) não cadastrada.");
                    }
                }

                return !TemNotificacao();
            }
            catch (Exception e)
            {
                Notificar("Erro ao inserir a cidade. " + e);
                return false;
            }
        }
    }
}
