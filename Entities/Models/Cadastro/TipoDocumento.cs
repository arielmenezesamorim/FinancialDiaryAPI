using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Cadastro
{
    [Dapper.Contrib.Extensions.Table("cadtbtipodocumento")]
    public class TipoDocumento
    {
        [Dapper.Contrib.Extensions.Key]
        public int cadtbtipodocumento_pkseq { get; set; }
        public string cadtbtipodocumento_sigla { get; set; }
        [Required(ErrorMessage = "Descrição é obrigatória")]
        public string cadtbtipodocumento_descricao { get; set; }
        [Dapper.Contrib.Extensions.Write(false)]
        public string tipodoc_mostrar
        {
            get
            {
                return cadtbtipodocumento_sigla + " - " + cadtbtipodocumento_descricao;
            }
        }
    }
}
