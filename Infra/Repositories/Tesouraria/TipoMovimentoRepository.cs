using Dapper;
using Domain.Interfaces.Tesouraria;
using Entities.Models.Filtro.Tesouraria;
using Entities.Models.Tesouraria;
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

namespace Infra.Repositories.Tesouraria
{
    public class TipoMovimentoRepository : BaseRepository<TipoMovimento>, TipoMovimentoInterface
    {
        public TipoMovimentoRepository(IDbConnection connection, IConfiguration configuration, INotificador notificador, ILogger<TipoMovimentoRepository> log) : base(connection, configuration, notificador, log) 
        { }

        public object Filtrar(TipoMovimentoFiltro filtro)
        {
            var SQL = new StringBuilder(" SELECT * FROM testbtipomovimento ");
            var parametros = new Dictionary<string, object>();

            if (filtro != null)
            {
                SQL.Append(FormarWhereFiltro(filtro));

                if (!String.IsNullOrEmpty(filtro.colunaOrdem))
                    SQL.Append($" ORDER BY {filtro.colunaOrdem} {filtro.ordem} ");
                else
                    SQL.Append($" ORDER BY  testbtipomovimento_descricao, testbtipomovimento_pkseq");

                if (filtro.tamanhoPagina != 0)
                    SQL.Append($" LIMIT {filtro.tamanhoPagina} OFFSET {filtro.tamanhoPagina * filtro.numeroPagina}");
            }

            return new
            {
                conteudo = LerTabela(SQL.ToString(), parametros),
                totalElementos = CalcularTotalElementos(filtro)
            };
        }

        private int CalcularTotalElementos(TipoMovimentoFiltro filtro)
        {
            var SQL = new StringBuilder(" SELECT COUNT(testbtipomovimento_pkseq) FROM testbtipomovimento ");
            if (filtro != null)
            {
                SQL.Append(FormarWhereFiltro(filtro));
            }

            var totalElementos = connection.ExecuteScalar<int>(SQL.ToString());
            return totalElementos;
        }

        private StringBuilder FormarWhereFiltro(TipoMovimentoFiltro filtro)
        {
            var whereAnd = " WHERE ";
            var SQL = new StringBuilder("");

            if (filtro != null)
            {
                if (!String.IsNullOrEmpty(filtro.descricao))
                {
                    SQL.Append($" {whereAnd} UPPER(testbtipomovimento_descricao) LIKE '{filtro.descricao.ToUpper()}%' ");
                    whereAnd = " AND ";
                }

                if (filtro.tipo != -1)
                {
                    SQL.Append($" {whereAnd} testbtipomovimento_tipo = {filtro.tipo} ");
                    whereAnd = " AND ";
                }

                if (filtro.indicador != -1)
                {
                    SQL.Append($" {whereAnd} testbtipomovimento_indicador = {filtro.indicador} ");
                    whereAnd = " AND ";
                }
            }

            return SQL;
        }
    }
}
