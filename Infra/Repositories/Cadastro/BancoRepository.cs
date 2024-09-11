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
    public class BancoRepository : BaseRepository<Banco>, BancoInterface
    {
        private readonly ILogger _log;

        public BancoRepository(IDbConnection connection, IConfiguration configuration, INotificador notificador, ILogger<BancoRepository> log) : base(connection, configuration, notificador, log)
        {
            this._log = log;
        }

        public Banco GetBanco(int id)
        {
            var SQL = @"SELECT * FROM cadtbbanco
                        LEFT OUTER JOIN cadtbcidade ON cadtbcidade_pkseq = cadtbbanco_fkseqcidade
                        WHERE cadtbbanco_pkseq = @sequencia
                        ORDER BY cadtbbanco_pkseq";

            var qry = connection.Query<Banco, Cidade, Banco>
              (
                  SQL,
                  (banco, cidade) => { banco.fkcidade = cidade; return banco; },
                  splitOn: "cadtbcidade_pkseq",
                  param: new { sequencia = id }
              );

            return qry.FirstOrDefault();
        }

        public Banco GetAgencia(int agencia)
        {
            var SQL = @"SELECT * FROM cadtbbanco
                        WHERE cadtbbanco_agencia = @AGENCIA";

            var qry = connection.Query<Banco>(SQL, param: new { AGENCIA = agencia });
            return qry.FirstOrDefault();

        }

        public object Filtrar(BancoFiltro filtro)
        {
            var SQL = new StringBuilder(" SELECT * FROM cadtbbanco ");
            var parametros = new Dictionary<string, object>();

            if (filtro != null)
            {
                if (!String.IsNullOrEmpty(filtro.descricao))
                {
                    SQL.Append(" WHERE UPPER(cadtbbanco_descricao) LIKE @nome ");
                    parametros.Add("nome", filtro.descricao.ToUpper() + "%");
                }

                if (!String.IsNullOrEmpty(filtro.colunaOrdem))
                    SQL.Append($" ORDER BY {filtro.colunaOrdem} {filtro.ordem} ");
                else
                    SQL.Append($" ORDER BY cadtbbanco_descricao, cadtbbanco_pkseq ");

                if (filtro.tamanhoPagina != 0)
                    SQL.Append($" LIMIT {filtro.tamanhoPagina} OFFSET {filtro.tamanhoPagina * filtro.numeroPagina}");
            }

            return new
            {
                conteudo = LerTabela(SQL.ToString(), parametros),
                totalElementos = CalcularTotalElementos(filtro)
            };
        }

        private int CalcularTotalElementos(BancoFiltro filtro)
        {
            var SQL = new StringBuilder(" SELECT COUNT(cadtbbanco_pkseq) FROM cadtbbanco ");
            if (filtro != null)
            {
                if (!String.IsNullOrEmpty(filtro.descricao))
                    SQL.Append($" WHERE UPPER(cadtbbanco_descricao) LIKE '{filtro.descricao.ToUpper() + "%"}'");
            }

            return connection.ExecuteScalar<int>(SQL.ToString());
        }
    }
}
