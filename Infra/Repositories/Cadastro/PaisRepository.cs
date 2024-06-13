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
    public class PaisRepository : BaseRepository<Pais>, PaisInterface
    {
        private readonly ILogger _log;

        public PaisRepository(IDbConnection connection, IConfiguration configuration, INotificador notificador, ILogger<PaisRepository> log) : base(connection, configuration, notificador, log)
        {
            this._log = log;
        }

        public Pais Get(string sigla)
        {
            var SQL = @"SELECT * FROM cadtbpais
                        WHERE upper(cadtbpais_pksigla) = @SIGLA";

            var qry = connection.Query<Pais>(SQL, param: new { SIGLA = sigla.ToUpper() });
            return qry.FirstOrDefault();
        }

        public object Filtrar(PaisFiltro filtro)
        {
            var parametros = new Dictionary<string, object>();
            var SQL = new StringBuilder(" SELECT * FROM cadtbpais ");
            var whereAnd = " WHERE ";

            if (filtro == null)
                SQL.Append(" ORDER BY cadtbpais_pksigla ");
            else
            {
                if (!String.IsNullOrEmpty(filtro.nome))
                {
                    SQL.Append($"{whereAnd} UPPER(cadtbpais_nome) LIKE @nome ");
                    parametros.Add("nome", filtro.nome.ToUpper() + "%");
                    whereAnd = " AND ";
                }

                if (filtro.continente != 5)
                {
                    SQL.Append($"{whereAnd} cadtbpais_continente = @continente ");
                    parametros.Add("continente", filtro.continente);
                    whereAnd = " AND ";
                }

                if (!String.IsNullOrEmpty(filtro.colunaOrdem))
                    SQL.Append($" ORDER BY {filtro.colunaOrdem} {filtro.ordem} ");
                else
                    SQL.Append($" ORDER BY cadtbpais_pksigla ");

                if (filtro.tamanhoPagina != 0)
                    SQL.Append($" LIMIT {filtro.tamanhoPagina} OFFSET {filtro.tamanhoPagina * filtro.numeroPagina}");
            }

            return new
            {
                conteudo = LerTabela(SQL.ToString(), parametros),
                totalElementos = CalcularTotalElementos(filtro)
            };
        }

        private int CalcularTotalElementos(PaisFiltro filtro)
        {
            var whereAnd = " WHERE ";
            var SQL = new StringBuilder(" SELECT COUNT(cadtbpais_pksigla) FROM cadtbpais ");
            if (filtro != null)
            {
                if (!String.IsNullOrEmpty(filtro.nome))
                {
                    SQL.Append($"{whereAnd} UPPER(cadtbpais_nome) LIKE '{filtro.nome.ToUpper() + "%"}'");
                    whereAnd = " AND ";
                }

                if (filtro.continente != 5)
                {
                    SQL.Append($"{whereAnd} cadtbpais_continente = {filtro.continente}");
                    whereAnd = " AND ";
                }
            }

            return connection.ExecuteScalar<int>(SQL.ToString());
        }
    }
}
