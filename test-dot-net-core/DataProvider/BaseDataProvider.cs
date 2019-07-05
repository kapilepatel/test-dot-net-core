using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AG_MS_Authentication
{

    public class BaseDataProvider
    {
        protected static SQLServerConn _SQLServerConn = SQLServerConn.GetInstance();
        //protected static MongoServerConn _MongoServerConn = MongoServerConn.GetInstance();
        //protected static VerticaServerConn _VerticaServerConn = VerticaServerConn.GetInstance();
    }
}
