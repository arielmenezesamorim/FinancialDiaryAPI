using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Security
{
    public class TokenConfigurations
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int Seconds { get; set; }
        public int SecondsRefreshToken { get; set; }
        public string Secret { get; set; }
    }
}
