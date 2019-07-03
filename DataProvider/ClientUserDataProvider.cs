using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AG_MS_Authentication.Model;
using log4net;

namespace AG_MS_Authentication.DataProvider
{
    public class ClientUserDataProvider : BaseDataProvider
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static ClientUser ValidateFetchUser(int clientId, string userName, string password)
        {
            ClientUser clientUser = null;            

            if(clientId == 1 && userName == "testuser" && password == "testpassword"){
                
                clientUser = new ClientUser();
                
                clientUser.Id = 1;
           
                clientUser.ClientId = 1;
                        
                clientUser.UserName = "testuser";

                clientUser.FirstName = "Ftest";

                clientUser.LastName = "Ltest";

                clientUser.Email = "test@example.com";

            }
            return clientUser;

        }

        public static bool VerifyUserService(int clientUserId, string serviceName, out int serviceId)
        {
            serviceId = 0;
            serviceName = "Dummyservice";
            return true;
        }

        public static void logToken(int clientUserId, string token)
        {
            
        }
    }
}
