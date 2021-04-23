using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalPaymentProj.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int PatientId { get; set; }

        public decimal Amount { get; set; }

        public string DateCreated { get; set; }
    }
}
