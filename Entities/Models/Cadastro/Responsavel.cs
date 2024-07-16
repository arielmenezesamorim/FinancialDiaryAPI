using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Cadastro
{
    [Dapper.Contrib.Extensions.Table("cadtbresponsavel")]
    public class Responsavel
    {
        [Dapper.Contrib.Extensions.Key]
        public int cadtbresponsavel_pkseq { get; set; }
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string cadtbresponsavel_nome { get; set; }
        [Required(ErrorMessage = "Tipo de inscrição é obrigatório")]
        public int cadtbresponsavel_tipoinsc { get; set; }
        [Required(ErrorMessage = "Inscrição é obrigatório")]
        public long cadtbresponsavel_inscricao { get; set; }
        public string cadtbresponsavel_telefone { get; set; }
        public string cadtbresponsavel_celular { get; set; }
        public string cadtbresponsavel_email { get; set; }
        public int cadtbresponsavel_cep { get; set; }
        public string cadtbresponsavel_endereco { get; set; }
        public string cadtbresponsavel_nro { get; set; }
        public string cadtbresponsavel_complemento { get; set; }
        public string cadtbresponsavel_bairro { get; set; }
        public int? cadtbresponsavel_fkseqcidade { get; set; }
        public string cadtbresponsavel_nroregprofissional { get; set; }
        [Dapper.Contrib.Extensions.Write(false)]
        public Cidade fkcidade { get; set; }
    }
}
