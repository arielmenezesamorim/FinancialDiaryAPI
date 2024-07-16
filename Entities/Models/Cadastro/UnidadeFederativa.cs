using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Cadastro
{
    [Dapper.Contrib.Extensions.Table("cadtbunfederativa")]
    public class UnidadeFederativa
    {
        [Dapper.Contrib.Extensions.ExplicitKey]
        [Required(ErrorMessage = "Sigla é obrigatória")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Sigla da unidade federativa deve conter {2} caracters")]
        public string cadtbunfederativa_pksigla { set; get; }
        [Required(ErrorMessage = "Nome é obrigatória")]
        public string cadtbunfederativa_nome { set; get; }
        [Required(ErrorMessage = "Região é obrigatória")]
        public int cadtbunfederativa_regiao { set; get; }
        public int cadtbunfederativa_codibge { set; get; }
        [Dapper.Contrib.Extensions.Write(false)]
        public string uf_mostrar
        {
            get
            {
                return cadtbunfederativa_pksigla + " - " + cadtbunfederativa_nome;
            }
        }
    }
}
