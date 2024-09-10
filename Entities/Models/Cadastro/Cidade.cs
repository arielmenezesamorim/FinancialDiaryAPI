using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Cadastro
{
    [Dapper.Contrib.Extensions.Table("cadtbcidade")]
    public class Cidade
    {
        [Dapper.Contrib.Extensions.Key]
        public int cadtbcidade_pkseq {  get; set; }
        [Required(ErrorMessage = "Unidade Federativa é obrigatório")]
        public string cadtbcidade_fksiglauf { get; set; }
        [Required(ErrorMessage = "País é obrigatório")]
        public string cadtbcidade_fksiglapais { get; set; }
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string cadtbcidade_nome { get; set; }
        public int cadtbcidade_codmunicipio { get; set; }
        [Dapper.Contrib.Extensions.Write(false)]
        public UnidadeFederativa fkuf { get; set; }
        [Dapper.Contrib.Extensions.Write(false)]
        public Pais fkpais { get; set; }
        [Dapper.Contrib.Extensions.Write(false)]
        public string cidade_uf_mostrar
        {
            get
            {
                return cadtbcidade_nome + " - " + cadtbcidade_fksiglauf;
            }
        }
    }
}
