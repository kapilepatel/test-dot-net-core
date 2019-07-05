using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

using System.Runtime.Serialization.Json;
using System.IO;
using Microsoft.IdentityModel.Logging;

using AG_MS_Authentication.DataProvider;

namespace MS_Authentication
{
    public class JWTManager
    {
        private static IOptions<Audience> _settings;



        public JWTManager(IOptions<Audience> settings)
        {
            _settings = settings;
        }

        public static string GenerateToken(int clientUserId)
        {
            var now = DateTime.UtcNow;

            var claims = new Claim[]
            {
                new Claim("ClientUserId",clientUserId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, ""),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
            };

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_settings.Value.Secret));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = _settings.Value.Iss,
                ValidateAudience = true,
                ValidAudience = _settings.Value.Aud,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true,
            };

            var jwt = new JwtSecurityToken(
                issuer: _settings.Value.Iss,
                audience: _settings.Value.Aud,
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(60)),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public static bool ValidateToken(string token, string serviceName, out int serviceId)
        {
            serviceId = 0;
            var simplePrinciple = GetPrincipal(token);
            var identity = simplePrinciple?.Identity as ClaimsIdentity;

            if (identity == null)
                return false;

            if (!identity.IsAuthenticated)
                return false;

            //var usernameClaim = identity.FindFirst(ClaimTypes.Name);
            //username = usernameClaim?.Value;
            var clientUserIdClaim = identity.FindFirst("ClientUserId");
            string clientUserIdString = clientUserIdClaim?.Value;

            if (string.IsNullOrEmpty(clientUserIdString))
                return false;

            int clientUserId = Convert.ToInt32(clientUserIdString);
            bool userServicePermission = ClientUserDataProvider.VerifyUserService(clientUserId, serviceName, out serviceId);
            if (userServicePermission)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                IdentityModelEventSource.ShowPII = true;

                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;

                var symmetricKey = Convert.FromBase64String(_settings.Value.Secret);
                var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_settings.Value.Secret));

                var validationParameters = new TokenValidationParameters
                {

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingKey,
                    ValidateIssuer = true,
                    ValidIssuer = _settings.Value.Iss,
                    ValidateAudience = true,
                    ValidAudience = _settings.Value.Aud,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    RequireExpirationTime = true,
                };

                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

                return principal;
            }
            catch (Exception)
            {
                //throws exception when not able to validate
                return null;
            }
        }


    }
}
