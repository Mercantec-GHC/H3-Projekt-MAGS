using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace DomainModels
{
    public class User : Common
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }

        public ICollection<Session> Sessions { get; set; }
    }

}
