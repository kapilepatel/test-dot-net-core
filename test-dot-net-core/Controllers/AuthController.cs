using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System.Runtime.Serialization.Json;
using System.IO;
using log4net;
using AG_MS_Authentication.DataProvider;
using AG_MS_Authentication.Model;

namespace MS_Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private IOptions<Audience> _settings;


        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public AuthController(IOptions<Audience> settings)
        {
            this._settings = settings;
            JWTManager jWTManager = new JWTManager(_settings);
        }


        [HttpGet("ValidateToken")]
        public JsonResult ValidateToken(string token, string service_name)
        {
            try
            {
                bool IsAuthenticated = JWTManager.ValidateToken(token, service_name, out int serviceId);
                var responseJson = new
                {
                    auth = IsAuthenticated,
                    service_id = serviceId
                };
                return Json(responseJson);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                var responseJson = new
                {
                    auth = false,
                    service_id = 0
                };
                return Json(responseJson);
            }
        }

        [HttpGet("GetToken")]
        public JsonResult GetToken(int clientId, string userName, string password)
        {
            try
            {
                log.Info("GetToken method called for client id: " + clientId + " and username: " + userName);
                ClientUser clientUser = ClientUserDataProvider.ValidateFetchUser(clientId, userName, password);
                if (clientUser != null)
                {
                    int clientUserId = clientUser.Id;
                    string encodedJwt = JWTManager.GenerateToken(clientUserId);
                    var responseJson = new
                    {
                        success = "true",
                        message = "Authentication successful",
                        access_token = encodedJwt,
                        expires_in_seconds = (int)TimeSpan.FromMinutes(60).TotalSeconds
                    };
                    log.Info("Token generated for client id:" + clientId + " and username: " + userName);
                    ClientUserDataProvider.logToken(clientUser.Id, encodedJwt);

                    return Json(responseJson);
                }
                else
                {
                    var responseJson = new
                    {
                        success = "false",
                        message = "Not authenticated",
                        access_token = "",
                        expires_in_seconds = 0
                    };
                    log.Info("Client not authenticated client id:" + clientId + " and username: " + userName);
                    return Json(responseJson);
                }
            }
            catch (Exception ex)
            {
                log.Error("Token generation failed for client id:" + clientId + " and username: " + userName);
                log.Error(ex);
                var responseJson = new
                {
                    success = "false",
                    message = "Some error occured",
                    access_token = "",
                    expires_in_seconds = 0
                };
                return Json(responseJson);
            }
        }

    }


}