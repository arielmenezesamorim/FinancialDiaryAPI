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
    public class BancoService : BancoIService
    {
        private readonly BancoInterface _bancoInterface;
        private readonly CidadeInterface _cidadeInterface;
        private readonly INotificador _notificador;

        public BancoService(BancoInterface bancoInterface, CidadeInterface cidadeInterface, INotificador notificador)
        {
            _bancoInterface = bancoInterface;
            _cidadeInterface = cidadeInterface;
            _notificador = notificador;
        }

        public Banco Get(int id)
        {
            return _bancoInterface.Get(id);
        }

        public IEnumerable<Banco> GetAll()
        {
            return _bancoInterface.GetAll();
        }

        public Banco Insert(Banco banco)
        {
            if (Testarbanco(banco, "I"))
                return _bancoInterface.Insert(banco);
            return banco;
        }

        public Banco Update(Banco banco)
        {
            if (Testarbanco(banco, "U"))
                return _bancoInterface.Update(banco);
            return banco;
        }

        public void Delete(Banco banco)
        {
            _bancoInterface.Delete(banco);
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

        private bool Testarbanco(Banco banco, string operacao)
        {
            try
            {
                var agencia = _bancoInterface.GetAgencia(banco.cadtbbanco_agencia);
                if (operacao == "I" && banco.cadtbbanco_agencia == agencia.cadtbbanco_agencia)
                    Notificar("Agência já cadastrada");

                if (banco.cadtbbanco_fkseqcidade == 0)
                    Notificar("Cidade é obrigatória.");
                else
                {
                    var cidade = _cidadeInterface.Get(banco.cadtbbanco_fkseqcidade);
                    if (cidade == null)
                        Notificar("Cidade não cadastrada");
                }

                return !TemNotificacao();
            }
            catch (Exception e)
            {
                Notificar("Erro ao inserir a banco. " + e);
                return false;
            }
        }
    }
}
