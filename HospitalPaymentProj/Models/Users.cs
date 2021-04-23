using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalPaymentProj.Models
{
    public class Users
    {
        public int Id { get; set; }

        public string UserName { get; set; }
        public string UserNameLogin { get; set; }
        
        public string Password { get; set; }
        public string PasswordLogin { get; set; }
    }
}
