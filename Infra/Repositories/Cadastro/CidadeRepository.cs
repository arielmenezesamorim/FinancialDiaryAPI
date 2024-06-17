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
    public class CidadeRepository : BaseRepository<Cidade>, CidadeInterface
    {
        private readonly ILogger _log;

        public CidadeRepository(IDbConnection connection, IConfiguration configuration, INotificador notificador, ILogger<PaisRepository> log) : base(connection, configuration, notificador, log)
        {
            this._log = log;
        }

        public Cidade GetCidade(int sequencia)
        {
            var SQL = @" SELECT * FROM cadtbcidade 
                         LEFT OUTER JOIN cadtbunfederativa ON cadtbunfederativa_pksigla = cadtbcidade_fksiglauf 
                         LEFT OUTER JOIN cadtbpais ON cadtbpais_pksigla = cadtbcidade_fksiglapais 
                         WHERE cadtbcidade.cadtbcidade_pkseq = @sequencia ";

            var qry = connection.Query<Cidade, UnidadeFederativa, Pais, Cidade>
              (
                  SQL,
                  (cidade, uf, pais) => { cidade.fkuf = uf; cidade.fkpais = pais; return cidade; },
                  splitOn: "cadtbunfederativa_pksigla, cadtbpais_pksigla",
                  param: new { sequencia }
              );

            return qry.FirstOrDefault();
        }

        public object Filtrar(CidadeFiltro filtro)
        {
            var parametros = new Dictionary<string, object>();
            var whereAnd = " WHERE ";
            var SQL = new StringBuilder(@" SELECT * FROM cadtbcidade
                                           LEFT OUTER JOIN cadtbunfederativa ON cadtbunfederativa_pksigla = cadtbcidade_fksiglauf 
                                           LEFT OUTER JOIN cadtbpais ON cadtbpais_pksigla = cadtbcidade_fksiglapais");
            if (filtro != null)
            {
                if (!String.IsNullOrEmpty(filtro.nome))
                {
                    SQL.Append($" {whereAnd} UPPER(cadtbcidade_nome) LIKE @nome");
                    parametros.Add("nome", filtro.nome.ToUpper() + "%");
                    whereAnd = " AND ";
                }

                if (!String.IsNullOrEmpty(filtro.uf))
                {
                    SQL.Append($" {whereAnd} UPPER(cadtbcidade_fksiglauf) = @uf");
                    parametros.Add("uf", filtro.uf.ToUpper());
                    whereAnd = " AND ";
                }

                if (filtro.codmunicipio != 0)
                {
                    SQL.Append($" {whereAnd} cadtbcidade_codmunicipio = @codmunicipio");
                    parametros.Add("codmunicipio", filtro.codmunicipio);
                    whereAnd = " AND ";
                }

                if (!String.IsNullOrEmpty(filtro.colunaOrdem))
                    SQL.Append($" ORDER BY {filtro.colunaOrdem} {filtro.ordem} ");
                else
                    SQL.Append($" ORDER BY cadtbcidade_nome, cadtbcidade_pkseq");

                if (filtro.tamanhoPagina != 0)
                    SQL.Append($" LIMIT {filtro.tamanhoPagina} OFFSET {filtro.tamanhoPagina * filtro.numeroPagina}");
            }

            return new
            {
                conteudo = LerTabela(SQL.ToString(), parametros),
                totalElementos = CalcularTotalElementos(filtro)
            };
        }

        public IEnumerable<Cidade> GetAllPorUf(string sigla)
        {
            var SQL = @" SELECT * FROM cadtbcidade 
                         LEFT OUTER JOIN cadtbunfederativa ON cadtbunfederativa_pksigla = cadtbcidade_fksiglauf 
                         LEFT OUTER JOIN cadtbpais ON cadtbpais_pksigla = cadtbcidade_fksiglapais 
                         WHERE cadtbunfederativa_pksigla = @sigla 
                         ORDER BY cadtbcidade_nome ";

            var qry = connection.Query<Cidade, UnidadeFederativa, Pais, Cidade>
              (
                  SQL,
                  (cidade, uf, pais) => { cidade.fkuf = uf; cidade.fkpais = pais; return cidade; },
                  splitOn: "cadtbunfederativa_pksigla, cadtbpais_pksigla",
                  param: new { sigla }
              );

            return qry;
        }

        private int CalcularTotalElementos(CidadeFiltro filtro)
        {
            var whereAnd = " WHERE ";
            var SQL = new StringBuilder(" SELECT COUNT(cadtbcidade_pkseq) FROM cadtbcidade ");
            if (filtro != null)
            {
                if (!String.IsNullOrEmpty(filtro.nome))
                {
                    SQL.Append($"{whereAnd} UPPER(cadtbcidade_nome) LIKE '{filtro.nome.ToUpper() + "%"}'");
                    whereAnd = " AND ";
                }

                if (!String.IsNullOrEmpty(filtro.uf))
                {
                    SQL.Append($"{whereAnd} UPPER(cadtbcidade_fksiglauf) = '{filtro.uf}'");
                    whereAnd = " AND ";
                }

                if (filtro.codmunicipio != 0)
                {
                    SQL.Append($" {whereAnd} cadtbcidade_codmunicipio = {filtro.codmunicipio}");
                    whereAnd = " AND ";
                }
            }

            return connection.ExecuteScalar<int>(SQL.ToString());
        }
    }
}
