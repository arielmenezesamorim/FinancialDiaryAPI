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
    public class DepartamentoRepository : BaseRepository<Departamento>, DepartamentoInterface
    {
        private readonly ILogger _log;

        public DepartamentoRepository(IDbConnection connection, IConfiguration configuration, INotificador notificador, ILogger<DepartamentoRepository> log) : base(connection, configuration, notificador, log)
        {
            this._log = log;
        }

        public Departamento Get(string sigla)
        {
            var SQL = @"SELECT * FROM cadtbdepartamento
                        WHERE upper(cadtbdepartamento_sigla) = @SIGLA";

            var qry = connection.Query<Departamento>(SQL, param: new { SIGLA = sigla.ToUpper() });
            return qry.FirstOrDefault();
        }

        public object Filtrar(DepartamentoFiltro filtro)
        {
            var SQL = new StringBuilder(" SELECT * FROM cadtbdepartamento ");
            var parametros = new Dictionary<string, object>();

            if (filtro != null)
            {
                if (!String.IsNullOrEmpty(filtro.descricao))
                {
                    SQL.Append(" WHERE UPPER(cadtbdepartamento_descricao) LIKE @nome ");
                    parametros.Add("nome", filtro.descricao.ToUpper() + "%");
                }

                if (!String.IsNullOrEmpty(filtro.colunaOrdem))
                    SQL.Append($" ORDER BY {filtro.colunaOrdem} {filtro.ordem} ");
                else
                    SQL.Append($" ORDER BY cadtbdepartamento_descricao, cadtbdepartamento_pkseq ");

                if (filtro.tamanhoPagina != 0)
                    SQL.Append($" LIMIT {filtro.tamanhoPagina} OFFSET {filtro.tamanhoPagina * filtro.numeroPagina}");
            }

            return new
            {
                conteudo = LerTabela(SQL.ToString(), parametros),
                totalElementos = CalcularTotalElementos(filtro)
            };
        }

        private int CalcularTotalElementos(DepartamentoFiltro filtro)
        {
            var SQL = new StringBuilder(" SELECT COUNT(cadtbdepartamento_pkseq) FROM cadtbdepartamento ");
            if (filtro != null)
            {
                if (!String.IsNullOrEmpty(filtro.descricao))
                    SQL.Append($" WHERE UPPER(cadtbdepartamento_descricao) LIKE '{filtro.descricao.ToUpper() + "%"}'");
            }

            var totalElementos = connection.ExecuteScalar<int>(SQL.ToString());
            return totalElementos;
        }
    }
}
