using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mantimentos.App.Business.JWT
{
    public class ConfigJWT
    {
        private readonly IConfiguration _configuration;

        public ConfigJWT(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string BuildToken()
        {
            //Recebendo os dados.
            var issuer = _configuration["JWT:issuer"] ?? null;
            var audience = _configuration["JWT:audience"] ?? null;
            var claims = new[]
            {
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti, "desafiomantimentostafaltandooque"),
            };
            //recolhendo os dados de nosso appsetttings, coletando o time que selecionamos para o token ser valido, interessante é fazer para expirar mais rapido maximo 2 horas.
            var expiration = DateTime.UtcNow.AddSeconds(double.Parse(_configuration["JWT:expiresSeconds"]));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            //Ate o momento a criptografia 256 não foi quebrada mas sempre procurar melhorar pois existe Hackers bem empenhados.
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            JwtSecurityToken token = new(
               issuer: issuer,
               audience: audience,
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public (string uniqueId, string error) ValidateToken(string token)
        {
            try
            {
                SymmetricSecurityKey mySecurityKey = new(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
                JwtSecurityTokenHandler tokenHandler = new();

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = mySecurityKey
                }, out SecurityToken validatedToken);

                JwtSecurityToken jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
                string uniqueId = jwtToken.Claims.First(claim => claim.Type == "jti").Value;

                return (uniqueId, string.Empty);
            }
            catch (SecurityTokenExpiredException ex)
            {
                return (string.Empty, ex.Message);
            }
            catch (SecurityTokenException ex)
            {
                return (string.Empty, ex.Message);
            }
            catch (Exception ex)
            {
                return (string.Empty, ex.Message);
            }
        }
    }
}
