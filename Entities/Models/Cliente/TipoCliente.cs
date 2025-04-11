using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Cliente
{
    [Dapper.Contrib.Extensions.Table("rectbtipocliente")]
    public class TipoCliente
    {
        [Dapper.Contrib.Extensions.Key]
        public int rectbtipocliente_pkseq { set; get; }
        public string rectbtipocliente_descricao { set; get; }
    }
}
