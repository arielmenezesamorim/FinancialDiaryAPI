using Dapper;
using Domain.Interfaces.Cadastro;
using Entities.Models.Cadastro;
using Entities.Models.Filtro.Cadastro;
using Entities.Notification;
using Infra.Repositories.Generics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories.Cadastro
{
    public class FormaPagamentoRepository : BaseRepository<FormaPagamento>, FormaPagamentoInterface
    {
        private readonly ILogger _log;

        public FormaPagamentoRepository(IDbConnection connection, IConfiguration configuration, INotificador notificador, ILogger<FormaPagamentoRepository> log) : base(connection, configuration, notificador, log)
        {
            this._log = log;
        }

        public FormaPagamento GetFormaPagamento(string sigla, string descricao)
        {
            var SQL = connection.Query<FormaPagamento>(" SELECT * FROM cadtbformapagamento WHERE cadtbformapagamento_sigla = @sigla OR cadtbformapagamento_descricao = @descricao", new { sigla = sigla, descricao = descricao }).FirstOrDefault();
            return SQL;
        }

        public object Filtrar(FormaPagamentoFiltro filtro)
        {
            var SQL = new StringBuilder(" SELECT * FROM cadtbformapagamento ");
            var parametros = new Dictionary<string, object>();

            if (filtro != null)
            {
                if (!String.IsNullOrEmpty(filtro.descricao))
                {
                    SQL.Append(" WHERE UPPER(cadtbformapagamento_descricao) LIKE @nome ");
                    parametros.Add("nome", filtro.descricao.ToUpper() + "%");
                }

                if (!String.IsNullOrEmpty(filtro.colunaOrdem))
                    SQL.Append($" ORDER BY {filtro.colunaOrdem} {filtro.ordem} ");
                else
                    SQL.Append($" ORDER BY cadtbformapagamento_descricao, cadtbformapagamento_pkseq ");

                if (filtro.tamanhoPagina != 0)
                    SQL.Append($" LIMIT {filtro.tamanhoPagina} OFFSET {filtro.tamanhoPagina * filtro.numeroPagina}");
            }

            return new
            {
                conteudo = LerTabela(SQL.ToString(), parametros),
                totalElementos = CalcularTotalElementos(filtro)
            };
        }


        private int CalcularTotalElementos(FormaPagamentoFiltro filtro)
        {
            var SQL = new StringBuilder(" SELECT COUNT(cadtbformapagamento_pkseq) FROM cadtbformapagamento ");
            if (filtro != null)
            {
                if (!String.IsNullOrEmpty(filtro.descricao))
                    SQL.Append($" WHERE UPPER(cadtbformapagamento_descricao) LIKE '{filtro.descricao.ToUpper() + "%"}'");
            }

            return connection.ExecuteScalar<int>(SQL.ToString());
        }
    }
}
