using Microsoft.IdentityModel.Tokens;
using ProjctModel.Entites;
using ProjctModel.VeiwModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace ProjctModel.Services
{
   public class SampleJwt
    {
        public static string GetToke(User user , List<Claim> claims)
        {
            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("4A441292-4131-4862-99F5-F074D8566DF1"));
            var SignCredential = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature);


            //امنیت خواندن
            var encredentialKey = "8078C32D-4D69-458E-8BAE-3752C50A1880";
            var encredential = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(encredentialKey));
            var encredentialsecurity = new EncryptingCredentials(encredential, SecurityAlgorithms.Aes256KW, SecurityAlgorithms.Aes256CbcHmacSha512);


            var Descriptor = new SecurityTokenDescriptor()
            {
                Issuer = "www.goolge.com",
                Audience = "ww.google.com",
                Subject = new ClaimsIdentity(claims),
                SigningCredentials= SignCredential,
                NotBefore= DateTime.Now,
                Expires= DateTime.Now.AddDays(7)
                //EncryptingCredentials= encredentialsecurity,
                //CompressionAlgorithm =CompressionAlgorithms.Deflate
            };


            var JwtTokenHandler = new JwtSecurityTokenHandler();
            var Result = JwtTokenHandler.CreateToken(Descriptor);

            return JwtTokenHandler.WriteToken(Result);

        }
     }
    }

