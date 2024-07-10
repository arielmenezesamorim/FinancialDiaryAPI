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
    public class TipoDocumentoRepository : BaseRepository<TipoDocumento>, TipoDocumentoInterface
    {
        private readonly ILogger _log;

        public TipoDocumentoRepository(IDbConnection connection, IConfiguration configuration, INotificador notificador, ILogger<PaisRepository> log) : base(connection, configuration, notificador, log)
        {
            this._log = log;
        }

        public TipoDocumento Get(string sigla)
        {
            var SQL = @" SELECT * FROM cadtbtipodocumento 
                         WHERE cadtbtipodocumento.cadtbtipodocumento_sigla = @sigla ";

            var qry = connection.Query<TipoDocumento>(SQL, new { sigla = sigla });
            return qry.FirstOrDefault();
        }

        public object Filtrar(TipoDocumentoFiltro filtro)
        {
            var SQL = new StringBuilder(" SELECT * FROM cadtbtipodocumento ");
            var parametros = new Dictionary<string, object>();

            if (filtro != null)
            {
                if (!String.IsNullOrEmpty(filtro.descricao))
                {
                    SQL.Append(" WHERE UPPER(cadtbtipodocumento_descricao) LIKE @nome ");
                    parametros.Add("nome", filtro.descricao.ToUpper() + "%");
                }

                if (!String.IsNullOrEmpty(filtro.colunaOrdem))
                    SQL.Append($" ORDER BY {filtro.colunaOrdem} {filtro.ordem} ");
                else
                    SQL.Append($" ORDER BY cadtbtipodocumento_descricao, cadtbtipodocumento_pkseq ");

                if (filtro.tamanhoPagina != 0)
                    SQL.Append($" LIMIT {filtro.tamanhoPagina} OFFSET {filtro.tamanhoPagina * filtro.numeroPagina}");
            }

            return new
            {
                conteudo = LerTabela(SQL.ToString(), parametros),
                totalElementos = CalcularTotalElementos(filtro)
            };
        }

        private int CalcularTotalElementos(TipoDocumentoFiltro filtro)
        {
            var SQL = new StringBuilder(" SELECT COUNT(cadtbtipodocumento_pkseq) FROM cadtbtipodocumento ");
            if (filtro != null)
            {
                if (!String.IsNullOrEmpty(filtro.descricao))
                    SQL.Append($" WHERE UPPER(cadtbtipodocumento_descricao) LIKE '{filtro.descricao.ToUpper() + "%"}'");
            }

            var totalElementos = connection.ExecuteScalar<int>(SQL.ToString());
            return totalElementos;
        }

    }
}
