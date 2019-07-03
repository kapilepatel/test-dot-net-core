using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Text;

namespace AG_MS_Authentication
{
    public class SQLServerConn
    {
        string _Enterprise_Service_ConString = string.Empty;        

        private SQLServerConn()
        {
            try
            {
                _Enterprise_Service_ConString = ConfigurationManager.ConnectionStrings["Enterprise_ServiceConnectionString"].ConnectionString;                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetSQLServerURL(SQLDBName name)
        {
            string url = string.Empty;
            switch (name)
            {
                case SQLDBName.Enterprise_Service:
                    url = _Enterprise_Service_ConString;
                    break;
               
            }
            return url;
        }

        #region Singleton Implementation
        private static SQLServerConn _instance = null;
        public static SQLServerConn GetInstance()
        {
            if (_instance == null)
                _instance = new SQLServerConn();
            return _instance;
        }
        #endregion
    }
    public enum SQLDBName
    {       
        Enterprise_Service
    }

    public class DBUpdateStatus
    {
        public string ErrorMessage { get; set; }
        //--- -1 is failed, 1 is success
        public int StatusCode { get; set; }
        public int AffectedRows { get; set; }
    }
}
