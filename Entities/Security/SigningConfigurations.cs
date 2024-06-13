using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Security
{
    public class SigningConfigurations
    {
        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }

        public SigningConfigurations(TokenConfigurations tokenConfiguration)
        {
            //using (var provider = new RSACryptoServiceProvider(2048))
            //{
            //    Key = new RsaSecurityKey(provider.ExportParameters(true));
            //}

            //SigningCredentials = new SigningCredentials(
            //    Key, SecurityAlgorithms.RsaSha256Signature);


            var symmetricKey = Convert.FromBase64String(tokenConfiguration.Secret);
            Key = new SymmetricSecurityKey(symmetricKey);
            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature);
        }
    }
}
