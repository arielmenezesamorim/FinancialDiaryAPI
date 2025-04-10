using Entities.Models.Cadastro;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Tesouraria
{
    [Dapper.Contrib.Extensions.Table("testbconta")]
    public class Conta
    {
        [Dapper.Contrib.Extensions.Key]
        public int testbconta_pkseq { get; set; }
        [Required(ErrorMessage = "Descrição é obrigatória")]
        public string testbconta_descricao { get; set; }
        [Required(ErrorMessage = "Tipo é obrigatório")]
        public int testbconta_tipo { get; set; }
        public int? testbconta_fkseqbanco { get; set; }
        public string testbconta_nroconta { get; set; }
        public string testbconta_digitoconta { get; set; }

        [Dapper.Contrib.Extensions.Write(false)]
        public Banco? fkbanco { get; set; }
    }
}
