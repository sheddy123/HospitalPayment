using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalPaymentProj.Models
{
    public class PatientPayment_VM
    {
        [Required]
        public string FirstName { get; set; }

        public string DateCreated { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Amount { get; set; }

        public string UserId { get; set; }

        public string AdminAttended { get; set; }
    }
}
