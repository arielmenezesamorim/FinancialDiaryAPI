using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Cadastro
{
    [Dapper.Contrib.Extensions.Table("cadtbformapagamento")]
    public class FormaPagamento
    {
        [Dapper.Contrib.Extensions.Key]
        public int cadtbformapagamento_pkseq { get; set; }

        [Required(ErrorMessage = "Sigla é Obrigatória")]
        public string? cadtbformapagamento_sigla { get; set; }

        [Required(ErrorMessage = "Descrição é obrigatório")]
        public string? cadtbformapagamento_descricao { get; set; }

        public bool cadtbformapagamento_gerartitulo { get; set; }

        public int cadtbformapagamento_fpagamento { get; set; }

        public int? cadtbformapagamento_fkseqdoc { set; get; }

        [Dapper.Contrib.Extensions.Write(false)]
        public TipoDocumento? fkdocumento { set; get; }

        [Dapper.Contrib.Extensions.Write(false)]
        public string urlprograma { set; get; }

        [Dapper.Contrib.Extensions.Write(false)]
        public string formapagamento_mostrar
        {
            get
            {
                return cadtbformapagamento_sigla + " - " + cadtbformapagamento_descricao;
            }
        }
    }
}
