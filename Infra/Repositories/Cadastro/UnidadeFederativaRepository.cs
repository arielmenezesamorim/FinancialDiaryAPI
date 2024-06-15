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
    public class UnidadeFederativaRepository : BaseRepository<UnidadeFederativa>, UnidadeFederativaInterface
    {
        private readonly ILogger _log;

        public UnidadeFederativaRepository(IDbConnection connection, IConfiguration configuration, INotificador notificador, ILogger<PaisRepository> log) : base(connection, configuration, notificador, log)
        {
            this._log = log;
        }

        public UnidadeFederativa Get(string sigla)
        {
            var SQL = @"SELECT * FROM cadtbunfederativa
                        WHERE upper(cadtbunfederativa_pksigla) = @SIGLA ";

            var qry = connection.Query<UnidadeFederativa>(SQL, new { SIGLA = sigla.ToUpper() });
            return qry.FirstOrDefault();
        }

        public object Filtrar(UnidadeFederativaFiltro filtro)
        {
            var SQL = new StringBuilder(" SELECT * FROM cadtbunfederativa ");
            var parametros = new Dictionary<string, object>();
            if (filtro != null)
            {
                if (!String.IsNullOrEmpty(filtro.nome))
                {
                    SQL.Append(" WHERE UPPER(cadtbunfederativa_nome) LIKE @nome ");
                    parametros.Add("nome", filtro.nome.ToUpper() + "%");
                }

                if (!String.IsNullOrEmpty(filtro.colunaOrdem))
                    SQL.Append($" ORDER BY {filtro.colunaOrdem} {filtro.ordem} ");
                else
                    SQL.Append($" ORDER BY cadtbunfederativa_nome, cadtbunfederativa_pksigla ");

                if (filtro.tamanhoPagina != 0)
                    SQL.Append($" LIMIT {filtro.tamanhoPagina} OFFSET {filtro.tamanhoPagina * filtro.numeroPagina}");
            }

            return new
            {
                conteudo = LerTabela(SQL.ToString(), parametros),
                totalElementos = CalcularTotalElementos(filtro)
            };
        }

        private int CalcularTotalElementos(UnidadeFederativaFiltro filtro)
        {
            var SQL = new StringBuilder(" SELECT COUNT(cadtbunfederativa_pksigla) FROM cadtbunfederativa ");
            if (filtro != null)
            {
                if (!String.IsNullOrEmpty(filtro.nome))
                    SQL.Append($" WHERE UPPER(cadtbunfederativa_nome) LIKE '{filtro.nome.ToUpper()}' ");
            }

            var totalElementos = connection.ExecuteScalar<int>(SQL.ToString());
            return totalElementos;
        }
    }
}
