using Dapper;
using Domain.Interfaces.Tesouraria;
using Entities.Models.Cadastro;
using Entities.Models.Filtro.Cadastro;
using Entities.Models.Filtro.Tesouraria;
using Entities.Models.Tesouraria;
using Entities.Notification;
using Infra.Repositories.Cadastro;
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
    public class ContaRepository : BaseRepository<Conta>, ContaInterface
    {
        private readonly ILogger _log;

        public ContaRepository(IDbConnection connection, IConfiguration configuration, INotificador notificador, ILogger<ContaRepository> log) : base(connection, configuration, notificador, log)
        {
            this._log = log;
        }

        public Conta GetConta(string nroconta)
        {
            var SQL = @"SELECT * FROM testbconta
                        WHERE testbconta_nroconta = @nroconta";

            var qry = connection.Query<Conta>(SQL, param: new { nroconta = nroconta });
            return qry.FirstOrDefault();
        }

        public object Filtrar(ContaFiltro filtro)
        {
            var SQL = new StringBuilder(@" SELECT * FROM testbconta 
                                           LEFT OUTER JOIN cadtbbanco ON cadtbbanco_pkseq = testbconta_fkseqbanco");
            var parametros = new Dictionary<string, object>();

            if (filtro != null)
            {
                SQL.Append(FormarWhereFiltro(filtro));

                if (!String.IsNullOrEmpty(filtro.colunaOrdem))
                    SQL.Append($" ORDER BY {filtro.colunaOrdem} {filtro.ordem} ");
                else
                    SQL.Append($" ORDER BY testbconta_descricao, testbconta_pkseq");

                if (filtro.tamanhoPagina != 0)
                    SQL.Append($" LIMIT {filtro.tamanhoPagina} OFFSET {filtro.tamanhoPagina * filtro.numeroPagina}");
            }

            return new
            {
                conteudo = connection.Query<Conta, Banco, Conta>(SQL.ToString(),
                (conta, banco) => { conta.fkbanco = banco; return conta; },
                splitOn: "cadtbbanco_pkseq",
                param: parametros),
                totalElementos = CalcularTotalElementos(filtro)
            };
        }

        private int CalcularTotalElementos(ContaFiltro filtro)
        {
            var SQL = new StringBuilder(@" SELECT COUNT(testbconta_pkseq) FROM testbconta");
            if (filtro != null)
            {
                SQL.Append(FormarWhereFiltro(filtro));
            }

            var totalElementos = connection.ExecuteScalar<int>(SQL.ToString());
            return totalElementos;
        }

        private StringBuilder FormarWhereFiltro(ContaFiltro filtro)
        {
            var whereAnd = " WHERE ";
            var SQL = new StringBuilder("");

            if (filtro != null)
            {
                if (!String.IsNullOrEmpty(filtro.descricao))
                {
                    SQL.Append($" {whereAnd} UPPER(testbconta_descricao) LIKE '{filtro.descricao.ToUpper()}%' ");
                    whereAnd = " AND ";
                }

                if (filtro.tipoConta != -1)
                {
                    SQL.Append($" {whereAnd} testbconta_tipo = {filtro.tipoConta} ");
                    whereAnd = " AND ";
                }
            }
            return SQL;
        }
    }
}
