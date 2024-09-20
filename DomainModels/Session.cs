using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class Session : Common
    {
        public Guid SessionId { get; set; }
        public int UserId { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string DeviceInfo { get; set; }

        public User User { get; set; }
    }

}
