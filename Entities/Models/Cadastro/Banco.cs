using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Cadastro
{
    [Dapper.Contrib.Extensions.Table("cadtbbanco")]
    public class Banco
    {
        [Dapper.Contrib.Extensions.Key]
        public int cadtbbanco_pkseq { set; get; }
        public int? cadtbbanco_agencia { set; get; }
        public string cadtbbanco_digitoagencia { set; get; }
        [Required(ErrorMessage = "Descrição é obrigatória")]
        public string cadtbbanco_descricao { set; get; }
        public int? cadtbbanco_cep { set; get; }
        public string cadtbbanco_endereco { set; get; }
        public string cadtbbanco_complemento { set; get; }
        public string cadtbbanco_nro { set; get; }
        public string cadtbbanco_bairro { set; get; }
        [Required(ErrorMessage = "Cidade é obrigatória")]
        public int cadtbbanco_fkseqcidade { set; get; }
        [Required(ErrorMessage = "Banco é obrigatório")]
        public int cadtbbanco_banco { set; get; }

        [Dapper.Contrib.Extensions.Write(false)]
        public Cidade fkcidade { set; get; }

        [Dapper.Contrib.Extensions.Write(false)]
        public string urlprograma { set; get; }
    }
}
