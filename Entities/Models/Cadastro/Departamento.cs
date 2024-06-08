using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Cadastro
{
    [Dapper.Contrib.Extensions.Table("cadtbdepartamento")]
    public class Departamento
    {
        [Dapper.Contrib.Extensions.Key]
        public int cadtbdepartamento_pkseq {  get; set; }

        [Required(ErrorMessage = "Sigla é obrigatória")]
        public string cadtbdepartamento_sigla { set; get; }

        [Required(ErrorMessage = "Descrição é obrigatória")]
        public string cadtbdepartamento_descricao { get; set; }

        [Dapper.Contrib.Extensions.Write(false)]
        public string urlprograma { set; get; }
    }
}
