using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AG_MS_Authentication.Model
{
    public class ClientUser
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
