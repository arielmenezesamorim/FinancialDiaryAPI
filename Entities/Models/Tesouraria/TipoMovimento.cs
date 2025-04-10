using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Tesouraria
{
    [Dapper.Contrib.Extensions.Table("testbtipomovimento")]
    public class TipoMovimento
    {
        [Dapper.Contrib.Extensions.Key]
        public int cxatbtmv_pkseq { set; get; }
        public int cxatbtmv_tipo { set; get; }
        public int cxatbtmv_indicador { set; get; }
        [Required(ErrorMessage = "Descrição é obrigatória.")]
        public string cxatbtmv_descricao { set; get; }
    }
}
