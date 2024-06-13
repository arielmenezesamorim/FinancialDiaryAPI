using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Security
{
    public class RefreshTokenData
    {
        public string refreshToken { get; set; }
        public string email { get; set; }
    }
}
