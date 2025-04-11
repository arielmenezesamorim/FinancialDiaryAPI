using Dapper;
using Domain.Interfaces.Cliente;
using Entities.Models.Cliente;
using Entities.Models.Filtro.Cliente;
using Entities.Notification;
using Infra.Repositories.Generics;
using Infra.Repositories.Tesouraria;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories.Cliente
{
    public class TipoClienteRepository : BaseRepository<TipoCliente>, TipoClienteInterface
    {

        public TipoClienteRepository(IDbConnection connection, IConfiguration configuration, INotificador notificador, ILogger<TipoClienteRepository> log) : base(connection, configuration, notificador, log)
        { }

        public object Filtrar(TipoClienteFiltro filtro)
        {
            var SQL = new StringBuilder(" SELECT * FROM rectbtipocliente ");
            var parametros = new Dictionary<string, object>();

            if (filtro != null)
            {
                SQL.Append(FormarWhereFiltro(filtro));

                if (!String.IsNullOrEmpty(filtro.colunaOrdem))
                    SQL.Append($" ORDER BY {filtro.colunaOrdem} {filtro.ordem} ");
                else
                    SQL.Append($" ORDER BY  rectbtipocliente_descricao, rectbtipocliente_pkseq");

                if (filtro.tamanhoPagina != 0)
                    SQL.Append($" LIMIT {filtro.tamanhoPagina} OFFSET {filtro.tamanhoPagina * filtro.numeroPagina}");
            }

            return new
            {
                conteudo = LerTabela(SQL.ToString(), parametros),
                totalElementos = CalcularTotalElementos(filtro)
            };
        }

        private int CalcularTotalElementos(TipoClienteFiltro filtro)
        {
            var SQL = new StringBuilder(" SELECT COUNT(rectbtipocliente_pkseq) FROM rectbtipocliente ");
            if (filtro != null)
            {
                SQL.Append(FormarWhereFiltro(filtro));
            }

            var totalElementos = connection.ExecuteScalar<int>(SQL.ToString());
            return totalElementos;
        }

        private StringBuilder FormarWhereFiltro(TipoClienteFiltro filtro)
        {
            var whereAnd = " WHERE ";
            var SQL = new StringBuilder("");

            if (filtro != null)
            {
                if (!String.IsNullOrEmpty(filtro.descricao))
                {
                    SQL.Append($" {whereAnd} UPPER(rectbtipocliente_descricao) LIKE '{filtro.descricao.ToUpper()}%' ");
                    whereAnd = " AND ";
                }

                if (filtro.tipo != -1)
                {
                    SQL.Append($" {whereAnd} rectbtipocliente_tipo = {filtro.tipo} ");
                    whereAnd = " AND ";
                }

                if (filtro.indicador != -1)
                {
                    SQL.Append($" {whereAnd} rectbtipocliente_indicador = {filtro.indicador} ");
                    whereAnd = " AND ";
                }
            }

            return SQL;
        }
    }
}
