using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Cadastro
{
    [Dapper.Contrib.Extensions.Table("cadtbpais")]
    public class Pais
    {
        [Dapper.Contrib.Extensions.ExplicitKey]
        [Required(ErrorMessage = "Sigla é obrigatório")]
        [StringLength(maximumLength: 3, MinimumLength = 3, ErrorMessage = "Sigla deve conter {1} caracters e no mínimo {2} dígitos. Ex: BRA")]
        public string cadtbpais_pksigla { get; set; } = null!;
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string cadtbpais_nome { get; set; } = null!;
        [Required(ErrorMessage = "Continente é obrigatório")]
        public int cadtbpais_continente { get; set; }
        public string cadtbpais_nacionalidade { get; set; } = null!;
        public int cadtbpais_codbacen { get; set; }
    }
}
