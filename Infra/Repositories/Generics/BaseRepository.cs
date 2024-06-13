using Dapper;
using Dapper.Contrib.Extensions;
using Domain.Interfaces.Generics;
using Entities.Notification;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories.Generics
{
    public class BaseRepository<T> : IGeneric<T> where T : class
    {
        protected readonly IDbConnection connection;
        protected readonly ILogger _log;
        private readonly INotificador _notificador;
        protected string? stringConexaoBanco;
        protected readonly IsolationLevel isolationLevel = IsolationLevel.RepeatableRead;
        protected readonly IConfiguration configuration;

        public BaseRepository(IDbConnection connection, IConfiguration configuration, INotificador notificador, ILogger log)
        {
            this.connection = connection;
            this.configuration = configuration;
            this.stringConexaoBanco = configuration.GetConnectionString("FINANCIALDIARY");
            this.connection.ConnectionString = this.stringConexaoBanco;
            this._log = log;
            this._notificador = notificador;
        }

        public virtual T Get(int id)
        {
            return connection.Get<T>(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return connection.GetAll<T>();
        }

        public virtual T Insert(T entidade)
        {
            try
            {
                var chave = connection.Insert<T>(entidade);
                return entidade;
            }
            catch (Exception e)
            {
                var msgErro = "Erro ao incluir dados. " + e.Message;
                Notificar(msgErro);
                this._log.LogError(msgErro);
                return entidade;
            }
        }

        public virtual T Update(T entidade)
        {
            try
            {
                var resposta = connection.Update<T>(entidade);
                return entidade;
            }
            catch (Exception e)
            {
                var msgErro = "Erro ao atualizar dados. " + e.Message;
                Notificar(msgErro);
                this._log.LogError(msgErro);
                return entidade;
            }
        }

        public virtual void Delete(T entidade)
        {
            try
            {
                var excluiu = connection.Delete<T>(entidade);
                if (!excluiu)
                {
                    Notificar("Nenhum registro foi excluído!");
                }
            }
            catch (Npgsql.PostgresException ex)
            {
                Notificar("Erro ao excluir dados em transação. " + ex.Message);
            }
        }

        public virtual IEnumerable<T> LerTabela(string SQL, Dictionary<string, object> parametros)
        {
            return connection.Query<T>(SQL, param: parametros);
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
    }
}
